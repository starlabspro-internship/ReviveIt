$(document).ready(function () {
    // Function to check if the user is on the login page
    function isLoginPage() {
        return window.location.pathname === "/LogIn/LogIn";
    }

    // Login form submission
    $("#loginForm").submit(function (event) {
        event.preventDefault();

        var loginData = {
            email: $("#email").val(),
            password: $("#password").val()
        };

        $.ajax({
            url: "/api/accounts/login",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(loginData),
            success: function (response) {
                localStorage.setItem("jwtToken", response.token);
                console.log("Login successful!");

                // Decode the JWT token to get user role
                const token = response.token;
                try {
                    const payload = KJUR.jws.JWS.readSafeJSONString(b64utoutf8(token.split(".")[1]));
                    console.log("Token Payload: ", payload);

                    // Retrieve the role from the token
                    const userRole = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                    console.log("User Role: ", userRole);

                    // Redirect based on user role
                    if (userRole === "Technician") {
                        console.log("Redirecting to Technician/Index");
                        window.location.href = "/Technician/Index";
                    } else if (userRole === "Company") {
                        console.log("Redirecting to Company/Index");
                        window.location.href = "/Company/Index";
                    } else if (userRole === "Customer") {
                        console.log("Redirecting to Customer/Index");
                        window.location.href = "/Customer/Index";
                    } else {
                        console.log("Redirecting to Home");
                        window.location.href = "/home"; // k
                    }
                } catch (error) {
                    console.error("Error decoding token: ", error);
                    alert("Failed to decode token. Please try again.");
                }
            },
            error: function (xhr) {
                var errorMessage = xhr.responseJSON?.message || "An unexpected error occurred. Please try again later.";
                console.error("Login error: ", errorMessage);
            }
        });
    });

    // Function to make authenticated AJAX requests
    function makeAuthenticatedRequest(url, method = "GET") {
        const token = localStorage.getItem("jwtToken");

        if (!token) {
            console.error("User is not authenticated");
            return;
        }

        $.ajax({
            url: url,
            type: method,
            headers: {
                Authorization: `Bearer ${token}`
            },
            success: function (data) {
                $("body").html(data);
            },
            error: function (xhr) {
                console.error("Access denied. Please login again.");
            }
        });
    }

    $(document).on("click", ".auth-link", function (e) {
        e.preventDefault();
        const url = $(this).attr("href");
        makeAuthenticatedRequest(url);
    });

    function checkAuthentication() {
        const token = localStorage.getItem("jwtToken");

        if (!token && !isLoginPage()) {
            console.error("User is not authenticated, redirecting to login");
            window.location.href = "/LogIn/LogIn";
        }
    }

    checkAuthentication();

});
