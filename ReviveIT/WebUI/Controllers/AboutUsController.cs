using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("AboutUs")]
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            ViewBag.IsAboutPage = true;
            ViewBag.TextUpdated = false; 
            return View();
        }

        [HttpPost]
        public IActionResult UpdateText()
        {
            ViewBag.IsAboutPage = true;
            ViewBag.TextUpdated = true; 
            return View("AboutUs");
        }
    }
}
