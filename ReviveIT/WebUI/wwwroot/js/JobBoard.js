document.getElementById("addJobBtn").addEventListener("click", () => {
    openModal();
});

async function fetchData(url, options = {}) {
    try {
        const response = await fetch(url, options);
        if (!response.ok) throw new Error('Network response was not ok');
        return await response.json();
    } catch (error) {
        console.error("Error fetching data:", error);
        return null;
    }
}

function getCookie(name) {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
        const [key, value] = cookie.trim().split('=');
        if (key === name) return decodeURIComponent(value);
    }
    return null;
}

function openModal() {
    const modal = new bootstrap.Modal(document.getElementById('jobModal'));
    modal.show();
}

async function getAllJobs() {
    const jobsData = await fetchData(`/api/GetJobs/get-jobs-by-user-id`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });

    if (jobsData && jobsData.jobs) {
        const sortedJobs = jobsData.jobs.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
        populateJobsContainer(sortedJobs);
    } else {
        populateJobsContainer([]);
    }
}

function populateJobsContainer(jobs) {
    const jobsContainer = document.getElementById('jobsContainer');
    jobsContainer.innerHTML = '';

    if (!jobs.length) {
        jobsContainer.innerHTML = `<p>No jobs found.</p>`;
        return;
    }

    jobs.forEach(job => {
        const jobCard = createJobCard(job);
        jobsContainer.innerHTML += jobCard;
    });

    addJobEventListeners();
}

function createJobCard(job) {
    return `
    <div class="col-md-6 mb-4" >
        <div class="shadow-sm border-0 h-100" style="background: white; border-radius: 15px;">
            <div class="card-body">
                <div class="d-flex justify-content-between align-items-center mb-2">
                    <h5 class="card-title mb-0">${job.title || 'N/A'}</h5>
                    <span class="badge bg-light text-dark">${job.company || 'N/A'}</span>
                </div>
                <p class="text-muted mb-1"><i class="fa fa-map-marker-alt" aria-hidden="true"></i> ${job.cityName || 'N/A'}</p>
                <p class="mb-3"><strong>${job.price || 'N/A'}</strong> •
                    <i class="fa fa-level-up-alt" aria-hidden="true"></i> ${job.status || 'N/A'} •
                    <i class="fa fa-briefcase" aria-hidden="true"></i> ${job.categoryName || 'N/A'}</p>
                <button class="btn btn-outline-primary btn-sm details-btn" data-job-id="${job.jobID}" data-bs-toggle="collapse" data-bs-target="#descriptionField${job.jobID}">Details</button>
                <div class="collapse" id="descriptionField${job.jobID}">
                    <p>${job.description || 'N/A'}</p>
                    <div class="applicant-list" id="applicantList${job.jobID}"></div>
                </div>

                <div class="option-box" style="margin-top: 1em">
                    <button class="btn btn-danger delete-btn" data-job-id="${job.jobID}">Delete Job</button>
                </div>
            </div>
        </div>
    </div>
    `;
}

function addJobEventListeners() {
    document.querySelectorAll('.delete-btn').forEach(button => {
        button.addEventListener('click', deleteJobPost);
    });

    document.querySelectorAll('.details-btn').forEach(button => {
        button.addEventListener('click', function (event) {
            const jobId = event.currentTarget.getAttribute('data-job-id');
            const applicantListElement = event.currentTarget.closest('.col-md-6').querySelector('.applicant-list');
            fetchApplicants(jobId, applicantListElement);
        });
    });
}

async function fetchApplicants(jobId, applicantListElement) {
    const applicants = await fetchData(`/api/JobApplication/get-job-applications/${jobId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });

    if (applicants) {
        applicantListElement.innerHTML = '';
        const selectedApplicant = applicants.find(applicant => applicant.status === "Selected");
        applicants.forEach(applicant => {
            applicantListElement.innerHTML += createApplicantCard(applicant, jobId, selectedApplicant);
        });
        addApplicantEventListeners();
    } else {
        applicantListElement.innerHTML = "<p>No applicants found</p>";
    }
}

function createApplicantCard(applicant, jobId, selectedApplicant) {
    const isSelected = selectedApplicant && selectedApplicant.applicationID === applicant.applicationID;
    return `
             <div class="applicant-card">
                  <a href="/ProsProfileView/${applicant.applicantUserId}" class="applicant-name-link" target="_blank">
                  ${applicant.applicantName || 'N/A'}
                   </a>
                      ${isSelected ?
            `<button class="btn btn-sm btn-secondary select-applicant-btn select-applicant-btn-selected"
                        data-job-id="${jobId}"
                         data-application-id="${applicant.applicationID}" disabled>Selected
                            </button>`
            : `<button class="btn btn-sm btn-success select-applicant-btn"
                             data-job-id="${jobId}"
                             data-application-id="${applicant.applicationID}">Select Applicant
                         </button>`
        }
             </div>`;
}

let isSelectingApplicant = false;
function addApplicantEventListeners() {
    document.querySelectorAll('.select-applicant-btn').forEach(button => {
        button.addEventListener('click', selectApplicant);
    });
}

async function selectApplicant(event) {
    if (isSelectingApplicant) return;
    isSelectingApplicant = true;
    const button = event.target;
    const jobId = event.target.getAttribute('data-job-id');
    const applicationId = event.target.getAttribute('data-application-id');

    const response = await fetchData(`/api/JobApplication/select-job-applicant/${applicationId}`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });

    if (response && response.success) {
        button.textContent = 'Selected';
        button.classList.remove("btn-success")
        button.classList.add("btn-secondary");
        button.disabled = true;
        const applicantListElement = button.closest('.applicant-list')
        fetchApplicants(jobId, applicantListElement)
        alert(`Applicant ${applicationId} has been selected for job ${jobId}.`);
    } else {
        alert('Error selecting the user');
    }
    isSelectingApplicant = false;
}

async function deleteJobPost(event) {
    const jobId = event.target.getAttribute('data-job-id');

    const response = await fetchData(`/api/Job/delete-job/${jobId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });
    if (response && response.success) {
        getAllJobs();
    } else {
        alert("There was an error deleting the job");
    }
}

let isJobPostingInProgress = false;
async function submitJobForm(event) {
    event.preventDefault();

    if (isJobPostingInProgress) return;
    isJobPostingInProgress = true;

    const jobData = {
        jobID: 0,
        title: document.getElementById('jobTitle').value.trim(),
        description: document.getElementById('jobDescription').value.trim(),
        status: "Available",
        createdAt: new Date().toISOString(),
        categoryId: parseInt(document.getElementById('jobCategory').value.trim(), 10),
        cityId: parseInt(document.getElementById('jobCity').value.trim(), 10),
        price: parseFloat(document.getElementById('jobPrice').value) || 0
    };

    const response = await fetchData(`/api/Job/create-job`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jobData)
    });

    if (response) {
        getAllJobs();
        bootstrap.Modal.getInstance(document.getElementById('jobModal')).hide();
    }

    isJobPostingInProgress = false;
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

function fetchCities() {
    fetch('/api/city/getCities')
        .then(response => response.json())
        .then(data => {
            const citiesDropdown = document.getElementById('jobCity');
            data.forEach(city => {
                const option = document.createElement('option');
                option.value = city.cityId;
                option.textContent = city.cityName;
                citiesDropdown.appendChild(option);
            })
        })
}

document.addEventListener('DOMContentLoaded', function () {
    fetchCategories();
    fetchCities();
    getAllJobs();

    const jobForm = document.getElementById('jobForm');
    if (jobForm) {
        jobForm.removeEventListener('submit', submitJobForm);
        jobForm.addEventListener('submit', submitJobForm);
    }
});