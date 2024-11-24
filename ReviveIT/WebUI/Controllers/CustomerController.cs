using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CustomerController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Role = "Customer";
        return View();
    }
    [Authorize(Roles = "Customer")]
    public IActionResult TechniciansCompanies()
    {
        ViewBag.Role = "Customer";
        return View();
    }
    [Authorize(Roles = "Customer")]
    public IActionResult PostJob()
    {
        ViewBag.Role = "Customer";
        return View();
    }
    [Authorize(Roles = "Customer")]
    public IActionResult Inbox()
    {
        ViewBag.Role = "Customer";
        return View();
    }
    [Authorize(Roles = "Customer")]
    public IActionResult Myaccount()
    {
        ViewBag.Role = "Customer";
        return View();
    }
}
