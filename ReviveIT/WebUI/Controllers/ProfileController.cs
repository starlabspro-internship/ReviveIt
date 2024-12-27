using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("Profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeProfileType(string profileType)
        {
            TempData["ProfileType"] = profileType;

            return RedirectToAction("Profile");
        }
    }
}
