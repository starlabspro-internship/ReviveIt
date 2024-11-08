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
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var token = await _loginFeature.AuthenticateUser(loginDto);
        if (token == null)
            return Unauthorized(new { Message = "Your Email or Password may be incorrect." });

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        registerDto.Role = MapRole(registerDto.Role);

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

    private string MapRole(string role)
    {
        return role == "Company" ? "Technician" : role;
    }
}
