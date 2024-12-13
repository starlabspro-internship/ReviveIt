async function fetchFullName() {
    const navbarFullName = document.getElementById('navbarFullName');
    try {
        const response = await fetch('/ProfileUpdate/api/info?type=fullname', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            navbarFullName.textContent = data.fullName || "John Doe";
        } else {
            navbarFullName.textContent = "John Doe";
        }
    } catch (error) {
        navbarFullName.textContent = "John Doe";
    }
}

async function fetchUserRole() {
    const statusText = document.getElementById('statusText');
    try {
        const response = await fetch('/ProfileUpdate/api/info?type=role', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            const roles = {
                'Admin': 'Admin',
                'Customer': 'Customer',
                'Technician': 'Technician',
                'Company': 'Company'
            };
            statusText.textContent = roles[data.role] || "Unknown";
        } else {
            statusText.textContent = "Error";
        }
    } catch (error) {
        statusText.textContent = "Error";
    }
}

document.addEventListener('DOMContentLoaded', () => {
    fetchFullName();
    fetchUserRole();
});