using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class TechnicianController : Controller
{
    public IActionResult Index()
        {
        ViewBag.Role = "Technician";
            return View();
        }
    [Authorize(Roles = "Technician")]
    public IActionResult PostedJobs()
        {
            ViewBag.Role = "Technician";
            return View();
        }
    [Authorize(Roles = "Technician")]
    public IActionResult Inbox()
        {
            ViewBag.Role = "Technician";
            return View();
        }
    [Authorize(Roles = "Technician")]
    public IActionResult Myaccount()
        {
            ViewBag.Role = "Technician";
            return View();
        }
    }
