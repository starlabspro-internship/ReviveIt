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
        private readonly UserManager<Users> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileUpdate(UserManager<Users> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
                return BadRequest("No file uploaded.");

            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Unauthorized("User not found.");

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/profile");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(profilePicture.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream);
            }

            user.ProfilePicture = $"/images/profile/{fileName}";
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Profile picture uploaded successfully.", ProfilePicture = user.ProfilePicture });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Unauthorized("User not found.");

            if (string.IsNullOrEmpty(user.ProfilePicture))
                return BadRequest("No profile picture to remove.");

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePicture.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            user.ProfilePicture = null;
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Profile picture removed successfully." });
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Unauthorized("User not found.");

            if (string.IsNullOrEmpty(user.ProfilePicture))
                return NotFound("No profile picture found.");

            var profilePictureUrl = $"{Request.Scheme}://{Request.Host}{user.ProfilePicture}";
            return Ok(new { ProfilePictureUrl = profilePictureUrl });
        }
    }
}