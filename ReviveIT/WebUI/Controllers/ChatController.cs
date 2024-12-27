using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebUI.Models;

[Authorize]
public class ChatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMessageService _messageService;
    private readonly UserManager<Users> _userManager;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(
        IMessageService messageService,
        UserManager<Users> userManager,
        IHubContext<ChatHub> hubContext,
        ApplicationDbContext context)
    {
        _messageService = messageService;
        _userManager = userManager;
        _hubContext = hubContext;
        _context = context;
    }

    [HttpGet("/Chat/Inbox")]
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

    [HttpGet("/Chat/ChatHistory/{sessionId}")]
    public IActionResult ChatHistory(int sessionId)
    {
        var messages = _context.Messages
                              .Where(m => m.ChatSessionId == sessionId)
                              .OrderBy(m => m.Timestamp)
                              .ToList();
        return Json(messages);
    }

    [HttpGet("/Chat/GetChatSessions")]
    public IActionResult GetChatSessions()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var sessions = _messageService.GetChatSessions(userId);
        return Ok(sessions);
    }

    [HttpGet("/Chat/SearchUsers")]
    public IActionResult SearchUsers(string email)
    {
        var users = _userManager.Users
            .Where(u => u.Email.Contains(email))
            .Select(u => new { u.Id, u.Email, u.FullName })
            .ToList();

        return Json(users);
    }

    [HttpGet("/Chat/GetUserByEmail")]
    public IActionResult GetUserByEmail(string email)
    {
        var user = _messageService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(new { user.Id, user.Email, user.FullName });
    }

    [HttpPost("/Chat/StartChatWithUser/{userId}")]
    public IActionResult StartChatWithUser(string userId)
    {
        var currentUserId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var recipient = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (recipient == null)
        {
            return NotFound("Recipient not found");
        }

        var chatSession = _messageService.GetChatSessions(currentUserId)
            .FirstOrDefault(cs =>
                (cs.CustomerId == currentUserId && (cs.TechnicianId == userId || cs.CompanyId == userId)) ||
                (cs.CustomerId == userId && (cs.TechnicianId == currentUserId || cs.CompanyId == currentUserId)) ||
                (cs.TechnicianId == currentUserId && cs.CustomerId == userId) ||
                (cs.TechnicianId == userId && cs.CustomerId == currentUserId) ||
                (cs.CompanyId == currentUserId && cs.CustomerId == userId) ||
                (cs.CompanyId == userId && cs.CustomerId == currentUserId));

        if (chatSession == null)
        {
            chatSession = new ChatSession
            {
                CustomerId = currentUserId,
                TechnicianId = recipient.Role == UserRole.Technician ? userId : null,
                CompanyId = recipient.Role == UserRole.Company ? userId : null,
                StartTime = DateTime.UtcNow
            };

            _context.ChatSessions.Add(chatSession);
            _context.SaveChanges();
        }

        return Json(chatSession.ChatSessionId);
    }

    [HttpGet("/api/account/currentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(new { user.Id, user.Email, user.FullName });
    }

    [HttpPost("/Chat/SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageModel model)
    {
        var senderId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(senderId))
        {
            return Unauthorized("User ID not found in token.");
        }

        if (string.IsNullOrEmpty(model.RecipientID))
        {
            return BadRequest("Recipient ID is required.");
        }

        await _messageService.SendMessageAsync(senderId, model.RecipientID, model.Message);
        var sender = await _userManager.FindByIdAsync(senderId);
        if (sender == null)
        {
            return NotFound("Sender not found.");
        }

        await _hubContext.Clients.User(model.RecipientID).SendAsync("ReceiveMessage", sender.Email, model.Message);

        return Ok();
    }

    [HttpPost("/Chat/MarkAsRead")]
    public IActionResult MarkAsRead(int messageId)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        _messageService.MarkMessageAsRead(messageId);
        return Ok();
    }

    [HttpGet("/Chat/GetUnseenMessagesCount")]
    public IActionResult GetUnseenMessagesCount()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var unseenCount = _messageService.GetUnseenMessagesCountForUser(userId);
        return Ok(new { UnseenMessagesCount = unseenCount });
    }

    [HttpGet("/api/messages/unseen-count")]
    public IActionResult GetUnseenMessagesCount(string userId)
    {
        var count = _messageService.GetUnseenMessagesCountForUser(userId);
        return Ok(count);
    }
}