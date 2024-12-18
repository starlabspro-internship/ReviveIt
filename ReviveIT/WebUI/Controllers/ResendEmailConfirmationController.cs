using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("ResendEmailConfirmation")]
    public class ResendEmailConfirmationController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<Users> _userManager;

        public ResendEmailConfirmationController (IEmailSender emailSender, UserManager<Users> userManager)
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ResendEmailConfirmation(string email)
        {
            ViewBag.Email = email;
            return View();

        }

        [HttpPost("api/resendEmailConfirmation")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendEmailDto request)
        {
            var email = request.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.EmailConfirmed)
            {
                return BadRequest("Invalid request.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userId = user.Id, token }, Request.Scheme);

            await _emailSender.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your email by clicking here: <a href='{confirmationLink}'>link</a>");

            return Ok("Confirmation email sent successfully.");
        }

        public class ResendEmailDto
        {
            public string Email { get; set; }
        }
    }
}
