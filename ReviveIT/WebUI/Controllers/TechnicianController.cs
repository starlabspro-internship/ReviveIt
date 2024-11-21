using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class TechnicianController : Controller
{

    public IActionResult Index()
        {
        //var userRole = User.FindFirst(ClaimTypes.Role)?.Value; Console.WriteLine($"User Role: {userRole}");
        ViewBag.Role = "Technician";
            return View();
        }

    [Authorize(Roles = "Technician")]
    public IActionResult PostedJobs()

        {
            ViewBag.Role = "Technician";
            return View();
        }

    public IActionResult Inbox()
        {
            ViewBag.Role = "Technician";
            return View();
        }

        public IActionResult Myaccount()
        {
            ViewBag.Role = "Technician";
            return View();
        }
    }
