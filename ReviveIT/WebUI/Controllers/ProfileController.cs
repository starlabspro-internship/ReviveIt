using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            var userProfile = new UserProfileModel
            {
                Name = GetUserName() ?? "Unavailable",
                Email = GetUserEmail() ?? "Unavailable",
                ProfileType = "Guest"
            };
            return View(userProfile);
        }

        private string GetUserName()
        {
            return null;
        }

        private string GetUserEmail()
        {
            return null;
        }

        [HttpPost]
        public IActionResult ChangeProfileType(string profileType)
        {
            TempData["ProfileType"] = profileType;

            return RedirectToAction("Profile");
        }

    }
}