﻿$(document).ready(function () {
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
                if (response.isEmailNotConfirmed) {
                    console.log(response.isEmailNotConfirmed);
                    window.location.href = "/ResendEmailConfirmation?email=" + encodeURIComponent(loginData.email);
                } else {
                    alert("Login successful!");
                    window.location.href = "/home";
                }
            },
            error: function (xhr) {
                var errorMessage = xhr.responseJSON?.message || "An unexpected error occurred. Please try again later.";
                alert(errorMessage); 
            }
        });
    });
});
