const apiUrlNavBar = "https://reviveit.devops99.pro/api/ProfileUpdate";

function getCookie(name) {
    const cookies = document.cookie.split("; ");
    const cookie = cookies.find(cookie => cookie.startsWith(name + "="));
    return cookie ? decodeURIComponent(cookie.split("=")[1]) : null;
}

async function fetchNavbarProfilePicture() {
    const navbarProfilePicture = document.getElementById('navbarProfilePicture');
    const placeholderUrl = "https://via.placeholder.com/150";

    try {
        const response = await fetch(`${apiUrlNavBar}/get`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            navbarProfilePicture.src = data.profilePictureUrl || placeholderUrl;
        } else {
            navbarProfilePicture.src = placeholderUrl;
        }
    } catch (error) {
        navbarProfilePicture.src = placeholderUrl;
    }
}

document.addEventListener('DOMContentLoaded', fetchNavbarProfilePicture);
