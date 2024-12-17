using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;

[Authorize]
public class ChatController : Controller
{
    private readonly IMessageService _messageService;
    private readonly UserManager<Users> _userManager;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IMessageService messageService, UserManager<Users> userManager, IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _userManager = userManager;
        _hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult Inbox()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var sessions = _messageService.GetChatSessions(userId);
        return View(sessions);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string receiverId, string message)
    {
        var senderId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(senderId))
        {
            return Unauthorized("User ID not found in token.");
        }

        _messageService.SendMessage(senderId, receiverId, message);

        var sender = await _userManager.FindByIdAsync(senderId);
        await _hubContext.Clients.User(receiverId).SendAsync("ReceiveMessage", sender.Email, message);

        return RedirectToAction("Inbox");
    }

    [HttpPost]
    public IActionResult MarkMessageAsRead(int messageId)
    {
        _messageService.MarkMessageAsRead(messageId);
        return RedirectToAction("Inbox");
    }

    [HttpGet]
    public IActionResult ChatHistory(int sessionId)
    {
        var messages = _messageService.GetChatHistory(sessionId);
        return View(messages);
    }

    [HttpGet]
    public IActionResult SearchUsers(string email)
    {
        var users = _userManager.Users
            .Where(u => u.Email.Contains(email))
            .Select(u => new { u.Id, u.Email })
            .ToList();
        return Json(users);
    }
}
