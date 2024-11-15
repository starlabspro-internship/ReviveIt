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

function updateProfileImage() {
    const fileInput = document.getElementById('imageUpload');
    const file = fileInput.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            cropperModal.classList.remove('d-none');
            cropperImage.src = e.target.result;

            if (cropper) cropper.destroy();

            cropper = new Cropper(cropperImage, {
                aspectRatio: 1,
                viewMode: 1,
                autoCropArea: 0.8,
                movable: true,
                rotatable: true,
                cropBoxResizable: true,
                responsive: true,
                crop(event) {
                }
            });
        };
        reader.readAsDataURL(file);
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