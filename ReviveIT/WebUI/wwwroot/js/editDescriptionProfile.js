async function getDescription() {
    try {
        const response = await fetch('/api/Profile/get-description', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} ${response.statusText}`);
        }

        const data = await response.json();

        document.getElementById('descriptionText').innerText = data.description || "No description available.";
    } catch (error) {
        console.error("Failed to fetch description:", error);
        alert("An error occurred while fetching the description.");
    }
}

function editDescription() {
    const descriptionText = document.getElementById("descriptionText");
    const currentDescription = descriptionText.innerText;

    descriptionText.innerHTML = `
        <textarea id="descriptionTextarea" class="form-control" rows="3">${currentDescription}</textarea>
        <button class="btn btn-primary btn-sm mt-2" onclick="saveDescription()">Save</button>
        <button class="btn btn-secondary btn-sm mt-2" onclick="cancelEdit()">Cancel</button>
    `;
}

function cancelEdit() {
    getDescription();
}

async function saveDescription() {
    const descriptionTextarea = document.getElementById("descriptionTextarea");
    const newDescription = descriptionTextarea.value;

    try {
        const response = await fetch('/api/Profile/update-description', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            },
            body: JSON.stringify({ description: newDescription })
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.status} ${response.statusText}`);
        }

        getDescription();
    } catch (error) {
        console.error("Failed to save description:", error);
        alert("An error occurred while saving the description.");
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const descriptionContainer = document.getElementById('descriptionContainer');
    if (descriptionContainer) { 
        getDescription();
    }
});