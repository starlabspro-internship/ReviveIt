using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class RrethNeshController : Controller
    {
        public IActionResult RrethNesh()
        {
            ViewBag.IsPunetPage = true;
            return View();
        }

    }
}
