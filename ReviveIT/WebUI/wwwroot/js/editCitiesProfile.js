async function fetchSelectedCities() {
    try {
        const response = await fetch('/ProfileUpdate/api/get-selected-cities', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch selected cities");

        const selectedCities = await response.json();

        const selectedCitiesList = document.getElementById('selectedCitiesList');
        selectedCitiesList.innerHTML = '';

        selectedCities.forEach(city => {
            const listItem = document.createElement('li');
            listItem.textContent = city.cityName;
            selectedCitiesList.appendChild(listItem);
        });
    } catch (error) {
        console.error("Error fetching selected cities:", error);
        alert("An error occurred while fetching selected cities.");
    }
}

async function editCities() {
    try {
        const response = await fetch('/api/city/getCities', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch cities");

        const cities = await response.json();
        const allCitiesList = document.getElementById('allCitiesList');
        allCitiesList.innerHTML = '';

        const selectAllItem = document.createElement('li');
        selectAllItem.innerHTML = `
            <label>
                <input type="checkbox" id="selectAllCities" /> Select All Cities
            </label>`;
        allCitiesList.appendChild(selectAllItem);

        cities.forEach(city => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <label>
                    <input type="checkbox" value="${city.cityId}" /> ${city.cityName}
                </label>`;
            allCitiesList.appendChild(listItem);
        });

        document.getElementById('editCitiesModal').style.display = 'block';

        const selectedCities = Array.from(document.querySelectorAll('#selectedCitiesList li')).map(li => li.textContent);
        document.querySelectorAll('#allCitiesList input').forEach(input => {
            if (selectedCities.includes(input.nextSibling.textContent.trim())) {
                input.checked = true;
            }
        });

        const selectAllCheckbox = document.getElementById('selectAllCities');
        selectAllCheckbox.addEventListener('change', function () {
            const allCheckboxes = allCitiesList.querySelectorAll('input[type="checkbox"]:not(#selectAllCities)');
            allCheckboxes.forEach(checkbox => {
                checkbox.checked = selectAllCheckbox.checked;
            });
        });

        allCitiesList.addEventListener('change', function (event) {
            if (event.target !== selectAllCheckbox) {
                const allCheckboxes = allCitiesList.querySelectorAll('input[type="checkbox"]:not(#selectAllCities)');
                const allChecked = Array.from(allCheckboxes).every(checkbox => checkbox.checked);
                selectAllCheckbox.checked = allChecked;
            }
        });
    } catch (error) {
        console.error("Error fetching cities:", error);
        alert("An error occurred while fetching cities.");
    }
}

function closeEditCitiesModal() {
    document.getElementById('editCitiesModal').style.display = 'none';
}

async function saveCities() {
    try {
        const selectedCityIds = Array.from(document.querySelectorAll('#allCitiesList input:checked'))
            .map(input => parseInt(input.value))
            .filter(id => !isNaN(id));

        if (!selectedCityIds.length) {
            alert("Please select at least one city.");
            return;
        }

        const response = await fetch('/ProfileUpdate/api/update-operating-cities', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(selectedCityIds)
        });

        if (!response.ok) throw new Error("Failed to update operating cities");

        alert("Operating cities updated successfully!");
        closeEditCitiesModal();
        fetchSelectedCities();
    } catch (error) {
        console.error("Error updating operating cities:", error);
        alert("An error occurred while updating operating cities.");
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const citiesContainer = document.getElementById('selectedCitiesContainer');
    if (citiesContainer) {
        fetchSelectedCities();
    }
});