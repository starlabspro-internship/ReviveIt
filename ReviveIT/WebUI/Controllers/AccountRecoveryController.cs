﻿using Application.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountRecoveryController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly TokenHelper _tokenHelper;

        public AccountRecoveryController(UserManager<Users> userManager, TokenHelper tokenHelper)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        [HttpGet("forgot-password")]
        public IActionResult forgotPassword()
        {
            return View();
        }

        [HttpGet("reset-password")]
        public IActionResult resetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            ViewData["Token"] = token;
            return View();
        }
    }
}