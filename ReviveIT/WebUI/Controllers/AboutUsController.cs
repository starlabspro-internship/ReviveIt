using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("AboutUs")]
    public class AboutUsController : Controller
    {
        public IActionResult AboutUs()
        {
            ViewBag.IsAboutPage = true;
            return View();
        }
    }
}
