using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PunetController : Controller
    {
        public IActionResult Punet()
        {
            ViewBag.IsPunetPage = true;
            return View();
        }
    }
}
