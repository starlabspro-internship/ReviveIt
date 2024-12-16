    $(document).ready(function () {
        $("#resendForm").submit(function (event) {
            event.preventDefault();
            var email = $("#email").val();

            var resendButton = $("button[type='submit']");
            resendButton.prop("disabled", true);
            var timer = 60;
            resendButton.text(`Wait ${timer} seconds`);

            var countdownInterval = setInterval(function () {
                timer--;
                resendButton.text(`Wait ${timer} seconds`);
                if (timer <= 0) {
                    clearInterval(countdownInterval);
                    resendButton.prop("disabled", false);
                    resendButton.text("Resend Confirmation Email");
                }
            }, 1000);

            $.ajax({
                url: "/ResendEmailConfirmation/api/resendEmailConfirmation",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({ email: email }),
                success: function () {
                    alert("Confirmation email resent successfully.");
                },
                error: function () {
                    alert("Failed to resend confirmation email. Please try again later.");
                }
            });
        });
    });