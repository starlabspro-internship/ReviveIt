using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("SignUp")]
    public class SignUpController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
