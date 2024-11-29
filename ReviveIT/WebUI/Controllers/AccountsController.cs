using Application.DTO;
using Application.Features.Accounts;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly LoginFeature _loginFeature;
    private readonly RegisterFeature _registerFeature;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<Users> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenHelper _tokenHelper;

    public AccountsController(LoginFeature loginFeature, RegisterFeature registerFeature, IEmailSender emailSender, UserManager<Users> userManager, IRefreshTokenRepository refreshTokenRepository, TokenHelper tokenHelper)
    {
        _loginFeature = loginFeature;
        _registerFeature = registerFeature;
        _emailSender = emailSender;
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _loginFeature.AuthenticateUser(loginDto);
        if (!result.IsSuccess)
            return Unauthorized(new { Message = result.ErrorMessage });

        SetTokenCookie(result.Token);

        return Ok(new { Message = "Login successful.", result.Token });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByEmailAsync(email);
       
        if (string.IsNullOrEmpty(user.Id))
        {
            return Unauthorized(new { message = "User ID not found in token." });
        }

        var result = await _refreshTokenRepository.RemoveRefreshTokenAsync(user.Id);

        if (result)
        {
            Response.Cookies.Delete("jwtToken");
            Response.Cookies.Delete("refreshToken");
            return Ok(new { message = "Successfully logged out" });
        }

        return NotFound(new { message = "Refresh token not found." });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return BadRequest(new { success = false, message = "Validation failed", errors = errors });
        }

        var registerResult = await _registerFeature.RegisterUserAsync(registerDto);

        if (!registerResult.Success)
        {
            return BadRequest(new { success = false, message = registerResult.Message });
        }

        return Ok(new { success = true, token = registerResult.Token, message = registerResult.Message });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return BadRequest("Invalid email confirmation request.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
            return Ok("Email confirmed successfully!");

        return BadRequest("Email confirmation failed.");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Success = false, Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = _tokenHelper.GeneratePasswordResetLink(resetToken);

            await _emailSender.SendEmailAsync(
                user.Email,
                "Reset Your Password",
                $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>"
            );
        }

        return Ok("If your email is registered, you will receive a password reset email.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if (string.IsNullOrWhiteSpace(model.Token) || string.IsNullOrWhiteSpace(model.NewPassword))
        {
            return BadRequest("Token and NewPassword are required.");
        }

        Users user = null;
        foreach (var u in await _userManager.Users.ToListAsync())
        {
            if (await _userManager.VerifyUserTokenAsync(u, "Default", "ResetPassword", model.Token))
            {
                user = u;
                break;
            }
        }

        if (user == null)
        {
            return BadRequest("Invalid or expired token.");
        }

        var resetResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (resetResult.Succeeded)
        {
            return Ok(new { Success = true, Message = "Password has been successfully reset." });
        }

        return BadRequest(new
        {
            Success = false,
            Message = "Password reset failed.",
            Errors = resetResult.Errors.Select(e => e.Description)
        });
    }

    private void SetTokenCookie(string jwtToken)
    {
        var secure = Request.IsHttps;

        Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = secure,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(3600),
        });
    }
}
