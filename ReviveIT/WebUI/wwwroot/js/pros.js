﻿let skipCount = 0;
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

function getFilterParameters(useUrl = false) {
    if (useUrl) {
        const urlParams = new URLSearchParams(window.location.search);
        return {
            keywords: urlParams.get('keywords'),
            selectedCityId: urlParams.get('SelectedCityId'),
            selectedCategoryId: urlParams.get('SelectedCategoryId'),
        };
    } else {
        return {
            keywords: document.getElementById('keywords').value || null,
            selectedCityId: document.getElementById('filterCities').value || null,
            selectedCategoryId: document.getElementById('filterCategories').value || null,
        };
    }
}

async function loadTechnicians(clear = false, useUrl = false) {
    if (clear) {
        skipCount = 0;
    }
    const takeCount = skipCount === 0 ? initialPageSize : additionalPageSize;

    const { keywords, selectedCityId, selectedCategoryId } = getFilterParameters(useUrl);

    const params = new URLSearchParams({
        skipCount,
        takeCount,
        ...(keywords && { keywords }),
        ...(selectedCityId && { selectedCityId }),
        ...(selectedCategoryId && {selectedCategoryId})
    });

    const response = await fetch(`/Pros/api/GetPros?${params.toString()}`, {
        credentials: 'include',
    });

    if (!response.ok) {
        const errorData = await response.json();
        alert(`Error: ${errorData.message}`);
        return;
    }

    const data = await response.json();

    if (clear) {
        document.getElementById('technicianContainer').innerHTML = '';
    }

    if (skipCount === 0) {
        document.getElementById('technicianContainer').innerHTML = '';
    }

    renderTechnicians(data.data);

    const container = document.getElementById('technicianContainer');
    const totalRendered = container.childElementCount;

    const viewMoreButton = document.getElementById('viewMoreButton');

    if (data.total <= initialPageSize || totalRendered >= data.total) {
        viewMoreButton.style.display = 'none';
    } else {
        viewMoreButton.style.display = 'block';
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
                        <img src="${technician.profilePicture}" alt="${technician.fullName}'s Profile Picture" style="width: 100%; height: auto; object-fit: cover; color:black"/>
                    </div>
                    <div class="detail-box" style="background-color:white">
                        <a href="${profileLink}" style="color:black">${technician.fullName}</a>
                        <h6 class="expert_position">
                            <span style="color:black">${technician.expertise}</span>
                            <span style="color:black" class="text-right">${technician.experience} Years of Experience</span>
                        </h6>
                        <span class="expert_rating" style="color:black">
                            ${generateStars(technician.rating)}
                        </span>
                        <p style="color:black">${technician.review}</p>
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

function fetchCategories(selectedCategoryId = null) {
    fetch('/api/categories/getCategories')
        .then(response => response.json())
        .then(data => {
            const jobCategoryDropdown = document.getElementById('filterCategories');
            data.forEach(category => {
                const option = document.createElement('option');
                option.value = category.categoryID;
                option.textContent = category.name;
                jobCategoryDropdown.appendChild(option);
            });
            if (selectedCategoryId) {
                jobCategoryDropdown.value = selectedCategoryId;
            }
        })
        .catch(error => console.error('Error fetching categories:', error));
}

function fetchCitites(selectedCityId = null) {
    fetch('/api/city/getCities')
        .then(response => response.json())
        .then(data => {
            const citiesDropdown = document.getElementById('filterCities');
            data.forEach(city => {
                const option = document.createElement('option');
                option.value = city.cityId;
                option.textContent = city.cityName;
                citiesDropdown.appendChild(option);
            });
            if (selectedCityId) {
                citiesDropdown.value = selectedCityId;
            }
        })
}

document.addEventListener('DOMContentLoaded', async () => {
    await checkAuthenticationStatus();

    const { keywords, selectedCityId, selectedCategoryId } = getFilterParameters(true);

    await Promise.all([
        fetchCategories(selectedCategoryId),
        fetchCitites(selectedCityId),
    ]);
    if (keywords) document.getElementById('keywords').value = keywords;

    loadTechnicians(true, true);

    document.getElementById('applyFiltersButton').addEventListener('click', (e) => {
        e.preventDefault();
        skipCount = 0;
        loadTechnicians(true, false);
    });

    const viewMoreButton = document.getElementById('viewMoreButton');

    if (viewMoreButton) {
        viewMoreButton.addEventListener('click', (e) => {
            e.preventDefault();
            loadTechnicians(false);
        });
    }
});