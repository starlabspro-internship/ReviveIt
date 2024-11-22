using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("LogIn")]
    public class LogInController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}