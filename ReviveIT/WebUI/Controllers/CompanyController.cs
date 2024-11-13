using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{

    public class CompanyController : Controller
    {
            public IActionResult Index()
        {
            ViewBag.Role = "Company";
            return View();
        }

            public IActionResult Inbox()
        {
            ViewBag.Role = "Company";
            return View();
       }
    }
}
