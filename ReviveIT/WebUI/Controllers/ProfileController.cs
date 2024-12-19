using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly IApplicationDbContext _context;

        public ProfileController(UserManager<Users> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Profile()
        {
            var userProfile = new UserProfileModel
            {
                Name = GetUserName() ?? "Unavailable",
                Email = GetUserEmail() ?? "Unavailable",
                ProfileType = "Guest"
            };
            return View(userProfile);
        }

        private string GetUserName()
        {
            return null;
        }

        private string GetUserEmail()
        {
            return null;
        }

        [HttpPost]
        public IActionResult ChangeProfileType(string profileType)
        {
            TempData["ProfileType"] = profileType;

            return RedirectToAction("Profile");
        }

        [HttpGet("get-description")]
        public async Task<IActionResult> GetDescription()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new { description = user.Description });
        }

        [HttpPut("update-description")]
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

        [HttpGet("get-selected-categories")]
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

        [HttpPost("updateCategories")]
        public async Task<IActionResult> UpdateCategories([FromBody] List<int> categoryIds)
        {
            if (categoryIds == null || !categoryIds.Any())
            {
                return BadRequest(new { message = "Category list cannot be empty." });
            }

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
    }
}