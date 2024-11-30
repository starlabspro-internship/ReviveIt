using Application.DTO;
using Application.Features.User;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Users> _userManager;

        public ProfileUpdate(ProfilePictureFeature profilePictureFeature, UpdateProfileFeature updateProfileFeature, UserManager<Users> userManager)
        {
            _profilePictureFeature = profilePictureFeature;
            _updateProfileFeature = updateProfileFeature;
            _userManager = userManager;
        }

        [HttpGet("fullname")]
        public async Task<IActionResult> GetFullName()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token");

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return NotFound("User not found");

            return Ok(new { FullName = user.FullName });
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetRole()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token");

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return NotFound("User not found");

            string roleAsString = user.Role.ToString();

            return Ok(new { Role = roleAsString });
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