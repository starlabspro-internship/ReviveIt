function getCookie(name) {
    const cookies = document.cookie.split("; ");
    const cookie = cookies.find((cookie) => cookie.startsWith(name + "="));
    return cookie ? decodeURIComponent(cookie.split("=")[1]) : null;
}

async function fetchFullName() {
    const navbarFullName = document.getElementById("navbarFullName");
    try {
        const response = await fetch("/ProfileUpdate/api/info?type=fullname", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            navbarFullName.textContent = data.fullName || "";
        }
    } catch (error) {
        console.error("Error fetching name:", error);
    }
}

async function fetchUserRole() {
    const statusText = document.getElementById("statusText");
    try {
        const response = await fetch("/ProfileUpdate/api/info?type=role", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            const roles = {
                Admin: "Admin",
                Customer: "Customer",
                Technician: "Technician",
                Company: "Company",
            };
            statusText.textContent = roles[data.role] || "Unknown";
        } else {
            statusText.textContent = "Error";
        }
    } catch (error) {
        statusText.textContent = "Error";
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchFullName();
    fetchUserRole();
});