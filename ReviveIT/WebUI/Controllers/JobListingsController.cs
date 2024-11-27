using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("JobListings")]
    public class JobListingsController : Controller
    {
        public IActionResult JobListings()
        {
            ViewBag.IsJobListingsPage = true;
            return View();
        }
    }
}