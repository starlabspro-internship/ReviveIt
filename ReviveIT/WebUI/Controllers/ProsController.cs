using Application.DTO;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("Pros")]
    public class ProsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Pros()
        {
            var technicians = _context.Users
                .Where(u => u.Role == UserRole.Technician || u.Role == UserRole.Company)
                .Select(u => new TechnicianViewModel
                {
                    FullName = u.FullName,
                    Expertise = u.Expertise,
                    Experience = u.Experience ?? 0,
                    CompanyName = u.CompanyName,
                    CompanyAddress = u.CompanyAddress,
                    ProfilePicture = string.IsNullOrEmpty(u.ProfilePicture) ? "/images/default-expert.jpg" : u.ProfilePicture,
                    Review = _context.Reviews
                            .Where(r => r.UserId == u.Id)
                            .Select(r => r.Content)
                            .FirstOrDefault() ?? "No reviews available",
                    Rating = _context.Reviews
                            .Where(r => r.UserId == u.Id)
                            .Any() ? _context.Reviews
                            .Where(r => r.UserId == u.Id)
                            .Average(r => r.Rating) : 0
                })
                .Take(3)
                .ToList();
            ViewBag.IsProsPage = true;
            return View(technicians);
        }
    }
}