﻿@{
    ViewData["Title"] = "Job Board";
}

@section Styles {
    <link href="~/css/JobBoard.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet" />
}

@section Scripts {
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
}

<div class="hero_area">
    <section class="slider_section">
        <div class="container-fluid py-5">
            <button class="slide-tab" type="button" data-bs-toggle="offcanvas" data-bs-target="#historyTab" aria-controls="historyTab">
                History
            </button>

            <div class="offcanvas offcanvas-end" tabindex="-1" id="historyTab" aria-labelledby="historyTabLabel">
                <div class="offcanvas-header">
                    <h5 id="historyTabLabel">History</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div class="offcanvas-body">
                    <div id="historyContent" class="text-muted">Empty</div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3 mb-4">
                    <div class="card shadow-sm p-4">
                        <h4 class="mb-4">Filters</h4>
                        <div class="filter-group">
                            @foreach (var filter in new[] { "Working Schedule", "Employment Type", "Experience Level", "Location", "Job Category" })
                            {
                                <div class="mb-4">
                                    <h6 class="text-muted d-flex justify-content-between align-items-center">
                                        <span>@filter</span>
                                        <button class="btn btn-link p-0 toggle-filter" data-filter="#@filter.Replace(" ", "")">
                                            <div class="arrow-button">
                                                <i class="bi bi-chevron-right"></i>
                                            </div>
                                        </button>
                                    </h6>
                                    <div id="@filter.Replace(" ", "")" class="filter-options collapse">
                                        @{
                                            var options = filter switch
                                            {
                                                "Working Schedule" => new[] { "Morning", "Afternoon", "Night", "Flexible" },
                                                "Employment Type" => new[] { "Full time", "Part time", "Contract", "Temporary", "Freelance" },
                                                "Experience Level" => new[] { "Entry Level", "Mid Level", "Senior Level", "Executive" },
                                                "Location" => new[] { "Remote", "On-site", "Hybrid", "International" },
                                                "Job Category" => new[] { "Technology", "Finance", "Healthcare", "Marketing", "Education" },
                                                _ => new[] { "Option 1", "Option 2", "Option 3" }
                                            };
                                        }
                                        @foreach (var option in options)
                                        {
                                            <div class="form-check">
                                                <input type="checkbox" class="form-check-input" id="@option.Replace(" ", "")" />
                                                <label class="form-check-label" for="@option.Replace(" ", "")">@option</label>
                                            </div>
                                        }
                                    </div>
                                </div>
                            }
                        </div>

                        <div class="mb-4">
                            <h6 class="text-muted">Salary Range</h6>
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <select class="form-select w-auto" id="salaryType">
                                    <option value="hourly">Per Hour</option>
                                    <option value="fixed">Fixed Salary</option>
                                </select>
                            </div>

                            <div class="mb-2">
                                <button class="btn btn-link text-decoration-none p-0 d-flex align-items-center" type="button" data-bs-toggle="collapse" data-bs-target="#salarySettings" aria-expanded="false" aria-controls="salarySettings">
                                    <span>Salary Settings</span>
                                    <div class="circle-icon ms-2">
                                        <i class="bi bi-chevron-down"></i>
                                    </div>
                                </button>

                                <div id="salarySettings" class="collapse mt-2">
                                    <div class="mb-2">
                                        <label for="minValue" class="form-label">Minimum:</label>
                                        <input type="number" id="minValue" class="form-control w-100" value="30000" />
                                    </div>
                                    <div class="mb-2">
                                        <label for="maxValue" class="form-label">Maximum:</label>
                                        <input type="number" id="maxValue" class="form-control w-100" value="200000" />
                                    </div>
                                    <div class="mb-2">
                                        <label for="stepValue" class="form-label">Step:</label>
                                        <input type="number" id="stepValue" class="form-control w-100" value="1000" />
                                    </div>
                                </div>
                            </div>

                            <input type="range" class="form-range" id="salaryRange" min="30000" max="200000" step="1000" />
                            <div class="d-flex justify-content-between">
                                <span id="minSalary">$30,000</span>
                                <span id="maxSalary">$200,000</span>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-9">
                    <div class="card shadow-sm p-4" style="min-height: 600px;">
                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <div>
                                <span class="text-muted me-2">Sort by:</span>
                                <select class="form-select d-inline-block w-auto">
                                    <option value="latest">Last updated</option>
                                    <option value="salary">In Progress</option>
                                    <option value="salary">Most Applicants</option>
                                </select>
                            </div>
                            <button id="addJobBtn" class="btn btn-primary">Add Job</button>
                        </div>

                        <div class="row g-4" id="jobsContainer">
                        </div>
                    </div>
                </div>
            </div >
        </div >
    </section >

    <script>
        let history = [];

        document.getElementById("addJobBtn").addEventListener("click", () => {
            const jobDetails = {
            title: prompt("Enter job title:"),
        company: prompt("Enter company name:"),
        location: prompt("Enter location:"),
        pay: prompt("Enter pay rate:"),
        level: prompt("Enter job level:"),
        type: prompt("Enter job type:")
            };

        if (jobDetails.title && jobDetails.company) {
            history.push(jobDetails);

        const jobCard = `
        <div class="col-md-6 mb-4">
            <div class="box shadow-sm border-0 h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center mb-2">
                        <h5 class="card-title mb-0">${jobDetails.title}</h5>
                        <span class="badge bg-light text-dark">${jobDetails.company}</span>
                    </div>
                    <p class="text-muted mb-1"><i class="fa fa-map-marker-alt" aria-hidden="true"></i> ${jobDetails.location}</p>
                    <p class="mb-3"><strong>${jobDetails.pay}</strong> •
                        <i class="fa fa-level-up-alt" aria-hidden="true"></i> ${jobDetails.level} •
                        <i class="fa fa-briefcase" aria-hidden="true"></i> ${jobDetails.type}</p>
                    <a href="#" class="btn btn-outline-primary btn-sm">Details</a>
                </div>
            </div>
        </div>
        `;

        document.getElementById("jobsContainer").innerHTML += jobCard;
        updateHistory();
            }
        });

        function updateHistory() {
            const historyContent = document.getElementById("historyContent");
        historyContent.innerHTML = history.length
                ? history.map((job, index) => `<p><strong>${index + 1}.</strong> ${job.title} at ${job.company}</p>`).join("")
        : "Empty";
        }

            document.querySelectorAll('.toggle-filter').forEach(button => {
            button.addEventListener('click', function () {
                const filterId = this.dataset.filter;
                const icon = this.querySelector('.arrow-button i');
                const filterDiv = document.querySelector(filterId);

                $(filterDiv).collapse('toggle');
                icon.classList.toggle('rotated');
            });
            });

        const salaryRange = document.getElementById('salaryRange');
        const minSalary = document.getElementById('minSalary');
        const maxSalary = document.getElementById('maxSalary');
        const salaryType = document.getElementById('salaryType');
        const minValue = document.getElementById('minValue');
        const maxValue = document.getElementById('maxValue');
        const stepValue = document.getElementById('stepValue');

        salaryRange.addEventListener('input', function () {
                const value = salaryRange.value;
        if (salaryType.value === 'hourly') {
            minSalary.textContent = `$${value} per hour`;
                } else {
            minSalary.textContent = `$${value}`;
                }
            });

        salaryType.addEventListener('change', function () {
                const value = salaryRange.value;
        if (salaryType.value === 'hourly') {
            minSalary.textContent = `$${value} per hour`;
        maxSalary.textContent = `$${salaryRange.max} per hour`;
                } else {
            minSalary.textContent = `$${value}`;
        maxSalary.textContent = `$${salaryRange.max}`;
                }
            });

        function updateRange() {
            salaryRange.min = minValue.value;
        salaryRange.max = maxValue.value;
        salaryRange.step = stepValue.value;
        minSalary.textContent = `$${salaryRange.min}`;
        maxSalary.textContent = `$${salaryRange.max}`;
            }

        minValue.addEventListener('input', updateRange);
        maxValue.addEventListener('input', updateRange);
        stepValue.addEventListener('input', updateRange);



        async function fetchData(url, options = { }) {
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
            const data = await fetchData(`ProfileUpdate/api/info?type=role`, {
            method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
        'Content-Type': 'application/json'
                }
            });
        return data ? data.role : null;
        }

        async function getUserCreatedJobs() {
            const data = await fetchData(`api/GetJobs/get-jobs-by-user-id`, {
            method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
        'Content-Type': 'application/json'
                }
            });
        return data ? data.jobs || [] : [];
        }

        async function checkIfUserApplied(jobId) {
            const data = await fetchData(`/api/jobapplication/has-applied/${jobId}`, {
            method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
        'Content-Type': 'application/json'
                }
            });
        return data ? {hasApplied: data.hasApplied, applicationId: data.applicationId } : {hasApplied: false, applicationId: null };
        }

        async function getAllJobs() {
            const userRole = await checkUserRole();
        const userCreatedJobs = await getUserCreatedJobs();

        const jobsData = await fetchData(`/api/GetJobs/get-all-jobs`, {
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
        <div class="col-md-6 mb-4">
            <div class="shadow-sm border-0 h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center mb-2">
                        <h5 class="card-title mb-0">${job.title || 'N/A'}</h5>
                        <span class="badge bg-light text-dark">${job.company || 'N/A'}</span>
                    </div>
                    <p class="text-muted mb-1"><i class="fa fa-map-marker-alt" aria-hidden="true"></i> ${job.cityName || 'N/A'}</p>
                    <p class="mb-3"><strong>${job.pay || 'N/A'}</strong> •
                        <i class="fa fa-level-up-alt" aria-hidden="true"></i> ${job.status} •
                        <i class="fa fa-briefcase" aria-hidden="true"></i> ${job.categoryName}</p>
                    <a href="#" class="btn btn-outline-primary btn-sm" data-bs-toggle="collapse" data-bs-target="#descriptionField">Details</a>
                    <div class="collapse" id="descriptionField">${job.description || 'N/A'}
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
        const response = await fetchData(`/api/jobapplication/delete-job-application/${applicationId}`, {
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
        const response = await fetchData(`/api/jobapplication/apply-for-job/${jobId}`, {
            method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookie('jwtToken')}`,
        'Content-Type': 'application/json'
                },
        body: JSON.stringify({jobID: jobId })
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
        cityId: parseInt(document.getElementById('jobCity').value.trim(), 10),
        price: parseFloat(document.getElementById('jobPrice').value) || 0
            };


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
    </script>
</div >
