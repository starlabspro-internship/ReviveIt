using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}