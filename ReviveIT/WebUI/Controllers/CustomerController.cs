using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize(Roles = "Customer)] - for authorize
[AllowAnonymous]
[Route("Customer")]
public class CustomerController : Controller
{
    [HttpGet("Index")]
    public IActionResult Index()
    {
        ViewBag.Role = "Customer";
        return View();
    }

    [HttpGet("TechniciansCompanies")]
    public IActionResult TechniciansCompanies()
    {
        ViewBag.Role = "Customer";
        return View();
    }

    [HttpGet("PostJob")]
    public IActionResult PostJob()
    {
        ViewBag.Role = "Customer";
        return View();
    }

    [HttpGet("Inbox")]
    public IActionResult Inbox()
    {
        ViewBag.Role = "Customer";
        return View();
    }

    [HttpGet("Myaccount")]
    public IActionResult Myaccount()
    {
        ViewBag.Role = "Customer";
        return View();
    }
}