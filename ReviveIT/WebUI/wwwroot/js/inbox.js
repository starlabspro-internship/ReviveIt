// Scroll the chat to the bottom
document.addEventListener("DOMContentLoaded", function () {
    const chatMessages = document.getElementById('chat-messages');
    if (chatMessages) {
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }
});