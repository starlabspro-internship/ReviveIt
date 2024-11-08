document.addEventListener("DOMContentLoaded", function () {
    const registerForm = document.getElementById("registerForm");

    registerForm.addEventListener("submit", async function (event) {
        event.preventDefault();

        const email = document.getElementById("email").value.trim();
        const password = document.getElementById("password").value;
        const confirmPassword = document.getElementById("confirm_password").value;
        const role = document.getElementById("role").value;
        const name = document.getElementById("name").value.trim();

        const expertise = document.getElementById("expertise")?.value || null;
        const experience = document.getElementById("experience")?.value || null;
        const companyName = document.getElementById("companyName")?.value || null;
        const companyAddress = document.getElementById("companyAddress")?.value || null;

        if (!email || !validateEmail(email)) {
            alert("Please enter a valid email address.");
            return;
        }

        if (!password || !validatePassword(password)) {
            alert("Password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number, and a non-alphanumeric character.");
            return;
        }

        if (password !== confirmPassword) {
            alert("Passwords do not match.");
            return;
        }

        if ((role === "User" || role === "Technician") && !name) {
            alert("Please enter your full name.");
            return;
        }

        if (role === "Technician" && !expertise) {
            alert("Please specify your expertise.");
            return;
        }

        if (role === "Technician" && !experience) {
            alert("Please specify your years of experience.");
            return;
        }

        if (role === "Company" && (!companyName || !companyAddress)) {
            alert("Company name and address are required for Company registration.");
            return;
        }

        const data = {
            email,
            password,
            confirmPassword,
            role,
            name: (role === "User" || role === "Technician") ? name : null,
            expertise: role === "Technician" ? expertise : null,
            experience: role === "Technician" ? experience : null,
            companyName: role === "Company" ? companyName : null,
            companyAddress: role === "Company" ? companyAddress : null
        };

        try {
            const response = await fetch("/api/Accounts/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data),
            });

            const result = await response.json();

            if (result.success) {
                alert(result.message);
                registerForm.reset();
            } else {
                alert(result.message || "Registration failed. Please try again.");
            }
        } catch (error) {
            console.error("Error:", error);
            alert("An unexpected error occurred. Please try again later.");
        }
    });

    function validateEmail(email) {
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    }

    function validatePassword(password) {
        const passwordPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$/;
        return passwordPattern.test(password);
    }
});
