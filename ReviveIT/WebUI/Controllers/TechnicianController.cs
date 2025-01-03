﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    [Route("Technician")]
    public class TechnicianController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            ViewBag.Role = "Technician";
            return View();
        }

        [HttpGet("PostedJobs")]
        public IActionResult PostedJobs()
        {
            ViewBag.Role = "Technician";
            return View();
        }

        [HttpGet("Inbox")]
        public IActionResult Inbox()
        {
            ViewBag.Role = "Technician";
            return View();
        }

        [HttpGet("Myaccount")]
        public IActionResult Myaccount()
        {
            ViewBag.Role = "Technician";
            return View();
        }
    }
}