using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompleteProfileApiController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly ApplicationDbContext _context;

        public CompleteProfileApiController(UserManager<Users> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("CompleteProfile")]
        public async Task<IActionResult> CompleteProfile([FromBody] CompleteProfileDto profileDto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Success = false, Message = "User not authenticated." });
            }

            var user = await _userManager.FindByIdAsync(userIdClaim);

            if (user == null)
            {
                return NotFound(new { Success = false, Message = "User not found." });
            }

            if (!string.IsNullOrEmpty(profileDto.Phone))
            {
                var existingUserWithPhone = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == profileDto.Phone);

                if (existingUserWithPhone != null && existingUserWithPhone.Id != user.Id)
                {
                    return BadRequest(new { Success = false, Message = "This phone number is already in use by another account." });
                }

                user.PhoneNumber = profileDto.Phone;
            }

            if (!string.IsNullOrEmpty(profileDto.Description))
            {
                user.Description = profileDto.Description;
            }

            // Remove existing cities for this user
            var existingOperatingCities = await _context.OperatingCities
                .Where(oc => oc.userId == userIdClaim)
                .ToListAsync();
            _context.OperatingCities.RemoveRange(existingOperatingCities);

            // Add new selected cities
            if (profileDto.Cities != null && profileDto.Cities.Any())
            {
                var operatingCities = profileDto.Cities.Select(cityId => new OperatingCity
                {
                    CityId = cityId,
                    userId = userIdClaim
                }).ToList();

                await _context.OperatingCities.AddRangeAsync(operatingCities);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();  // Save changes to the OperatingCity table
                return Ok(new { Success = true, Message = "Profile and cities updated successfully." });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "There was an error updating your profile." });
            }
        }
    }


public class CompleteProfileDto
    {
        public string Phone { get; set; }
        public string Description { get; set; }
        public List<int> Cities { get; set; }  // List of selected city IDs
    }
}
