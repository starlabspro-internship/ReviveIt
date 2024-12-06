using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string senderId, string receiverId, string message);
        IEnumerable<ChatSession> GetChatSessions(string userId);
        IEnumerable<Messages> GetChatHistory(int chatSessionId);
        void MarkMessageAsRead(int messageId);
    }
}
