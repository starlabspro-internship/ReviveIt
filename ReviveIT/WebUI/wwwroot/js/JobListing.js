const apiUrls = {
    getJobs: "https://localhost:7018/api/GetJobs",
    jobPost: "https://localhost:7018/api/Job",
    jobApplications: "https://localhost:7018/api/jobapplication",
    userInfo: "https://localhost:7018/api/ProfileUpdate/info"
};

async function fetchData(url, options = {}) {
    try {
        const response = await fetch(url, options);
        if (!response.ok) throw new Error('Network response was not ok');
        return await response.json();
    } catch (error) {
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

async function checkUserRole() {
    const data = await fetchData(`${apiUrls.userInfo}?type=role`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });
    return data ? data.role : null;
}

async function getUserCreatedJobs() {
    const data = await fetchData(`${apiUrls.getJobs}/get-jobs-by-user-id`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });
    return data ? data.jobs || [] : [];
}

async function checkIfUserApplied(jobId) {
    const data = await fetchData(`${apiUrls.jobApplications}/has-applied/${jobId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });
    return data ? { hasApplied: data.hasApplied, applicationId: data.applicationId } : { hasApplied: false, applicationId: null };
}

async function getAllJobs() {
    const userRole = await checkUserRole();
    const userCreatedJobs = await getUserCreatedJobs();

    const jobsData = await fetchData(`${apiUrls.getJobs}/get-all-jobs`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });

    if (jobsData && jobsData.jobs) {
        const sortedJobs = jobsData.jobs.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));

        const updatedJobs = await Promise.all(sortedJobs.map(async (job) => {
            job.isUserCreated = userCreatedJobs.some(userJob => userJob.jobID === job.jobID);
            const applicationStatus = await checkIfUserApplied(job.jobID);
            job.hasApplied = applicationStatus.hasApplied;
            job.applicationId = applicationStatus.applicationId;
            return job;
        }));

        populateJobsContainer(updatedJobs, userRole);
    }
}

function populateJobsContainer(jobs, userRole) {
    const jobsContainer = document.getElementById('jobsContainer');
    jobsContainer.innerHTML = '';

    if (!jobs.length) {
        jobsContainer.innerHTML = `<p>No jobs found.</p>`;
        return;
    }

    jobs.forEach(job => {
        const jobCard = createJobCard(job, userRole);
        jobsContainer.innerHTML += jobCard;
    });

    addJobEventListeners();
}

function createJobCard(job, userRole) {
    return `
        <div class="col-lg-6">
            <div class="box">
                <div class="job_content-box">
                    <div class="detail-box">
                        <h5>${job.title || 'N/A'}</h5>
                        <div class="detail-info">
                            <h6><i class="fa fa-th-list" aria-hidden="true"></i><span>${job.categoryName || 'N/A'}</span></h6>
                            <h6><i class="fa fa-info-circle" aria-hidden="true"></i><span>${job.status || 'N/A'}</span></h6>
                            <h6><i class="fa fa-list" aria-hidden="true"></i><span>${job.description || 'N/A'}</span></h6>
                        </div>
                    </div>
                </div>
                <div class="option-box" style="margin-top: 1em">
                    ${job.isUserCreated ? `<p class="text-muted">This is your job listing</p>` : createJobActionButtons(job, userRole)}
                </div>
            </div>
        </div>
    `;
}

function createJobActionButtons(job, userRole) {
    if (userRole === 'Technician' || userRole === 'Company' || userRole === 'Admin') {
        return job.hasApplied
            ? `<button class="delete-btn" data-application-id="${job.applicationId}">Revoke Application</button>`
            : `<button class="apply-btn" data-job-id="${job.jobID}">Apply Now</button>`;
    }
    return '';
}

function addJobEventListeners() {
    document.querySelectorAll('.delete-btn').forEach(button => {
        button.addEventListener('click', deleteJobApplication);
    });

    document.querySelectorAll('.apply-btn').forEach(button => {
        button.addEventListener('click', applyForJob);
    });
}

async function deleteJobApplication(event) {
    const applicationId = event.target.getAttribute('data-application-id');
    const response = await fetchData(`${apiUrls.jobApplications}/delete-job-application/${applicationId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        }
    });

    if (response) {
        getAllJobs();
    }
}

async function applyForJob(event) {
    const jobId = event.target.getAttribute('data-job-id');
    const response = await fetchData(`${apiUrls.jobApplications}/apply-for-job/${jobId}`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ jobID: jobId })
    });

    if (response) {
        getAllJobs();
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
        price: parseFloat(document.getElementById('jobPrice').value) || 0
    };

    const response = await fetchData(`${apiUrls.jobPost}/create-job`, {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jobData)
    });

    if (response) {
        const modal = new bootstrap.Modal(document.getElementById('jobModal'));
        modal.hide();
        document.getElementById('jobForm').reset();
        await getAllJobs();
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

document.addEventListener('DOMContentLoaded', function () {
    fetchCategories();
    getAllJobs();

    const jobForm = document.getElementById('jobForm');
    if (jobForm) {
        jobForm.removeEventListener('submit', submitJobForm);
        jobForm.addEventListener('submit', submitJobForm);
    }
});