using Application.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Pros()
        {
            ViewBag.IsProsPage = true;
            return View();
        }

        [HttpGet("api/GetPros")]
        public IActionResult GetPros(string? keywords, int? selectedCityId, int? selectedCategoryId, int skipCount = 0, int takeCount = 3)
        {
            var query = _context.Users
                .Where(u => u.Role == UserRole.Technician || u.Role == UserRole.Company)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(u =>
                    u.FullName.Contains(keywords) ||
                    u.Expertise.Contains(keywords) ||
                    u.CompanyName.Contains(keywords));
            }

            if (selectedCityId.HasValue)
            {
                query = query.Where(u =>
                    _context.OperatingCities.Any(oc => oc.userId == u.Id && oc.CityId == selectedCityId.Value));
            }

            if (selectedCategoryId.HasValue)
            {
                query = query.Where(u =>
                    _context.UserCategories.Any(uc => uc.UserId == u.Id && uc.CategoryId == selectedCategoryId.Value));
            }

            var totalCount = query.Count();

            var technicians = GetTechnicians(skipCount, takeCount, query);

            return Ok(new
            {
                data = technicians,
                total = totalCount
            });
        }

        [HttpGet("api/IsAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            return Ok(new { isAuthenticated });
        }

        private List<TechnicianViewModel> GetTechnicians(int skipCount, int takeCount, IQueryable<Users>? filteredQuery = null)
        {
            var query = filteredQuery ?? _context.Users
                .Where(u => u.Role == UserRole.Technician || u.Role == UserRole.Company);

            var users = query
                .OrderBy(u => u.Id)
                .Skip(skipCount)
                .Take(takeCount)
                .AsNoTracking()
                .ToList();

            var technicians = users.Select(u => new TechnicianViewModel
            {
                Id = u.Id,
                FullName = u.Role == UserRole.Company ? u.CompanyName : u.FullName,
                Role = u.Role.ToString(),
                Expertise = u.Expertise,
                Experience = u.Experience ?? 0,
                CompanyName = u.CompanyName,
                CompanyAddress = u.CompanyAddress,
                ProfilePicture = GetProfilePicture(u),
                Review = GetReview(u),
                Rating = GetRating(u)
            }).ToList();

            return technicians;
        }

        private string GetProfilePicture(Users user)
        {
            return user.Role == UserRole.Company
                ? (string.IsNullOrEmpty(user.ProfilePicture) ? "/images/defaultCompanyPicture.png" : user.ProfilePicture)
                : (string.IsNullOrEmpty(user.ProfilePicture) ? "/images/defaultProfilePicture.png" : user.ProfilePicture);
        }

        private string GetReview(Users user)
        {
            return _context.Reviews
                .Where(r => r.UserId == user.Id)
                .Select(r => r.Content)
                .FirstOrDefault() ?? "No reviews available";
        }

        private double GetRating(Users user)
        {
            return _context.Reviews
                .Where(r => r.UserId == user.Id)
                .Any() ? _context.Reviews
                .Where(r => r.UserId == user.Id)
                .Average(r => r.Rating) : 0;
        }
    }
}
