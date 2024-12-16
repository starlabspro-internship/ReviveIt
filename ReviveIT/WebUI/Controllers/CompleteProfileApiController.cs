using Application.DTO;
using Application.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Technician,Company")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompleteProfileApiController : ControllerBase
    {
        private readonly CompleteProfileFeature _profileLogic;

        public CompleteProfileApiController(CompleteProfileFeature profileLogic)
        {
            _profileLogic = profileLogic;
        }

        [HttpPost("CompleteProfile")]
        public async Task<IActionResult> CompleteProfile([FromBody] CompleteProfileDto profileDto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            var result = await _profileLogic.UpdateProfileAsync(userIdClaim, profileDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}