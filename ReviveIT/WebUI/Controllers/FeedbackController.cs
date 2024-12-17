using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

[Route("feedback")]
public class FeedbackController : Controller
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<Users> _userManager;

    public FeedbackController(IApplicationDbContext context, UserManager<Users> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    [Route("submit")]
    public IActionResult Submit()
    {
        return View("UserFeedback", new Feedback());
    }

    [Authorize]
    [HttpPost]
    [Route("submit")]
    public async Task<IActionResult> Submit(Feedback model)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                model.UserId = user.Id;
                _context.Users.Attach(user);
                model.User = user;
                ModelState.Remove(nameof(model.User));

                if (ModelState.IsValid)
                {
                    model.Date = DateTime.Now;
                    _context.Feedbacks.Add(model);

                    try
                    {
                        var saveResult = await _context.SaveChangesAsync();
                        if (saveResult > 0)
                        {
                            TempData["Feedback"] = JsonConvert.SerializeObject(model);
                            return RedirectToAction("ThankYou");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Feedback was not saved. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occurred while saving your feedback. Please try again.");
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                }
            }
            else
            {
                ModelState.AddModelError("", "Authenticated user not found. Please try again.");
            }
        }
        else
        {
            ModelState.AddModelError("", "User ID not found. Please log in and try again.");
        }

        return View("UserFeedback", model);
    }

    [HttpGet]
    [Route("thank-you")]
    public IActionResult ThankYou()
    {
        var feedbackJson = TempData["Feedback"]?.ToString();
        var feedback = feedbackJson != null ? JsonConvert.DeserializeObject<Feedback>(feedbackJson) : new Feedback();
        return View(feedback);
    }
}
