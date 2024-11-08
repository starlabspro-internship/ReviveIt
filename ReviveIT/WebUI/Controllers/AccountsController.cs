using Application.DTO;
using Application.Features.Accounts;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly LoginFeature _loginFeature;

    public AccountsController(LoginFeature loginFeature)
    {
        _loginFeature = loginFeature;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var result = await _loginFeature.AuthenticateUser(loginDto);
        if (!result.IsSuccess)
            return Unauthorized(new { Message = result.ErrorMessage });

        return Ok(new { Token = result.Token });
    }
}
