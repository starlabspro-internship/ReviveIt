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
