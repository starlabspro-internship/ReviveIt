using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.DTO;
using System.Linq;
using Infrastructure.Data;

namespace WebUI.Controllers
{ 
    public class MjeshtritController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MjeshtritController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Mjeshtrit()
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
            ViewBag.IsMjeshtritPage = true;
            return View(technicians);
        }
    }
}