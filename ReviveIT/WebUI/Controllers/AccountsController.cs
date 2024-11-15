using Application.DTO;
using Application.Features.Accounts;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly LoginFeature _loginFeature;
    private readonly RegisterFeature _registerFeature;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<Users> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AccountsController(LoginFeature loginFeature, RegisterFeature registerFeature, IEmailSender emailSender, UserManager<Users> userManager, IRefreshTokenRepository refreshTokenRepository)
    {
        _loginFeature = loginFeature;
        _registerFeature = registerFeature;
        _emailSender = emailSender;
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _loginFeature.AuthenticateUser(loginDto);
        if (!result.IsSuccess)
            return Unauthorized(new { Message = result.ErrorMessage });

        return Ok(new { Token = result.Token });
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
}