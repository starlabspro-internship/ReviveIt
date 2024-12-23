function getCookie(name) {
    const cookies = document.cookie.split("; ");
    const cookie = cookies.find((cookie) => cookie.startsWith(name + "="));
    return cookie ? decodeURIComponent(cookie.split("=")[1]) : null;
}

async function fetchNavbarProfilePicture() {
    const navbarProfilePicture = document.getElementById("navbarProfilePicture");
    try {
        const response = await fetch(`/ProfileUpdate/api/get`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            navbarProfilePicture.src = data.profilePictureUrl;

        }
    } catch (error) {
        navbarProfilePicture.src = "/images/defaultProfilePicture.png";
    }
}

document.addEventListener("DOMContentLoaded", fetchNavbarProfilePicture);