const apiPortfolioUrl = "/api/Portfolio";

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

function openModal() {
    const modal = new bootstrap.Modal(document.getElementById('jobModal'));
    modal.show();
    attachCloseButtonListeners(document.getElementById('jobModal'))
}

async function getPortfolio() {
    try {
        const response = await fetch(`${apiPortfolioUrl}/my-portfolio`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (response.ok) {
            const result = await response.json();
            populatePortfolioContainer(result.portfolioDocuments);
        } else {
            populatePortfolioContainer([]);
        }
    } catch (error) {
        alert("An error occurred while fetching portfolio.");
    }
}

function populatePortfolioContainer(portfolio) {
    const container = document.getElementById("portfolioContainer");
    container.innerHTML = "";

    if (!portfolio || portfolio.length === 0) {
        container.innerHTML = `
            <div class="d-flex justify-content-center align-items-center" style="width: 100%;">
                <p class="text-muted">No portfolio images available.</p>
            </div`;
        return;
    }

    portfolio.forEach(item => {
        const col = document.createElement("div");
        col.className = "col-4 mb-2 position-relative";

        col.innerHTML = `
            <div class="d-flex flex-column justify-content-center align-items-center border p-1" style="cursor: pointer;" onclick="openPortfolioModal('${item.filePath}', '${item.title}', '${item.description || "No description available"}')">
                <img src="${item.filePath}" alt="Portfolio Image" class="img-fluid rounded" style="height: 8em; object-fit: cover;">
                <p class="text-muted" style="margin-bottom: 0.5em; margin-top: 0.5em;">${item.title}</p>
                <button class="btn btn-danger btn-sm top-0 end-0" onclick="event.stopPropagation(); deletePortfolioImage(${item.id});">×</button>
            </div>
        `;

        container.appendChild(col);
    });
}

function openPortfolioModal(filePath, title, description) {
    const modalHTML = `
        <div class="modal-overlay">
            <div class="modal-content">
                <span class="close-modal">×</span>
                <img src="${filePath}" alt="Full Image" class="modal-image" style="height: 25em;"/>
                <h3>${title}</h3>
                <p>${description}</p>
            </div>
        </div>
    `;

    const newModalElement = document.createElement('div');
    newModalElement.innerHTML = modalHTML;

    document.body.appendChild(newModalElement);
    attachCloseButtonListeners(newModalElement);

    document.querySelector('.modal-overlay').addEventListener('click', (e) => {
        if (e.target === document.querySelector('.modal-overlay')) {
            document.querySelector('.modal-overlay').remove();
        }
    });
}

async function uploadPortfolioImage(event) {
    event.preventDefault();

    const formData = new FormData();
    const title = document.getElementById("portfolioTitle").value;
    const description = document.getElementById("portfolioDescription").value;
    const photoInput = document.getElementById("portfolioPhoto");

    if (photoInput.files.length === 0) {
        alert("Please select a photo to upload.");
        return;
    }

    formData.append("title", title);
    formData.append("description", description);
    formData.append("photo", photoInput.files[0]);

    try {
        const response = await fetch(`${apiPortfolioUrl}/upload-portfolio`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${getCookie("jwtToken")}`
            },
            body: formData
        });

        if (response.ok) {
            alert("Portfolio image uploaded successfully!");
            const modal = new bootstrap.Modal(document.getElementById('jobModal'));
            modal.hide();
            document.getElementById('jobForm').reset();
            getPortfolio();
        } else {
            const errorData = await response.json();
            alert(`Failed to upload image: ${errorData.message || "Unknown error."}`);
        }
    } catch (error) {
        alert("An error occurred while uploading the image. Please try again.");
    }
}

async function deletePortfolioImage(portfolioDocumentId) {
    if (!confirm("Are you sure you want to delete this portfolio image?")) {
        return;
    }

    try {
        const response = await fetch(`${apiPortfolioUrl}/delete-portfolio/${portfolioDocumentId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
            }
        });

        if (response.ok) {
            alert("Portfolio image deleted successfully!");
            getPortfolio();
        } else {
            const errorData = await response.json();
            alert(`Failed to delete the portfolio image: ${errorData.Message || "Unknown error occurred."}`);
        }
    } catch (error) {
        alert("An error occurred while trying to delete the portfolio image.");
    }
}
/*TO BE FIXED*/
//function populateDaysOfWeek() {
//    const daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
//    const daysAvailable = document.getElementById("daysAvailable");

//    daysAvailable.innerHTML = "";

//    daysOfWeek.forEach(day => {
//        const option = document.createElement("option");
//        option.value = day;
//        option.textContent = day;
//        daysAvailable.appendChild(option);
//    });
//}

//function populateMonths() {
//    const months = [
//        "January", "February", "March", "April", "May", "June",
//        "July", "August", "September", "October", "November", "December"
//    ];
//    const monthsUnavailable = document.getElementById("monthsUnavailable");

//    monthsUnavailable.innerHTML = "";

//    months.forEach(month => {
//        const option = document.createElement("option");
//        option.value = month;
//        option.textContent = month;
//        monthsUnavailable.appendChild(option);
//    });
//}

//function toggleAvailability() {
//    const availabilityOptions = document.getElementById("availabilityOptions");
//    availabilityOptions.classList.toggle("d-none");
//}

//window.onload = function () {
//    populateDaysOfWeek();
//    populateMonths();
//};

document.addEventListener('DOMContentLoaded', () => {
    const portfolioContainer = document.getElementById('portfolioContainer');
    if (portfolioContainer) {
        getPortfolio();
    }
});