﻿document.addEventListener("DOMContentLoaded", function () {
    const registerForm = document.getElementById("registerForm");
    const loginRedirectLink = document.getElementById("logInRedirect");

    registerForm.parentElement.classList.add("register-container");

    const spinner = document.createElement("div");
    spinner.className = "loader";
    registerForm.parentElement.appendChild(spinner);

    const roleMapping = {
        "Customer": 1,
        "Technician": 2,
        "Company": 3
    };

    registerForm.addEventListener("submit", async function (event) {
        event.preventDefault();

        const email = document.getElementById("email").value.trim();
        const password = document.getElementById("password").value;
        const confirmPassword = document.getElementById("confirm_password").value;
        const role = roleMapping[document.getElementById("role").value];
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
            alert(
                "Password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number, and a non-alphanumeric character."
            );
            return;
        }

        if (password !== confirmPassword) {
            alert("Passwords do not match.");
            return;
        }

        if ((role === 1 || role === 2) && !name) {
            alert("Please enter your full name.");
            return;
        }

        if (role === 2 && !expertise) {
            alert("Please specify your expertise.");
            return;
        }

        if (role === 2 && !experience) {
            alert("Please specify your years of experience.");
            return;
        }

        if (role === 3 && (!companyName || !companyAddress)) {
            alert("Company name and address are required for Company registration.");
            return;
        }

        const data = {
            email,
            password,
            confirmPassword,
            role,
            name: role === 1 || role === 2 ? name : null,
            expertise: role === 2 ? expertise : null,
            experience: role === 2 ? experience : null,
            companyName: role === 3 ? companyName : null,
            companyAddress: role === 3 ? companyAddress : null
        };

        registerForm.style.display = "none";
        if (loginRedirectLink) loginRedirectLink.style.display = "none";
        spinner.style.display = "block";

        try {
            const response = await fetch("/api/Accounts/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data),
            });

            const result = await response.json();

            spinner.style.display = "none";

            if (result.success) {
                const messageContainer = document.createElement("div");
                messageContainer.className = "alert alert-success";
                messageContainer.innerHTML = `
                    <p>Registration was successful! A confirmation link has been sent to your email.</p>
                    <p>Please check your email to verify your account.</p>
                    <a href="/LogIn/LogIn" class="btn btn-primary mt-3" style="display: block; margin: 20px auto; text-align: center;">Go to Login</a>
                `;
                registerForm.parentElement.appendChild(messageContainer);
            } else {
                registerForm.style.display = "block";
                if (loginRedirectLink) loginRedirectLink.style.display = "block";
                alert(result.message || "Registration failed. Please try again.");
            }
        } catch (error) {
            console.error("Error:", error);
            alert("An unexpected error occurred. Please try again later.");

            registerForm.style.display = "block";
            if (loginRedirectLink) loginRedirectLink.style.display = "block";
            spinner.style.display = "none";
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