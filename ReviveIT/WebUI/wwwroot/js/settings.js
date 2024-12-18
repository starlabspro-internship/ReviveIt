function showSection(sectionId) {
    document.getElementById('accountManagementSection').style.display = 'none';
    document.getElementById('languageSection').style.display = 'none';
    document.getElementById(sectionId).style.display = 'block';
}

document.getElementById('changePasswordForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const currentPassword = document.getElementById('currentPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    const messageDiv = document.getElementById('changePasswordMessage');
    messageDiv.innerHTML = '';

    if (newPassword !== confirmPassword) {
        messageDiv.innerHTML = '<div class="alert alert-danger">New passwords do not match.</div>';
        return;
    }

    try {
        const response = await fetch('/api/accounts/change-password', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${getTokenFromCookies()}`
            },
            body: JSON.stringify({
            currentPassword: currentPassword,
            newPassword: newPassword,
            confirmPassword: confirmPassword
            })
        });

        const data = await response.json();

        if (response.ok) {
            messageDiv.innerHTML = '<div class="alert alert-success">' + data.message + '</div>';
            document.getElementById('changePasswordForm').reset();
            clearAuthCookies();
            setTimeout(() => {
                window.location.href = '/login';
            }, 2000);
        }else {
            const errors = data.errors || [data.message];
            messageDiv.innerHTML = '<div class="alert alert-danger">' + errors.join('<br>') + '</div>';
        }
    } catch (error) {
        console.error('Error changing password:', error);
        messageDiv.innerHTML = '<div class="alert alert-danger">An unexpected error occurred. Please try again.</div>';
        }
    });

function getTokenFromCookies() {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
            const [name, value] = cookie.trim().split('=');
    if (name === 'jwtToken') return decodeURIComponent(value);
        }
    return null;
}

function clearAuthCookies() {
    document.cookie = 'jwtToken=; Max-Age=0; path=/; Secure; SameSite=Strict';
    document.cookie = 'refreshToken=; Max-Age=0; path=/; Secure; SameSite=Strict';
}