using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
