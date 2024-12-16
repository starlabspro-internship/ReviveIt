const userApiUrl = "/api/ProfileUpdate";

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

document.addEventListener("DOMContentLoaded", () => {
    fetchName();
    fetchRole();
});

async function fetchName() {
    const userName = document.getElementById('UserName');
    if (!userName) {
        console.error("Element with ID 'UserName' not found.");
        return;
    }

    try {
        const response = await fetch('/api/ProfileUpdate/info?type=fullname', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            userName.textContent = data.fullName || "John Doe";
        } else {
            console.error("Failed to fetch full name:", response.status);
            userName.textContent = "John Doe";
        }
    } catch (error) {
        console.error("Error fetching full name:", error);
        userName.textContent = "John Doe";
    }
}

async function fetchRole() {
    const userRole = document.getElementById('userRole');
    if (!userRole) {
        console.error("Element with ID 'userRole' not found.");
        return;
    }

    try {
        const response = await fetch('/api/ProfileUpdate/info?type=role', {
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
            console.error("Failed to fetch role:", response.status);
            userRole.textContent = "Error";
        }
    } catch (error) {
        console.error("Error fetching role:", error);
        userRole.textContent = "Error";
    }
}

function populateDaysOfWeek() {
    const daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
    const daysAvailable = document.getElementById("daysAvailable");

    daysAvailable.innerHTML = "";

    daysOfWeek.forEach(day => {
        const option = document.createElement("option");
        option.value = day;
        option.textContent = day;
        daysAvailable.appendChild(option);
    });
}

function populateMonths() {
    const months = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];
    const monthsUnavailable = document.getElementById("monthsUnavailable");

    monthsUnavailable.innerHTML = "";

    months.forEach(month => {
        const option = document.createElement("option");
        option.value = month;
        option.textContent = month;
        monthsUnavailable.appendChild(option);
    });
}

function toggleAvailability() {
    const availabilityOptions = document.getElementById("availabilityOptions");
    availabilityOptions.classList.toggle("d-none");
}

window.onload = function () {
    populateDaysOfWeek();
    populateMonths();
};

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
        const response = await fetch(`${userApiUrl}/upload`, {
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
            console.error("Upload failed:", error);
            alert(`Failed to upload: ${error.message || 'Unknown error'}`);
        }
    } catch (error) {
        console.error("Error uploading profile picture:", error);
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

async function getProfilePicture() {
    try {
        const response = await fetch(`${userApiUrl}/get`, {
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
            console.error("Failed to fetch profile picture:", response.status);
            const profileImage = document.getElementById('profileImage');
            profileImage.src = "https://via.placeholder.com/150";
        }
    } catch (error) {
        console.error("Error fetching profile picture:", error);
        const profileImage = document.getElementById('profileImage');
        profileImage.src = "https://via.placeholder.com/150";
    }
}

async function removeProfilePicture() {
    try {
        const response = await fetch(`${userApiUrl}/remove`, {
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
        console.error("Error removing profile picture:", error);
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
        const response = await fetch(`${userApiUrl}/update-profile`, {
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
            console.error("Failed to update profile picture:", error);
            alert(`Failed to update profile picture: ${error.message || 'Unknown error'}`);
        }
    } catch (error) {
        console.error("Error updating profile picture:", error);
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
});