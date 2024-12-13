using Domain.Entities;
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

        public CompleteProfileApiController(UserManager<Users> userManager)
        {
            _userManager = userManager;
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

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Success = true, Message = "Profile updated successfully." });
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
    }
}
