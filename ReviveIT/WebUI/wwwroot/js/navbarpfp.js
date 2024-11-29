const apiUrlNavBar = "https://localhost:7018/api/ProfileUpdate";

function getCookie(name) {
    const cookies = document.cookie.split("; ");
    for (const cookie of cookies) {
        const [key, value] = cookie.split("=");
        if (key === name) {
            return decodeURIComponent(value);
        }
    }
    return null;
}

async function fetchNavbarProfilePicture() {
    try {
        const response = await fetch(`${apiUrlNavBar}/get`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            const navbarProfilePicture = document.getElementById('navbarProfilePicture');

            if (data.profilePictureUrl) {
                navbarProfilePicture.src = data.profilePictureUrl;
            } else {
                navbarProfilePicture.src = "https://via.placeholder.com/150";
            }
        } else {
            console.error("Failed to fetch profile picture for navbar:", response.status);
            const navbarProfilePicture = document.getElementById('navbarProfilePicture');
            navbarProfilePicture.src = "https://via.placeholder.com/150";
        }
    } catch (error) {
        console.error("Error fetching navbar profile picture:", error);
        const navbarProfilePicture = document.getElementById('navbarProfilePicture');
        navbarProfilePicture.src = "https://via.placeholder.com/150";
    }
}


document.addEventListener('DOMContentLoaded', () => {
    fetchNavbarProfilePicture();
});
