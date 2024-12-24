using Application.DTO;
using Application.Features.User;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly IApplicationDbContext _context;


        public ProfileUpdateController(ProfilePictureFeature profilePictureFeature, UpdateProfileFeature updateProfileFeature, UserManager<Users> userManager, UserInfoFeature userInfoFeature, IApplicationDbContext context)
        {
            _profilePictureFeature = profilePictureFeature;
            _updateProfileFeature = updateProfileFeature;
            _userManager = userManager;
            _userInfoFeature = userInfoFeature;
            _context = context;
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

        [HttpGet("api/get-description")]
        public async Task<IActionResult> GetDescription()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new { description = user.Description });
        }

        [HttpPut("api/update-description")]
        public async Task<IActionResult> UpdateDescription([FromBody] UpdateDescriptionDto updateDescriptionDto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID claim is missing." });

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return NotFound(new { message = "User not found" });

            if (string.IsNullOrWhiteSpace(updateDescriptionDto.Description))
                return BadRequest(new { message = "Description cannot be empty." });

            user.Description = updateDescriptionDto.Description;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(500, new { message = "Failed to update description." });

            return Ok(new { message = "Description updated successfully." });
        }

        [HttpGet("api/get-selected-categories")]
        public async Task<IActionResult> GetSelectedCategories()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            var userCategories = await _context.UserCategories
                .Where(uc => uc.UserId == userId)
                .Select(uc => new
                {
                    uc.Category.CategoryID,
                    uc.Category.Name
                })
                .ToListAsync();

            return Ok(userCategories);
        }

        [HttpPost("api/update-categories")]
        public async Task<IActionResult> UpdateCategories([FromBody] List<int> categoryIds)
        {
            if (categoryIds == null || !categoryIds.Any())
                return BadRequest(new { message = "Category list cannot be empty." });

            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var existingUserCategories = _context.UserCategories.Where(uc => uc.UserId == userId);
            _context.UserCategories.RemoveRange(existingUserCategories);

            var newUserCategories = categoryIds.Select(categoryId => new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId
            });

            await _context.UserCategories.AddRangeAsync(newUserCategories);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categories updated successfully" });
        }

        [HttpGet("api/get-selected-cities")]
        public async Task<IActionResult> GetSelectedCities()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var selectedCities = await _context.OperatingCities
                .Where(oc => oc.userId == userId)
                .Select(oc => new
                {
                    oc.CityId,
                    CityName = oc.City.CityName
                })
                .ToListAsync();

            return Ok(selectedCities);
        }

        [HttpPost("api/update-operating-cities")]
        public async Task<IActionResult> UpdateOperatingCities([FromBody] List<int> cityIds)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var existingOperatingCities = _context.OperatingCities.Where(oc => oc.userId == userId);
            _context.OperatingCities.RemoveRange(existingOperatingCities);

            var newOperatingCities = cityIds.Select(cityId => new OperatingCity
            {
                userId = userId,
                CityId = cityId
            });

            await _context.OperatingCities.AddRangeAsync(newOperatingCities);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Operating cities updated successfully" });
        }
    }
}