using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SendMessage(string senderId, string receiverId, string message)
        {
            var chatSession = _context.ChatSessions.FirstOrDefault(cs =>
                (cs.TechnicianId == senderId && cs.CustomerId == receiverId) ||
                (cs.TechnicianId == receiverId && cs.CustomerId == senderId));

            if (chatSession == null)
            {
                chatSession = new ChatSession
                {
                    TechnicianId = senderId,
                    CustomerId = receiverId,
                    StartTime = DateTime.UtcNow
                };
                _context.ChatSessions.Add(chatSession);
                _context.SaveChanges();
            }

            var chatMessage = new Messages
            {
                ChatSessionId = chatSession.ChatSessionId,
                SenderID = senderId,
                RecipientID = receiverId,
                MessageContent = message,
                Timestamp = DateTime.UtcNow,
                Viewed = false
            };

            _context.Messages.Add(chatMessage);
            _context.SaveChanges();
        }

        public IEnumerable<ChatSession> GetChatSessions(string userId)
        {
            return _context.ChatSessions
                .Where(cs => cs.TechnicianId == userId || cs.CustomerId == userId)
                .Include(cs => cs.Messages)
                .Include(cs => cs.Technician)
                .Include(cs => cs.Customer)
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

        public void MarkMessageAsRead(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                message.Viewed = true;
                _context.SaveChanges();
            }
        }
    }
}
