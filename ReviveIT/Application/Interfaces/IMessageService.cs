using Domain.Entities;

public interface IMessageService
{
    IEnumerable<ChatSession> GetChatSessions(string userId);
    ChatSession GetChatSession(string userId, string otherUserId); 
    IEnumerable<Messages> GetChatHistory(int sessionId);
    Task SendMessageAsync(string senderId, string recipientId, string message );
    void MarkMessageAsRead(int messageId);
    Users GetUserByEmail(string email);
    int GetUnseenMessagesCountForUser(string userId);
}