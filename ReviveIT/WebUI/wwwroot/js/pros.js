let skipCount = 0;
const initialPageSize = 3;
const additionalPageSize = 6;
let isAuthenticated = false;

async function checkAuthenticationStatus() {
    const response = await fetch('/Pros/api/IsAuthenticated', {
        credentials: 'include',
    });

    if (response.ok) {
        const data = await response.json();
        isAuthenticated = data.isAuthenticated;
    } else {
        console.error('Failed to check authentication status');
    }
}

async function loadTechnicians() {
    const takeCount = skipCount === 0 ? initialPageSize : additionalPageSize;

    const response = await fetch(`/Pros/api/GetPros?skipCount=${skipCount}&takeCount=${takeCount}`, {
        credentials: 'include',
    });

    if (!response.ok) {
        const errorData = await response.json();
        alert(`Error: ${errorData.message}`);
        return;
    }

    const data = await response.json();

    if (skipCount === 0) {
        document.getElementById('technicianContainer').innerHTML = '';
    }

    renderTechnicians(data.data);

    const container = document.getElementById('technicianContainer');
    const totalRendered = container.childElementCount;

    // Check if buttons exist before hiding
    const viewMoreButton = document.getElementById('viewMoreButton');
    const loginToSeeMoreButton = document.getElementById('loginToSeeMoreButton');

    if (totalRendered >= data.total) {
        if (viewMoreButton) viewMoreButton.style.display = 'none';
        if (loginToSeeMoreButton) loginToSeeMoreButton.style.display = 'none';
    } else {
        skipCount += takeCount;
    }
}

function renderTechnicians(technicians) {
    const container = document.getElementById('technicianContainer');

    technicians.forEach(technician => {
        const profileLink = isAuthenticated
            ? `/ProsProfileView/${technician.id}`
            : `/LogIn?returnUrl=/ProsProfileView/${technician.id}`;

        const technicianHtml = `
            <div class="col-md-6 col-lg-4 mx-auto">
                <div class="box">
                    <div class="img-box">
                        <img src="${technician.profilePicture}" alt="${technician.fullName}'s Profile Picture" style="width: 100%; height: auto; object-fit: cover;"/>
                    </div>
                    <div class="detail-box">
                        <a href="${profileLink}">${technician.fullName}</a>
                        <h6 class="expert_position">
                            <span>${technician.expertise}</span>
                            <span>${technician.experience} Years of Experience</span>
                        </h6>
                        <span class="expert_rating">
                            ${generateStars(technician.rating)}
                        </span>
                        <p>${technician.review}</p>
                    </div>
                </div>
            </div>`;

        container.innerHTML += technicianHtml;
    });
}

function generateStars(rating) {
    const fullStars = Math.floor(rating);
    const halfStar = rating % 1 >= 0.5;
    const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

    let starsHtml = '';

    for (let i = 0; i < fullStars; i++) {
        starsHtml += '<i class="fa fa-star" aria-hidden="true"></i>';
    }

    if (halfStar) {
        starsHtml += '<i class="fa fa-star-half-o" aria-hidden="true"></i>';
    }

    for (let i = 0; i < emptyStars; i++) {
        starsHtml += '<i class="fa fa-star-o" aria-hidden="true"></i>';
    }

    return starsHtml;
}

document.addEventListener('DOMContentLoaded', async () => {
    await checkAuthenticationStatus();
    loadTechnicians();

    const viewMoreButton = document.getElementById('viewMoreButton');
    const loginToSeeMoreButton = document.getElementById('loginToSeeMoreButton');

    if (viewMoreButton) {
        viewMoreButton.addEventListener('click', (e) => {
            e.preventDefault();
            loadTechnicians();
        });
    }

    if (loginToSeeMoreButton) {
        loginToSeeMoreButton.addEventListener('click', (e) => {
            e.preventDefault();
            window.location.href = '/LogIn';
        });
    }
});
