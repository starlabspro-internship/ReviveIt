using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
   
    public class CustomerController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {

            ViewBag.Role = "Customer";

            return View();
        }
        [AllowAnonymous]
        public IActionResult TechniciansCompanies()
        {
            ViewBag.Role = "Customer";
            return View();
        }
        [AllowAnonymous]

        public IActionResult PostJob()
        {
            ViewBag.Role = "Customer";
            return View();
        }
        [AllowAnonymous]
        public IActionResult Inbox()
        {
            ViewBag.Role = "Customer";
            return View();
        }

        public IActionResult Myaccount()
        {
            ViewBag.Role = "Customer";
            return View();
        }
    }
}
