using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    
    public class TechnicianController : Controller
{
        
        public IActionResult Index()
    {
            ViewBag.Role = "Technician";
            return View();
    }
 
        public IActionResult PostedJobs1()
    {
            ViewBag.Role = "Technician";
            return View();
    }
       
        public IActionResult Inbox()
    {
            ViewBag.Role = "Technician";
            return View();
    }
       
        public IActionResult MyAccount()
    {
            ViewBag.Role = "Technician";
            return View();
    }
}
}
