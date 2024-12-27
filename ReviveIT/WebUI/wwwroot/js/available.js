const baseUrl = "https://localhost:7018/api/TechnicianAvailability";

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

const headers = {
    'Authorization': `Bearer ${getCookie('jwtToken')}`,
    'Content-Type': 'application/json'
};

document.addEventListener("DOMContentLoaded", async () => {
    const apiUrl = "/api/TechnicianAvailability/AvailableTechnician";
    const saveButton = document.getElementById('saveAvailability');
    const updateButton = document.getElementById('updateAvailability');

    try {
        const response = await fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();

        if (data && data.available && data.available.length > 0) {
            displayData(data.available);
            const { daysAvailable, monthsUnavailable, specificUnavailableDates } = data.available[0];

            if (daysAvailable) {
                daysAvailable.split(',').forEach(day => {
                    const option = document.querySelector(`#daysAvailable option[value="${day}"]`);
                    if (option) {
                        option.selected = true;
                    }
                });
            }

            if (monthsUnavailable) {
                monthsUnavailable.split(',').forEach(month => {
                    const option = document.querySelector(`#monthsUnavailable option[value="${month}"]`);
                    if (option) {
                        option.selected = true;
                    }
                });
            }

            if (specificUnavailableDates) {
                document.getElementById('specificUnavailableDates').value = specificUnavailableDates;
            }

            saveButton.style.display = 'none';
            updateButton.style.display = 'block';
        } else {
            displayNoDataMessage();
            saveButton.style.display = 'block';
            updateButton.style.display = 'none';
        }
    } catch (error) {
        console.error("Failed to fetch data:", error);
        displayNoDataMessage();
    }
});

function displayData(availabilityData) {
    const container = document.getElementById("dataContainer");
    container.innerHTML = "";

    if (!availabilityData || availabilityData.length === 0) {
        displayNoDataMessage();
        return;
    }

    availabilityData.forEach((item) => {
        const availabilityCard = document.createElement("div");
        availabilityCard.classList.add("mb-4", "p-3", "border", "rounded");

        availabilityCard.innerHTML = `
            <p><strong>Days Available:</strong> ${item.daysAvailable}</p>
            <p><strong>Months Unavailable:</strong> ${item.monthsUnavailable}</p>
            <p><strong>Specific Unavailable Dates:</strong> ${item.specificUnavailableDates}</p>
        `;

        container.appendChild(availabilityCard);
    });
}

function displayNoDataMessage() {
    const container = document.getElementById("dataContainer");
    container.innerHTML = `<div class="alert alert-info" role="alert">
        No availability set by the technician yet.
    </div>`;
}



async function saveAvailability() {
    const daysAvailable = Array.from(document.getElementById('daysAvailable').selectedOptions).map(option => option.value);
    const monthsUnavailable = Array.from(document.getElementById('monthsUnavailable').selectedOptions).map(option => option.value);
    const specificUnavailableDates = document.getElementById('specificUnavailableDates').value;

    if (!daysAvailable.length || !monthsUnavailable.length) {
        alert("Please select available days and unavailable months.");
        return;
    }

    const payload = {
        DaysAvailable: daysAvailable.join(','),
        MonthsUnavailable: monthsUnavailable.join(','),
        SpecificUnavailableDates: specificUnavailableDates
    };

    console.log('Payload:', payload);

    try {
        const response = await fetch('/api/TechnicianAvailability/PostAvailable', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const error = await response.json();
            console.error('Error:', error);
            alert(`Error: ${error.message || 'Bad Request'}`);
        } else {
            alert('Availability saved successfully.');
            clearForm();
            location.reload();
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function updateAvailability() {
    const daysAvailable = Array.from(document.getElementById('daysAvailable').selectedOptions).map(option => option.value);
    const monthsUnavailable = Array.from(document.getElementById('monthsUnavailable').selectedOptions).map(option => option.value);
    const specificUnavailableDates = document.getElementById('specificUnavailableDates').value;

    if (!daysAvailable.length || !monthsUnavailable.length) {
        alert("Please select available days and unavailable months.");
        return;
    }


    const payload = {
        DaysAvailable: daysAvailable.join(','),
        MonthsUnavailable: monthsUnavailable.join(','),
        SpecificUnavailableDates: specificUnavailableDates
    };

    try {
        const response = await fetch('/api/TechnicianAvailability/update-available', {
            method: 'PUT',
            headers,
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const error = await response.json();
            console.error('Error:', error);
            alert(`Error: ${error.message || 'Bad Request'}`);
        } else {
            alert('Availability updated successfully.');
            clearForm();
            location.reload();
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function deleteAvailability() {
    try {
        const response = await fetch('/api/TechnicianAvailability/delete-availableTechnician', {
            method: 'DELETE',
            headers
        });

        if (response.ok) {
            console.log('Availability deleted successfully.');
            clearForm();
            location.reload();
        } else {
            console.error('Failed to delete availability:', await response.json());
        }
    } catch (error) {
        console.error('Error deleting availability:', error);
    }
}

function clearForm() {
    document.getElementById('daysAvailable').selectedIndex = -1;
    document.getElementById('monthsUnavailable').selectedIndex = -1;
    document.getElementById('specificUnavailableDates').value = '';
}

document.getElementById('saveAvailability').addEventListener('click', saveAvailability);
document.getElementById('updateAvailability').addEventListener('click', updateAvailability);
document.getElementById('deleteAvailability').addEventListener('click', deleteAvailability);