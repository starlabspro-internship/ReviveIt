using Application.DTO;
using Application.Features.User;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    [Route("ProfileUpdate")]
    [ApiController]
    public class ProfileUpdateController : ControllerBase
    {
        private readonly ProfilePictureFeature _profilePictureFeature;
        private readonly UpdateProfileFeature _updateProfileFeature;
        private readonly UserManager<Users> _userManager;
        private readonly UserInfoFeature _userInfoFeature;

        public ProfileUpdateController(ProfilePictureFeature profilePictureFeature, UpdateProfileFeature updateProfileFeature, UserManager<Users> userManager, UserInfoFeature userInfoFeature)
        {
            _profilePictureFeature = profilePictureFeature;
            _updateProfileFeature = updateProfileFeature;
            _userManager = userManager;
            _userInfoFeature = userInfoFeature;
        }

        [HttpGet("api/info")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string type)
        {
            var result = await _userInfoFeature.HandleAsync(User, type);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { Error = result.Message });
        }

        [HttpPost("api/upload")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile profilePicture)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.UploadProfilePictureAsync(profilePicture, userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("api/remove")]
        public async Task<IActionResult> RemoveProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.RemoveProfilePictureAsync(userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("api/get")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var result = await _profilePictureFeature.GetProfilePictureAsync(userIdClaim);

            return Ok(result);
        }

        [HttpPut("api/update-profile")]
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