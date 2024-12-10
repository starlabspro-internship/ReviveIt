using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("ProsProfileView")]
    public class ProsProfileViewController : Controller
    {
        // Serves the static/dynamic ProsProfile view
        [HttpGet("{id}")]
        public IActionResult ProsProfile(string id)
        {
            ViewBag.TechnicianId = id; // Pass the technician ID to the view
            ViewBag.IsProsPage = true;
            return View("~/Views/Pros/ProsProfile.cshtml"); // Explicit path to the view
        }
    }
}
