const urlParams = new URLSearchParams(window.location.search);
const token = urlParams.get('token');

document.getElementById("resetPasswordForm").addEventListener("submit", function (event) {
    event.preventDefault();

    const newPassword = document.getElementById("newPassword").value;
    const confirmPassword = document.getElementById("confirmPassword").value;

    if (newPassword !== confirmPassword) {
        alert("New password and confirm password do not match.");
        return;
    }

    const resetPasswordData = {
        Token: token,
        NewPassword: newPassword,
        ConfirmPassword: confirmPassword
    };

    fetch("/api/accounts/reset-password", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(resetPasswordData)
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text); });
            }
            return response.json();
        })
        .then(data => {
            alert("Password reset successful!");
            window.location.href = '/LogIn/LogIn';
        })
        .catch(error => {
            console.error("Error:", error.message);
            alert("An error occurred: " + error.message);
        });
});

