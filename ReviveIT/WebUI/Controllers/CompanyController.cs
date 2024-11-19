using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//[Authorize(Roles = "Company")] - for authorize and ViewBag for testing

[AllowAnonymous]
[Route("Company")]
public class CompanyController : Controller
{
    [HttpGet("Index")]
    public IActionResult Index()
    {
        ViewBag.Role = "Company";
        return View();
    }

    [HttpGet("Inbox")]
    public IActionResult Inbox()
    {
        ViewBag.Role = "Company";
        return View();
    }

    [HttpGet("Myaccount")]
    public IActionResult Myaccount()
    {
        ViewBag.Role = "Company";
        return View();
    }
}