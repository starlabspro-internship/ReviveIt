$(document).ready(function () {
    $('#profileForm').on('submit', function (event) {
        event.preventDefault();

        const phone = $('#phone').val();
        const description = $('#description').val();

        const token = localStorage.getItem('jwtToken');

        if (!token) {
            alert("You are not authenticated. Please log in.");
            return;
        }

        $.ajax({
            url: '/api/CompleteProfileApi/CompleteProfile',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            data: JSON.stringify({ phone: phone, description: description }),
            success: function (response) {
                alert(response.message || "Profile updated successfully!");
                window.location.href = '/';
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText || "An error occurred while updating your profile.");
            }
        });
    });
});