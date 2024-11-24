using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CompanyController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Role = "Company";
        return View();
    }
    [Authorize(Roles = "Company")]
    public IActionResult Inbox()
    {
        ViewBag.Role = "Company";
        return View();
    }
    [Authorize(Roles = "Company")]
    public IActionResult Myaccount()
    {
        ViewBag.Role = "Company";
        return View();
    }
}
