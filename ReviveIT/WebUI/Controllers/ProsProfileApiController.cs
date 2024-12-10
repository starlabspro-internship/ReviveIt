using Infrastructure.Data; // Replace with your actual DbContext namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/ProsProfile")]
    [ApiController]  // Use ApiController attribute for automatic model binding and error handling
    public class ProsProfileApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProsProfileApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API to fetch technician profile by ID
        [HttpGet("GetTechnicianProfile/{id}")]
        public async Task<IActionResult> GetTechnicianProfile(string id)
        {
            var user = await _context.Users
                .Include(u => u.Portfolios) // Include portfolio data
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound(new { message = "Technician not found." });

            var profile = new
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                Expertise = user.Expertise,
                Experience = user.Experience,
                Portfolios = user.Portfolios.Select(p => new
                {
                    p.Title,
                    p.Description,
                    p.FilePath,
                    p.FileType
                })
            };

            return Ok(profile);
        }
    }
}
