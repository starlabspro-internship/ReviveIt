using Application.DTO;
using Application.Features.Accounts;
using Microsoft.AspNetCore.Mvc;

[Route("Accounts")]
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly LoginFeature _loginFeature;
    private readonly RegisterFeature _registerFeature;

    public AccountsController(LoginFeature loginFeature, RegisterFeature registerFeature)
    {
        _loginFeature = loginFeature;
        _registerFeature = registerFeature;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _loginFeature.AuthenticateUser(loginDto);
        if (!result.IsSuccess)
            return Unauthorized(new { Message = result.ErrorMessage });

        // Return the token and redirect URL in the response
        return Ok(new { Token = result.Token, RedirectUrl = result.RedirectUrl });
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
}