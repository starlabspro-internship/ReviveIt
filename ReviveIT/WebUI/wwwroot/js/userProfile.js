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

function openModalPfp() {
    const modal = new bootstrap.Modal(document.getElementById('profileModal'));
    modal.show();
}

function zoomImage() {
    const profileImage = document.getElementById('profileImage').src;
    const zoomedImage = document.getElementById('zoomedImage');
    zoomedImage.src = profileImage;

    const zoomModal = new bootstrap.Modal(document.getElementById('zoomModal'));
    zoomModal.show();
}

document.addEventListener("DOMContentLoaded", () => {
    fetchName();
    fetchRole();
});

async function fetchName() {
    const userName = document.getElementById('UserName');
    if (!userName) return;

    try {
        const response = await fetch('/ProfileUpdate/api/info?type=fullname', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            userName.textContent = data.fullName || "";
        } else {
            userName.textContent = "John Doe";
        }
    } catch (error) {
        userName.textContent = "John Doe";
    }
}

async function fetchRole() {
    const userRole = document.getElementById('userRole');
    if (!userRole) return;

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
            userRole.textContent = roles[data.role] || "Unknown";
        } else {
            userRole.textContent = "Error";
        }
    } catch (error) {
        userRole.textContent = "Error";
    }
}

let cropper;
const cropperModal = document.getElementById('cropperModal');
const cropperImage = document.getElementById('cropperImage');
const profileImage = document.getElementById('profileImage');
const imageUpload = document.getElementById("imageUpload");

imageUpload.addEventListener("change", (event) => {
    const file = event.target.files[0];

    if (file) {
        const reader = new FileReader();

        reader.onload = (e) => {
            profileImage.src = e.target.result;
        };

        reader.readAsDataURL(file);
    }
});

async function uploadProfileImage() {
    const fileInput = document.getElementById('imageUpload');
    const file = fileInput.files[0];

    if (!file) {
        alert("Please select a file to upload.");
        return;
    }

    const formData = new FormData();
    formData.append("ProfilePicture", file);

    try {
        const response = await fetch('/ProfileUpdate/api/upload', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            },
            body: formData
        });

        if (response.ok) {
            const result = await response.json();
            alert("Profile picture uploaded successfully!");
            getProfilePicture();
        } else {
            const error = await response.json();
            alert(`Failed to upload: ${error.message || 'Unknown error'}`);
        }
    } catch (error) {
        alert("An error occurred while uploading the profile picture.");
    }
}

function rotateImage(degrees) {
    if (cropper) cropper.rotate(degrees);
}

function saveCroppedImage() {
    if (cropper) {
        const croppedCanvas = cropper.getCroppedCanvas();
        if (croppedCanvas) {
            profileImage.src = croppedCanvas.toDataURL();
            cropperModal.classList.add('d-none');
            cropper.destroy();
            cropper = null;
        }
    }
}

function cancelCrop() {
    cropperModal.classList.add('d-none');
    if (cropper) {
        cropper.destroy();
        cropper = null;
    }
}

cropper = new Cropper(cropperImage, {
    aspectRatio: 1,
    viewMode: 1,
    autoCropArea: 0.8,
    movable: true,
    rotatable: true,
    cropBoxResizable: true,
    responsive: true,
    ready() {
        document.querySelectorAll('.cropper-hide, .cropper-view-box').forEach(element => {
            element.style.display = 'none';
        });
    }
});

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

async function editCategories() {
    try {
        const response = await fetch('/api/categories/getCategories', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch categories");

        const categories = await response.json();

        const allCategoriesList = document.getElementById('allCategoriesList');
        allCategoriesList.innerHTML = '';

        categories.forEach(category => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <label>
                    <input type="checkbox" value="${category.categoryID}" /> ${category.name}
                </label>`;
            allCategoriesList.appendChild(listItem);
        });

        document.getElementById('editCategoriesModal').style.display = 'block';

        const selectedCategories = Array.from(document.querySelectorAll('#selectedCategoriesList li')).map(li => li.textContent.trim());
        document.querySelectorAll('#allCategoriesList input').forEach(input => {
            if (selectedCategories.includes(input.nextSibling.textContent.trim())) {
                input.checked = true;
            }
        });
    } catch (error) {
        console.error("Error fetching categories:", error);
        alert("An error occurred while fetching categories.");
    }
}

function closeEditCategoriesModal() {
    document.getElementById('editCategoriesModal').style.display = 'none';
}

async function saveCategories() {
    try {
        const selectedCategoryIds = Array.from(document.querySelectorAll('#allCategoriesList input:checked'))
            .map(input => parseInt(input.value))
            .filter(id => !isNaN(id));

        if (!selectedCategoryIds.length) {
            alert("Please select at least one category.");
            return;
        }

        const response = await fetch('/api/Profile/updateCategories', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(selectedCategoryIds)
        });

        if (!response.ok) throw new Error("Failed to update categories");

        alert("Categories updated successfully!");
        closeEditCategoriesModal();
        fetchSelectedCategories();
    } catch (error) {
        console.error("Error updating categories:", error);
        alert("An error occurred while updating categories.");
    }
}

async function fetchSelectedCategories() {
    try {
        const response = await fetch('/api/Profile/get-selected-categories', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch selected categories");

        const categories = await response.json();

        const categoriesList = document.getElementById('selectedCategoriesList');
        categoriesList.innerHTML = '';

        categories.forEach(category => {
            const listItem = document.createElement('li');
            listItem.textContent = category.name;
            categoriesList.appendChild(listItem);
        });
    } catch (error) {
        console.error("Error fetching selected categories:", error);
        alert("An error occurred while fetching the categories.");
    }
}

async function fetchSelectedCities() {
    try {
        const response = await fetch('/api/Profile/getSelectedCities', {
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

        cities.forEach(city => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <label>
                    <input type="checkbox" value="${city.cityId}" /> ${city.cityName}
                </label>`;
            allCitiesList.appendChild(listItem);
        });

        // Show modal
        document.getElementById('editCitiesModal').style.display = 'block';

        // Pre-select user's selected cities
        const selectedCities = Array.from(document.querySelectorAll('#selectedCitiesList li')).map(li => li.textContent);
        document.querySelectorAll('#allCitiesList input').forEach(input => {
            if (selectedCities.includes(input.nextSibling.textContent.trim())) {
                input.checked = true;
            }
        });
    } catch (error) {
        console.error("Error fetching cities:", error);
        alert("An error occurred while fetching cities.");
    }
}

// Close the modal
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

        const response = await fetch('/api/Profile/updateOperatingCities', {
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

async function getProfilePicture() {
    try {
        const response = await fetch('/ProfileUpdate/api/get', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();

            const profileImage = document.getElementById('profileImage');

            if (data.profilePictureUrl) {
                profileImage.src = `${data.profilePictureUrl}`;
            } else {
                profileImage.src = "https://via.placeholder.com/150";
            }
        } else {
            const profileImage = document.getElementById('profileImage');
            profileImage.src = "https://via.placeholder.com/150";
        }
    } catch (error) {
        const profileImage = document.getElementById('profileImage');
        profileImage.src = "https://via.placeholder.com/150";
    }
}

async function removeProfilePicture() {
    try {
        const response = await fetch('/ProfileUpdate/api/remove', {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            alert("Profile picture removed successfully!");
            const profileImage = document.getElementById('profileImage');
            profileImage.src = 'https://via.placeholder.com/150';
        } else {
            alert("Failed to remove profile picture.");
        }
    } catch (error) {
        alert("Error removing profile picture.");
    }
}

async function updateProfileImage(event) {
    const fileInput = document.getElementById("imageUpload");

    if (fileInput.files.length === 0) {
        alert("Please select a profile picture to upload.");
        return;
    }

    const file = fileInput.files[0];
    const base64Image = await convertToBase64(file);

    const updateProfileDTO = {
        profilePicture: base64Image,
    };

    try {
        const response = await fetch('/ProfileUpdate/api/update-profile', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            },
            body: JSON.stringify(updateProfileDTO),
        });

        if (response.ok) {
            alert("Profile picture updated successfully!");
            getProfilePicture();
        } else {
            const error = await response.json();
            alert(`Failed to update profile picture: ${error.message || 'Unknown error'}`);
        }
    } catch (error) {
        alert("An error occurred while updating the profile picture.");
    }
}

function convertToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
        reader.readAsDataURL(file);
    });
}

document.addEventListener('DOMContentLoaded', () => {
    getProfilePicture();
    getDescription();
    fetchSelectedCategories();
    fetchSelectedCities();
});

document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape') {
        const categoriesModal = document.getElementById('editCategoriesModal');
        const citiesModal = document.getElementById('editCitiesModal');

        if (categoriesModal && categoriesModal.style.display === 'block') {
            closeEditCategoriesModal();
        }

        if (citiesModal && citiesModal.style.display === 'block') {
            closeEditCitiesModal();
        }
    }
});
