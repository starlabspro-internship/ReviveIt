$(document).ready(function () {
    function isLoginPage() {
        return window.location.pathname.toLowerCase() === "/login/login";
    }

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

                const userRole = response.role;

                switch (userRole) {
                    case "Technician":
                        window.location.href = "/Technician/Index";
                        break;
                    case "Company":
                        window.location.href = "/Company/Index";
                        break;
                    case "Customer":
                        window.location.href = "/Customer/Index";
                        break;
                    default:
                        window.location.href = "/home";
                        break;
                }
            },
            error: function (xhr) {
                var errorMessage = xhr.responseJSON?.message || "An unexpected error occurred. Please try again later.";
                alert(errorMessage);
            }
        });
    });

    function makeAuthenticatedRequest(url, method = "GET") {
        const token = localStorage.getItem("jwtToken");

        if (!token) {
            window.location.href = "/logIn/LogIn";
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
                window.history.pushState({}, "", url);
            },
            error: function (xhr) {
                alert("Access denied. Please login again.");
                window.location.href = "/logIn/LogIn";
            }
        });
    }

    $.ajaxSetup({
        beforeSend: function (xhr) {
            const token = localStorage.getItem("jwtToken");
            if (token) {
                xhr.setRequestHeader("Authorization", `Bearer ${token}`);
            }
        }
    });

    $(document).off("click", ".auth-link").on("click", ".auth-link", function (e) {
        e.preventDefault();
        const url = $(this).attr("href");
        makeAuthenticatedRequest(url);
    });

    function checkAuthentication() {
        const token = localStorage.getItem("jwtToken");

        if (!token && !isLoginPage()) {
            window.location.href = "/logIn/LogIn";
        }
    }

    checkAuthentication();
});
