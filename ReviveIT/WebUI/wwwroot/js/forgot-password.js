document.addEventListener("DOMContentLoaded", function () {
    const forgotPasswordForm = document.getElementById("forgotPasswordForm");
    const loginRedirectLink = document.getElementById("loginRedirect");

    const spinner = document.createElement("div");
    spinner.className = "loader";
    spinner.style.display = "none";
    forgotPasswordForm.parentElement.appendChild(spinner);

    forgotPasswordForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        const email = document.getElementById("email").value.trim();

        if (!email || !validateEmail(email)) {
            alert("Please enter a valid email address.");
            return;
        }

        forgotPasswordForm.style.display = "none";
        if (loginRedirectLink) loginRedirectLink.style.display = "none";
        spinner.style.display = "block";

        try {
            const response = await fetch("/api/accounts/forgot-password", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email: email }),
            });

            spinner.style.display = "none";

            if (response.ok) {
                const messageContainer = document.createElement("div");
                messageContainer.className = "alert alert-success text-center";
                messageContainer.style.marginTop = "20px";
                messageContainer.innerHTML = `
                    <p>A reset link has been sent to your email. Please check your inbox.</p>
                `;
                forgotPasswordForm.parentElement.appendChild(messageContainer);
            } else {
                const errorData = await response.json();
                console.error("Error:", errorData);
                alert(errorData.message || "Failed to send reset link. Please try again.");

                forgotPasswordForm.style.display = "block";
                if (loginRedirectLink) loginRedirectLink.style.display = "block";
            }
        } catch (error) {
            console.error("Error:", error);
            alert("An unexpected error occurred.");

            forgotPasswordForm.style.display = "block";
            if (loginRedirectLink) loginRedirectLink.style.display = "block";
            spinner.style.display = "none";
        }
    });

    function validateEmail(email) {
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    }
});
