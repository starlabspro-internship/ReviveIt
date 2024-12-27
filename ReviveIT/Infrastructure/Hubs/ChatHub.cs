using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities; // Ensure the namespace includes your entities

namespace Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userEmail = Context.UserIdentifier;
            _logger.LogInformation($"User connected with Email: {userEmail}, ConnectionId: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userEmail = Context.UserIdentifier;
            _logger.LogInformation($"User disconnected with Email: {userEmail}, ConnectionId: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToUser(string recipientId, string message)
        {
            var senderEmail = Context.UserIdentifier;
            if (string.IsNullOrEmpty(senderEmail))
            {
                _logger.LogError("Sender email is null or empty.");
                return;
            }

            if (string.IsNullOrEmpty(recipientId))
            {
                _logger.LogError("Recipient ID is null or empty.");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                _logger.LogError("Message content is null or empty.");
                return;
            }

            _logger.LogInformation($"Preparing to send message from {senderEmail} to {recipientId}");

            try
            {
                // Log connection state before sending the message
                _logger.LogInformation($"Connection state before sending: {Context.ConnectionId}, user: {senderEmail}");

                // Attempt to send the message
                await Clients.User(recipientId).SendAsync("ReceiveMessage", senderEmail, message);

                // Log success
                _logger.LogInformation("Message sent successfully.");
            }
            catch (Exception ex)
            {
                // Log any exceptions
                _logger.LogError($"Error sending message from {senderEmail} to {recipientId}: {ex.Message}", ex);
            }
        }

    }

}
