function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(";").shift();
    return "";
}

async function fetchData(url, method = "GET", body = null, isFormData = false) {
    try {
        const headers = {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            ...(isFormData ? {} : { "Content-Type": "application/json" }),
        };
        const response = await fetch(url, {
            method,
            headers,
            body: body ? (isFormData ? body : JSON.stringify(body)) : null,
        });
        if (!response.ok) {
            const errorText = await response.text();
            alert(`HTTP Error ${response.status}: ${errorText}`);
            throw new Error(`HTTP Error ${response.status}: ${errorText}`);
        }
        return response.json();
    } catch (error) {
        alert(`Error fetching data: ${error.message}`);
        throw error;
    }
}

function openModalPfp() {
    const modal = new bootstrap.Modal(document.getElementById("profileModal"));
    modal.show();
    attachCloseButtonListeners(document.getElementById('profileModal'));
}

function zoomImage() {
    const profileImage = document.getElementById("profileImage").src;
    const zoomedImage = document.getElementById("zoomedImage");
    zoomedImage.src = profileImage;

    const zoomModal = new bootstrap.Modal(document.getElementById("zoomModal"));
    zoomModal.show();
    attachCloseButtonListeners(document.getElementById('zoomModal'));
}

document.addEventListener("DOMContentLoaded", () => {
    fetchName();
    fetchRole();
    getProfilePicture();
    fetchWorkExperience();
    getReviews();
});

async function fetchName() {
    const userName = document.getElementById("UserName");
    if (!userName) return;

    try {
        const response = await fetch("/ProfileUpdate/api/info?type=fullname", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            userName.textContent = data.fullName || "";
        } else {
            userName.textContent = "";
        }
    } catch (error) {
        userName.textContent = "";
    }
}

async function fetchRole() {
    const userRole = document.getElementById("userRole");
    if (!userRole) return;

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
            userRole.textContent = roles[data.role] || "Unknown";
        } else {
            userRole.textContent = "Error";
        }
    } catch (error) {
        userRole.textContent = "Error";
    }
}

async function fetchWorkExperience() {
    const workExperienceText = document.getElementById("workExperienceText");
    if (!workExperienceText) return;

    try {
        const response = await fetch("/ProfileUpdate/api/info?type=experience", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            const experienceYears = data.experience;
            if (experienceYears) {
                workExperienceText.textContent = `${experienceYears} years experience`;
            } else {
                workExperienceText.textContent = "No work experience provided.";
            }
        } else {
            workExperienceText.textContent = "Failed to load work experience.";
        }
    } catch (error) {
        workExperienceText.textContent = "Error loading work experience.";
    }
}

let cropper;
const cropperModal = document.getElementById("cropperModal");
const cropperImage = document.getElementById("cropperImage");
const profileImage = document.getElementById("profileImage");
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
    const fileInput = document.getElementById("imageUpload");
    const file = fileInput.files[0];

    if (!file) {
        alert("Please select a file to upload.");
        return;
    }

    const formData = new FormData();
    formData.append("ProfilePicture", file);

    try {
        const response = await fetch("/ProfileUpdate/api/upload", {
            method: "POST",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
            body: formData,
        });

        if (response.ok) {
            const result = await response.json();
            alert("Profile picture uploaded successfully!");
            getProfilePicture();
        } else {
            const error = await response.json();
            alert(`Failed to upload: ${error.message || "Unknown error"}`);
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
            cropperModal.classList.add("d-none");
            cropper.destroy();
            cropper = null;
        }
    }
}

function cancelCrop() {
    cropperModal.classList.add("d-none");
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
        document.querySelectorAll(".cropper-hide, .cropper-view-box").forEach(
            (element) => {
                element.style.display = "none";
            }
        );
    },
});

async function getProfilePicture() {
    try {
        const response = await fetch("/ProfileUpdate/api/get", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            const data = await response.json();
            const profileImage = document.getElementById("profileImage");
            profileImage.src = data.profilePictureUrl;
        }
    } catch (error) {
        const profileImage = document.getElementById("profileImage");
        profileImage.src = "/images/defaultProfilePicture.png";
    }
}

async function removeProfilePicture() {
    try {
        const response = await fetch("/ProfileUpdate/api/remove", {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
        });

        if (response.ok) {
            alert("Profile picture removed successfully!");
            getProfilePicture();
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
        const response = await fetch("/ProfileUpdate/api/update-profile", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${getCookie("jwtToken")}`,
            },
            body: JSON.stringify(updateProfileDTO),
        });

        if (response.ok) {
            alert("Profile picture updated successfully!");
            getProfilePicture();
        } else {
            const error = await response.json();
            alert(
                `Failed to update profile picture: ${error.message || "Unknown error"}`
            );
        }
    } catch (error) {
        alert("An error occurred while updating the profile picture.");
    }
}

function convertToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
        reader.readAsDataURL(file);
    });
}

document.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
        const categoriesModal = document.getElementById("editCategoriesModal");
        const citiesModal = document.getElementById("editCitiesModal");

        if (categoriesModal && categoriesModal.style.display === "block") {
            closeEditCategoriesModal();
        }

        if (citiesModal && citiesModal.style.display === "block") {
            closeEditCitiesModal();
        }
    }
});

async function getReviews() {
    try {
        const profileUrl = `/ProfileUpdate/api/info?type=userid`;
        const data = await fetchData(profileUrl);

        if (!data || !data.userid) {
            alert("Failed to fetch user ID.");
            document.getElementById("reviewsDropdown").innerHTML =
                `<p class='no-reviews-message' >Failed to fetch user id.</p>`;
            return;
        }
        const userId = data.userid;
        const reviewUrl = `/api/review/technicians/${userId}/reviews`;
        const reviewData = await fetchData(reviewUrl);


        if (!reviewData || !reviewData.reviews || reviewData.reviews.length === 0) {
            document.getElementById("reviewsDropdown").innerHTML =
                `<p class='no-reviews-message' >No reviews available.</p>`;
            return;
        }
        displayReviews(reviewData.reviews);
    } catch (error) {
        alert(`Error fetching reviews: ${error.message}`);
        document.getElementById("reviewsDropdown").innerHTML =
            `<p class="error-message">Failed to load reviews.</p>`;
    }
}
function displayReviews(reviews) {
    const reviewsDropdown = document.getElementById("reviewsDropdown");
    reviewsDropdown.innerHTML = "";

    if (reviews && reviews.length > 0) {
        let reviewsHTML = reviews
            .map(
                (review) =>
                    `<div class="review-item">
                             <p class="rating">Rating: ${review.rating}/5</p>
                             <p class="comment">Comment: ${review.content}</p>
                            <p class="reviewer">Review by ${review.reviewerName || "Anonymous User"
                    }</p>
                   </div>`
            )
            .join("");
        reviewsDropdown.innerHTML = reviewsHTML;
    } else {
        document.getElementById("reviewsDropdown").innerHTML =
            `<p class='no-reviews-message' >No reviews available.</p>`;
    }
}

document.getElementById("toggleReviews").addEventListener("click", function () {
    const reviewsDropdown = document.getElementById("reviewsDropdown");
    const isVisible = reviewsDropdown.style.display !== "none";
    reviewsDropdown.style.display = isVisible ? "none" : "block";
    this.textContent = isVisible ? "Show Reviews" : "Hide Reviews";
    this.setAttribute("aria-expanded", (!isVisible).toString());
});

function attachCloseButtonListeners(modal) {
    const closeButtons = modal.querySelectorAll(".close-modal");

    closeButtons.forEach(button => {
        button.addEventListener('click', function () {
            const modal = button.closest('.modal-overlay, #profileModal, #zoomModal, #cropperModal, #editCategoriesModal, #editCitiesModal, #jobModal, .modal, .modal-overlay');
            if (modal) {
                modal.style.display = "none";
                document.body.classList.remove("modal-open");
                document.querySelector(".modal-backdrop")?.remove();
            }
        });
    });
}
function closeEditCategoriesModal() {
    document.getElementById("editCategoriesModal").style.display = "none";
}

function closeEditCitiesModal() {
    document.getElementById("editCitiesModal").style.display = "none";
}