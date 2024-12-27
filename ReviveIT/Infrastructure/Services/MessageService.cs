using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _context;

    public MessageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public ChatSession GetChatSession(string userId, string otherUserId)
    {
        return _context.ChatSessions.FirstOrDefault(cs =>
            (cs.CustomerId == userId && (cs.TechnicianId == otherUserId || cs.CompanyId == otherUserId)) ||
            (cs.CustomerId == otherUserId && (cs.TechnicianId == userId || cs.CompanyId == userId)) ||
            (cs.TechnicianId == userId && cs.CustomerId == otherUserId) ||
            (cs.TechnicianId == otherUserId && cs.CustomerId == userId) ||
            (cs.CompanyId == userId && cs.CustomerId == otherUserId) ||
            (cs.CompanyId == otherUserId && cs.CustomerId == userId)
        );
    }

    public async Task SendMessageAsync(string senderId, string recipientId, string message)
    {
        var sender = await _context.Users.FindAsync(senderId);
        var recipient = await _context.Users.FindAsync(recipientId);

        if (sender == null || recipient == null)
        {
            throw new Exception("Invalid sender or recipient");
        }

        var recentMessage = await _context.Messages
            .Where(m => m.SenderID == senderId && m.RecipientID == recipientId && m.MessageContent == message)
            .OrderByDescending(m => m.Timestamp)
            .FirstOrDefaultAsync();

        if (recentMessage != null && (DateTime.UtcNow - recentMessage.Timestamp).TotalSeconds < 1)
        {
            return;
        }

        var senderRoles = await _context.UserRoles.Where(ur => ur.UserId == senderId)
                                                  .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, ur.RoleId, r.Name })
                                                  .ToListAsync();
        const string CustomerRoleId = "6433e50a-dc84-42d1-8ed7-59c2290e02c9";
        var isSenderCustomer = senderRoles.Any(ur => ur.RoleId == CustomerRoleId);

        var recipientRoles = await _context.UserRoles.Where(ur => ur.UserId == recipientId)
                                                     .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, ur.RoleId, r.Name })
                                                     .ToListAsync();
        const string TechnicianRoleId = "4585797c-902d-48ea-acdd-412d39c52da1";
        const string CompanyRoleId = "c1f58253-3c40-4699-984e-11dcff9c0408";
        var isRecipientValid = recipientRoles.Any(ur => ur.RoleId == TechnicianRoleId || ur.RoleId == CompanyRoleId);

        var chatSession = _context.ChatSessions.FirstOrDefault(cs =>
            (cs.CustomerId == senderId && (cs.TechnicianId == recipientId || cs.CompanyId == recipientId)) ||
            (cs.CustomerId == recipientId && (cs.TechnicianId == senderId || cs.CompanyId == senderId))
        );

        if (chatSession == null && isSenderCustomer && isRecipientValid)
        {
            chatSession = GetOrCreateChatSession(senderId, recipientRoles.FirstOrDefault(ur => ur.RoleId == TechnicianRoleId)?.UserId, recipientRoles.FirstOrDefault(ur => ur.RoleId == CompanyRoleId)?.UserId);
        }
        else if (chatSession == null)
        {
            throw new Exception("Chat session does not exist or invalid roles.");
        }

        var chatMessage = new Messages
        {
            ChatSessionId = chatSession.ChatSessionId,
            SenderID = senderId,
            RecipientID = recipientId,
            MessageContent = message,
            Timestamp = DateTime.UtcNow,
            Viewed = false
        };

        _context.Messages.Add(chatMessage);
        await _context.SaveChangesAsync();
    }

    private ChatSession GetOrCreateChatSession(string customerId, string technicianId, string companyId)
    {
        var chatSession = _context.ChatSessions.FirstOrDefault(cs =>
            cs.CustomerId == customerId && (cs.TechnicianId == technicianId || cs.CompanyId == companyId))
            ?? new ChatSession
            {
                CustomerId = customerId,
                TechnicianId = technicianId,
                CompanyId = companyId,
                StartTime = DateTime.UtcNow
            };

        if (chatSession.ChatSessionId == 0)
        {
            _context.ChatSessions.Add(chatSession);
            _context.SaveChanges();
        }

        return chatSession;
    }

    public IEnumerable<ChatSession> GetChatSessions(string userId)
    {
        return _context.ChatSessions
            .Include(cs => cs.Customer)
            .Include(cs => cs.Technician)
            .Include(cs => cs.Company)
            .Where(cs => cs.CustomerId == userId || cs.TechnicianId == userId || cs.CompanyId == userId)
            .ToList();
    }

    public IEnumerable<Messages> GetChatHistory(int chatSessionId)
    {
        return _context.Messages
            .Where(m => m.ChatSessionId == chatSessionId)
            .OrderBy(m => m.Timestamp)
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .ToList();
    }

    public int GetUnseenMessagesCount(string userId)
    {
        return _context.Messages.Count(m => m.RecipientID == userId && !m.Viewed);
    }

    public void MarkMessageAsRead(int messageId)
    {
        var message = _context.Messages.Find(messageId);
        if (message != null)
        {
            message.Viewed = true;
            _context.SaveChanges();
        }
    }

    public Users GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public int GetUnseenMessagesCountForUser(string userId)
    {
        var unseenMessages = _context.Messages
            .Where(m => m.RecipientID == userId && !m.Viewed)
            .GroupBy(m => m.SenderID)
            .Select(g => new { g.Key, Count = g.Count() })
            .Count();

        return unseenMessages;
    }
}