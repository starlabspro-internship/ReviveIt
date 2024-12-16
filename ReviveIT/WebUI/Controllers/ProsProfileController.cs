using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("ProsProfileView")]
    public class ProsProfileViewController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult ProsProfile(string id)
        {
            ViewBag.TechnicianId = id; 
            ViewBag.IsProsPage = true;
            return View("~/Views/Pros/ProsProfile.cshtml");
        }
    }
}
