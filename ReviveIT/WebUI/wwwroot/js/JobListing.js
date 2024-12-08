﻿const apiUrl = "https://localhost:7018/api/GetJobs";
const apiJobPostUlr = "https://localhost:7018/api/Job";

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
}

async function getAllJobs() {
    try {
        const response = await fetch(`${apiUrl}/get-all-jobs`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const result = await response.json();
            populateJobsContainer(result.jobs);
        } else {
            alert("Failed to fetch jobs. Please try again.");
        }
    } catch (error) {
        alert("An error occurred while fetching jobs.");
    }
}

function populateJobsContainer(jobs) {
    const jobsContainer = document.getElementById('jobsContainer');
    jobsContainer.innerHTML = '';

    if (!Array.isArray(jobs) || jobs.length === 0) {
        jobsContainer.innerHTML = `<p>No jobs found.</p>`;
        return;
    }

    jobs.forEach(job => {
        const jobCard = `
            <div class="col-lg-6">
                <div class="box">
                    <div class="job_content-box">
                        <div class="detail-box">
                            <h5>${job.title || 'N/A'}</h5>
                            <div class="detail-info">
                                <h6 style="width: 35%;">
                                    <i class="fa fa-th-list" aria-hidden="true"></i>
                                    <span>${job.categoryName || 'N/A'}</span>
                                </h6>
                                <h6 style="width: 15%;">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i>
                                    <span>${job.status || 'N/A'}</span>
                                </h6>
                                <h6 style="width: 50%;">
                                    <i class="fa fa-list" aria-hidden="true"></i>
                                    <span>${job.description || 'N/A'}</span>
                                </h6>
                            </div>
                        </div>
                    </div>
                    <div class="option-box"  style="margin-top: 1em">
                        <a href="#" class="apply-btn">
                            Apply Now
                        </a>
                    </div>
                </div>
            </div>
        `;

        jobsContainer.innerHTML += jobCard;
    });
}

function fetchCategories() {
    fetch('/api/categories/getCategories')
        .then(response => response.json())
        .then(data => {
            const jobCategoryDropdown = document.getElementById('jobCategory');
            data.forEach(category => {
                const option = document.createElement('option');
                option.value = category.categoryID;
                option.textContent = category.name;
                jobCategoryDropdown.appendChild(option);
            });
        })
        .catch(error => console.error('Error fetching categories:', error));
}

async function submitJobForm(event) {
    event.preventDefault();

    const jobTitle = document.getElementById('jobTitle').value.trim();
    const jobDescription = document.getElementById('jobDescription').value.trim();
    const jobCategory = document.getElementById('jobCategory').value.trim();
    const jobPrice = parseFloat(document.getElementById('jobPrice').value) || 0;

    const jobData = {
        jobID: 0,
        title: jobTitle,
        description: jobDescription,
        status: "Available",
        createdAt: new Date().toISOString(),
        categoryId: parseInt(jobCategory, 10),
        price: jobPrice,
    };

    const jobForm = document.getElementById('jobForm');
    const spinner = document.getElementById('jobSpinner');

    jobForm.style.display = "none";
    spinner.style.display = "block";

    try {
        const response = await fetch(`${apiJobPostUlr}/create-job`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(jobData),
        });

        const result = await response.json();

        spinner.style.display = "none";

        if (response.ok) {
            alert('Job posted successfully!');
            const modal = new bootstrap.Modal(document.getElementById('jobModal'));
            modal.hide();
            jobForm.reset();
            getAllJobs();
        } else {
            jobForm.style.display = "block";
            alert(`Error: ${result.message}`);
        }
    } catch (error) {
        console.error("Error while posting job:", error);
        alert('An error occurred while posting the job.');
        jobForm.style.display = "block";
        spinner.style.display = "none";
    }
}

document.addEventListener('DOMContentLoaded', function () {
    fetchCategories();
    getAllJobs();

    const jobForm = document.getElementById('jobForm');
    jobForm.addEventListener('submit', submitJobForm);
});
