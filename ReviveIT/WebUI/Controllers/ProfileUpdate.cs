using Application.Features.User;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileUpdate : ControllerBase
    {
        private readonly ProfilePictureFeature _profilePictureFeature;

        public ProfileUpdate(ProfilePictureFeature profilePictureFeature)
        {
            _profilePictureFeature = profilePictureFeature;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile profilePicture)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.UploadProfilePictureAsync(profilePicture, userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.RemoveProfilePictureAsync(userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.GetProfilePictureAsync(userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return NotFound(result);
        }
    }
}
