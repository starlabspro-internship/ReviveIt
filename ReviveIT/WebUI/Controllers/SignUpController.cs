using Application.DTO;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("SignUp")]
    public class SignUpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SignUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SignUp()
        {
            var categories = _context.Categories.ToList();
            return View(new RegisterViewModel { Categories = categories });
        }
    }
}