document.addEventListener("DOMContentLoaded", function () {
    const searchEmailInput = document.getElementById('searchEmail');
    const userList = document.getElementById('userList');
    const chatMessages = document.getElementById('chat-messages');
    const chatMessageInput = document.getElementById('chatMessage');
    const sendButton = document.getElementById('sendButton');
    const userInfo = document.getElementById('user-info');

    let currentRecipientId = null;
    let currentSessionId = null;
    let recipientFullName = "";
    let recipientEmail = "";
    let currentUser = null;

    fetchCurrentUser();
    fetchChatSessions();

    function getTokenFromCookies() {
        const cookies = document.cookie.split(';');
        for (let cookie of cookies) {
            const [name, value] = cookie.trim().split('=');
            if (name === 'jwtToken') return decodeURIComponent(value);
        }
        return null;
    }

    async function fetchCurrentUser() {
        try {
            const response = await fetch('/api/account/currentUser', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                }
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            currentUser = await response.json();
            fetchChatSessions();
        } catch (error) { }
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.start().catch(function (err) { });

    connection.on("ReceiveMessage", function (senderEmail, message) {
        if (currentUser) {
            appendMessage(message, senderEmail === currentUser.email);
        }
    });

    async function fetchChatSessions() {
        try {
            const response = await fetch('/Chat/GetChatSessions', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                }
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            const sessions = await response.json();
            userList.innerHTML = '';
            if (sessions.length === 0) {
                userList.innerHTML = '<p>No chat sessions found.</p>';
            } else {
                sessions.forEach(function (session) {
                    let user;
                    if (currentUser.id === session.customerId) {
                        user = session.technician;
                    } else if (currentUser.id === session.technicianId) {
                        user = session.customer;
                    }
                    if (user) {
                        addUserToList(user, session.chatSessionId);
                    }
                });
            }
        } catch (error) { }
    }

    function addUserToList(user, chatSessionId) {
        const userElement = document.createElement('div');
        userElement.classList.add('chat-session', 'list-group-item', 'list-group-item-action', 'border-0');
        userElement.innerHTML = `
        <div class="d-flex align-items-start">
            <div class="rounded-circle mr-1" style="width: 40px; height: 40px; background-color: #ddd;"></div>
            <div class="flex-grow-1 ml-3">
                ${user.fullName} (${user.email})
                <div class="small"><span class="fas fa-circle chat-online"></span> Online</div>
            </div>
        </div>
    `;
        userElement.dataset.id = user.id;
        userElement.dataset.sessionId = chatSessionId;
        userElement.addEventListener('click', function () {
            currentSessionId = chatSessionId;
            currentRecipientId = user.id;
            loadChatMessages(currentSessionId);
            displayUserInfo(user);
        });
        userList.appendChild(userElement);
    }

    async function loadChatMessages(sessionId) {
        try {
            const response = await fetch(`/Chat/ChatHistory/${sessionId}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                }
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            const messages = await response.json();
            chatMessages.innerHTML = '';
            if (messages.length === 0) {
                chatMessages.innerHTML = '<p>No messages found for this session.</p>';
            } else {
                messages.forEach(function (message) {
                    if (currentUser) {
                        appendMessage(message.messageContent, message.senderID === currentUser.id);
                    }
                });
            }
        } catch (error) {
            chatMessages.innerHTML = '<p>Error loading messages.</p>';
        }
    }

    function appendMessage(message, isSender) {
        const messageElement = document.createElement('div');
        messageElement.classList.add('chat-message');
        messageElement.classList.add(isSender ? 'chat-message-right' : 'chat-message-left');
        messageElement.innerHTML = `<div class="message-content">${message}</div>`;
        chatMessages.appendChild(messageElement);
        scrollToBottom();
    }

    function scrollToBottom() {
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }

    sendButton.addEventListener('click', async function () {
        const messageContent = chatMessageInput.value;
        if (!messageContent) return;
        if (!currentRecipientId) {
            alert("Please select a user to chat with.");
            return;
        }

        const sendMessagePayload = {
            RecipientID: currentRecipientId,
            Message: messageContent
        };

        try {
            const response = await fetch('/Chat/SendMessage', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                },
                body: JSON.stringify(sendMessagePayload)
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            chatMessageInput.value = '';
            connection.invoke("SendMessageToUser", currentRecipientId, messageContent).catch(function (err) { });
            appendMessage(messageContent, true);

            const existingChatSession = userList.querySelector(`.chat-session[data-session-id='${currentSessionId}']`);
            if (!existingChatSession) {
                addUserToList({ id: currentRecipientId, fullName: recipientFullName, email: recipientEmail }, currentSessionId);
            }
        } catch (error) { }
    });

    searchEmailInput.addEventListener('input', async function () {
        const email = searchEmailInput.value;
        if (!currentUser) {
            return;
        }
        try {
            const response = await fetch(`/Chat/SearchUsers?email=${email}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                }
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            const users = await response.json();
            userList.innerHTML = '';
            if (users.length === 0) {
                userList.innerHTML = '<p>No users found.</p>';
            } else {
                users.forEach(function (user) {
                    const userElement = document.createElement('div');
                    userElement.classList.add('user', 'list-group-item', 'list-group-item-action', 'border-0');
                    userElement.innerHTML = `
                        <div class="d-flex align-items-start">
                            <div class="rounded-circle mr-1" style="width: 40px; height: 40px; background-color: #ddd;"></div>
                            <div class="flex-grow-1 ml-3">
                                                             ${user.fullName} (${user.email})
                                <div class="small"><span class="fas fa-circle chat-online"></span> Online</div>
                            </div>
                        </div>
                    `;
                    userElement.dataset.id = user.id;
                    userElement.addEventListener('click', function () {
                        if (!currentUser) {
                            return;
                        }
                        currentRecipientId = user.id;
                        recipientFullName = user.fullName;
                        recipientEmail = user.email;
                        userElement.classList.add('selected');
                        displayUserInfo(user);
                        startChatWithUser(user.id);
                    });
                    userList.appendChild(userElement);
                });
            }
        } catch (error) { }
    });
    function displayUserInfo(user) {
        userInfo.innerHTML = `
            <div>
                <div class="fullname">${user.fullName}</div>
                <div class="email">${user.email}</div>
            </div>
        `;
        recipientFullName = user.fullName;
        recipientEmail = user.email;
    }

    async function startChatWithUser(userId) {
        try {
            const response = await fetch(`/Chat/StartChatWithUser/${userId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getTokenFromCookies()}`
                }
            });
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            const sessionId = await response.json();
            currentSessionId = sessionId;
            loadChatMessages(currentSessionId);
            fetchChatSessions();
        } catch (error) { }
    }
});