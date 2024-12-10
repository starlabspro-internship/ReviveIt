using Application.Features.User;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProsProfileApiController : ControllerBase
{
    private readonly GetTechnicianProfileFeature _getTechnicianProfileFeature;

    public ProsProfileApiController(GetTechnicianProfileFeature getTechnicianProfileFeature)
    {
        _getTechnicianProfileFeature = getTechnicianProfileFeature;
    }

    [HttpGet("GetTechnicianProfile/{id}")]
    public async Task<IActionResult> GetTechnicianProfile(string id)
    {
        var result = await _getTechnicianProfileFeature.ExecuteAsync(id);

        if (!result.IsSuccess)
            return NotFound(new { message = result.ErrorMessage });

        return Ok(result.Data);
    }
}