using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CompleteProfileController : Controller
    {
        [HttpGet("CompleteProfile")]
        public IActionResult CompleteProfile()
        {
            return View();
        }
    }
}