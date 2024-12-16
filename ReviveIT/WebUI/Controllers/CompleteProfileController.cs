using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CompleteProfileController : Controller
    {
        [Authorize(Roles = "Technician,Company")]
        [HttpGet("CompleteProfile")]
        public IActionResult CompleteProfile()
        {
            ViewBag.IsCompleteProfilePage = true;
            return View();
        }
    }
}