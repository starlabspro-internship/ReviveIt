document.addEventListener("DOMContentLoaded", () => {
    const logoutButton = document.getElementById("logoutButton");
    if (logoutButton) {
        logoutButton.addEventListener("click", async (event) => {
            event.preventDefault();
            try {
                const response = await fetch('/api/accounts/logout', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    credentials: 'include'
                });

                if (response.ok) {
                    alert("Successfully logged out");
                    window.location.href = '/login';
                } else {
                    const errorText = await response.text();
                    alert(`Error: ${errorText || 'Unknown error occurred'}`);
                }
            } catch (error) {
                console.error("Error during logout:", error);
                alert("An error occurred. Please try again.");
            }
        });
    } else {
        console.error("Logout button not found in DOM");
    }
});
