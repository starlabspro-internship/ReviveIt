using Application.DTO;
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
        private readonly UpdateProfileFeature _updateProfileFeature;

        public ProfileUpdate(ProfilePictureFeature profilePictureFeature, UpdateProfileFeature updateProfileFeature)
        {
            _profilePictureFeature = profilePictureFeature;
            _updateProfileFeature = updateProfileFeature;
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

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token");

            var result = await _updateProfileFeature.UpdateProfileAsync(userIdClaim, updateProfileDTO);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}