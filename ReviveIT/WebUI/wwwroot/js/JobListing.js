async function fetchData(url, options = {}) {
    try {
        const response = await fetch(url, options);
        if (!response.ok) throw new Error("Network response was not ok");
        return await response.json();
    } catch (error) {
        return null;
    }
}

function getCookie(name) {
    const cookies = document.cookie.split(";");
    for (let cookie of cookies) {
        const [key, value] = cookie.trim().split("=");
        if (key === name) return decodeURIComponent(value);
    }
    return null;
}

function openModal() {
    const modal = new bootstrap.Modal(document.getElementById("jobModal"));
    modal.show();
}

async function checkUserRole() {
    const data = await fetchData(`ProfileUpdate/api/info?type=role`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
    });
    return data ? data.role : null;
}

async function getUserCreatedJobs() {
    const data = await fetchData(`api/GetJobs/get-jobs-by-user-id`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
    });
    return data ? data.jobs || [] : [];
}

async function checkIfUserApplied(jobId) {
    const data = await fetchData(`/api/jobapplication/has-applied/${jobId}`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
    });
    return data
        ? { hasApplied: data.hasApplied, applicationId: data.applicationId }
        : { hasApplied: false, applicationId: null };
}

document.getElementById("applyFiltersButton").addEventListener("click", async () => {
    const keywords = document.getElementById("keywords").value.trim();
    const selectedCityId = document.getElementById("filterCities").value;
    const selectedCategoryId = document.getElementById("filterCategories").value;
    const price = document.getElementById("price").value.trim();
    const numberOfApplicants = document.getElementById("numberOfApplicants").value;
    const sortBy = document.getElementById("sortBy").value;

    const filterParams = {
        keywords,
        selectedCityId,
        selectedCategoryId,
        price,
        numberOfApplicants,
        sortBy,
    };

    await fetchAndFilterJobs(filterParams);
});

async function fetchAndFilterJobs(filters) {
    const userRole = await checkUserRole();
    const userCreatedJobs = await getUserCreatedJobs();

    const queryString = new URLSearchParams(filters).toString();

    const jobsData = await fetchData(`/api/GetJobs/get-all-jobs?${queryString}`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
    });

    if (jobsData && jobsData.jobs) {
        const updatedJobs = await Promise.all(
            jobsData.jobs.map(async (job) => {
                job.isUserCreated = userCreatedJobs.some(
                    (userJob) => userJob.jobID === job.jobID
                );

                const applicationStatus = await checkIfUserApplied(job.jobID);
                job.hasApplied = applicationStatus.hasApplied;
                job.applicationId = applicationStatus.applicationId;

                return job;
            })
        );

        populateJobsContainer(updatedJobs, userRole);
    }
}

async function getAllJobs() {
    const userRole = await checkUserRole();
    const userCreatedJobs = await getUserCreatedJobs();

    const jobsData = await fetchData(`/api/GetJobs/get-all-jobs`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
    });

    if (jobsData && jobsData.jobs) {
        const sortedJobs = jobsData.jobs.sort(
            (a, b) => new Date(b.createdAt) - new Date(a.createdAt)
        );

        const updatedJobs = await Promise.all(
            sortedJobs.map(async (job) => {
                job.isUserCreated = userCreatedJobs.some(
                    (userJob) => userJob.jobID === job.jobID
                );
                const applicationStatus = await checkIfUserApplied(job.jobID);
                job.hasApplied = applicationStatus.hasApplied;
                job.applicationId = applicationStatus.applicationId;
                return job;
            })
        );

        populateJobsContainer(updatedJobs, userRole);
    }
}

function populateJobsContainer(jobs, userRole) {
    const jobsContainer = document.getElementById("jobsContainer");
    jobsContainer.innerHTML = "";

    if (!jobs.length) {
        jobsContainer.innerHTML = `<p>No jobs found.</p>`;
        return;
    }

    jobs.forEach((job) => {
        const jobCard = createJobCard(job, userRole);
        jobsContainer.innerHTML += jobCard;
    });

    addJobEventListeners();
}

function createJobCard(job, userRole) {
    const cardStyle = `
        display: flex;
            flex-direction: column;
       border: 1px solid #ddd;
         border-radius: 8px;
          padding: 15px;
       margin-bottom: 15px;
      background-color: #fff;
          box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        transition: transform 0.2s ease, box-shadow 0.2s ease;

  `;

    const titleStyle = `
          font-size: 1.2em;
        font-weight: bold;
           margin-bottom: 10px;
          color: #333;
       `;


    const detailInfoStyle = `
         display: flex;
           flex-direction: column;
          gap: 5px;
          margin-bottom: 10px;
     `;
    const detailItemStyle = `
           font-size: 0.9em;
        color: #666;
        `;


    const iconStyle = `
           margin-right: 5px;
       color: #777;
      `;
    const optionBoxStyle = `
         margin-top: 10px;
         display: flex;
    justify-content: flex-end;
         align-items: center;
       `;

    const userJobMessageStyle = `
        font-style: italic;
        color: #888;
     `;

    const actionButtonStyle = `
          padding: 6px 10px;
         border: none;
      border-radius: 4px;
          cursor: pointer;
          color: white;
        margin-left: 4px;
         font-size: 0.8em;
          transition: background-color 0.2s ease;

      `;
    const detailsButtonStyle = `
         padding: 6px 10px;
            border: 1px solid #007bff;
           border-radius: 4px;
       cursor: pointer;
           color: #007bff;
         margin-left: 4px;
          font-size: 0.8em;
           transition:  0.2s ease;
  `;

    let descriptionArea = job.description ? `
        <div style="margin-bottom: 1em;  width: 100%;">
             <button class="description-btn" data-bs-toggle="collapse" style="${detailsButtonStyle} background-color:white; display: flex;    margin-right: auto;  margin-left: 0px;   align-items: center;    " data-bs-target="#descriptionField${job.jobID}"
                 onmouseover="this.style.backgroundColor='#f0f8ff';"
                     onmouseout="this.style.backgroundColor='white';"

       >Details
      </button>
    </div>` : "";
    return `
      <div class="col-lg-6">
          <div style="${cardStyle}"
             onmouseover="this.style.transform='scale(1.02)'; this.style.boxShadow='0 4px 8px rgba(0,0,0,0.2)';"
              onmouseout="this.style.transform='scale(1)'; this.style.boxShadow='0 2px 4px rgba(0,0,0,0.1)';">
               <div class="job_content-box">
                       <div class="detail-box">
                             <h5 style="${titleStyle}">${job.title || 'N/A'}</h5>
                             <div style="${detailInfoStyle}">
                                   <h6 style="${detailItemStyle}"><i class="fa fa-th-list" style="${iconStyle}" aria-hidden="true"></i><span>${job.categoryName || 'N/A'}</span></h6>
                                    <h6 style="${detailItemStyle}"><i class="fa fa-info-circle" style="${iconStyle}" aria-hidden="true"></i><span>${job.status || 'N/A'}</span></h6>
                                    <h6 style="${detailItemStyle};">
                                    <i class="fa fa-map-marker-alt" style="${iconStyle}"  aria-hidden="true"></i><span>${job.cityName || 'N/A'}</span></h6>
                                    <h6 style="${detailItemStyle}; "><i class="fa fa-dollar-sign" style="${iconStyle}" aria-hidden="true"></i><span>${job.price ? `${job.price}` : 'N/A'}</span></h6>
                                    <h6 style="${detailItemStyle}; "><i class="fa fa-user" style="${iconStyle}" aria-hidden="true"></i><span>Applicants: ${job.numberOfApplicants}</span></h6>
                            </div>
                               ${descriptionArea}
                            <div class="collapse" id="descriptionField${job.jobID}">
                                 <p style = "color: #333; padding: 1em"> ${job.description || 'N/A'} </p>
                           </div>
                     </div>
            </div>
            <div style="${optionBoxStyle}">
                        ${job.isUserCreated
            ? `<p style="${userJobMessageStyle}; margin-right:0; ">This is your job listing</p>`
            : createJobActionButtons(job, userRole, actionButtonStyle, userJobMessageStyle)
        }
             </div>
         </div>
    </div>
`;
}

function createJobActionButtons(job, userRole, buttonStyle, userJobMessageStyle) {
    const applyButtonStyle = `${buttonStyle} background-color: #28a745;`;
    const deleteButtonStyle = `${buttonStyle} background-color: #dc3545;`;

    if (userRole === "Technician" || userRole === "Company" || userRole === "Admin") {
        return job.isUserCreated ? ''
            : job.hasApplied
                ? `<button style="${deleteButtonStyle}" class="delete-btn" data-application-id="${job.applicationId
                }"  onmouseover="this.style.backgroundColor='#c82333';"
                 onmouseout="this.style.backgroundColor='#dc3545';"   >Revoke Application</button>`
                : `<button style="${applyButtonStyle}"  class="apply-btn" data-job-id="${job.jobID}"   onmouseover="this.style.backgroundColor='#1e7e34';"
                onmouseout="this.style.backgroundColor='#28a745';"  >Apply Now</button>`;
    }
    return '';
}

function createJobActionButtons(job, userRole, buttonStyle) {
    const applyButtonStyle = `${buttonStyle} background-color: #28a745;`;
    const deleteButtonStyle = `${buttonStyle} background-color: #dc3545;`;
    const userJobMessageStyle = `
   font-style: italic;
      color: #888;

`;

    if (userRole === "Technician" || userRole === "Company" || userRole === "Admin") {
        return job.isUserCreated ? '' : job.hasApplied
            ? `<button style="${deleteButtonStyle}" class="delete-btn" data-application-id="${job.applicationId
            }"      onmouseover="this.style.backgroundColor='#c82333';"
              onmouseout="this.style.backgroundColor='#dc3545';"

                      >Revoke Application</button>`
            : `<button  style="${applyButtonStyle}" class="apply-btn" data-job-id="${job.jobID}" onmouseover="this.style.backgroundColor='#1e7e34';"
                   onmouseout="this.style.backgroundColor='#28a745';"
                      >Apply Now</button>`;
    }

    return '';
}

function addJobEventListeners() {
    document.querySelectorAll(".delete-btn").forEach((button) => {
        button.addEventListener("click", deleteJobApplication);
    });

    document.querySelectorAll(".apply-btn").forEach((button) => {
        button.addEventListener("click", applyForJob);
    });
}

async function deleteJobApplication(event) {
    const applicationId = event.target.getAttribute("data-application-id");
    const response = await fetchData(
        `/api/jobapplication/delete-job-application/${applicationId}`,
        {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
                "Content-Type": "application/json",
            },
        }
    );

    if (response) {
        getAllJobs();
    }
}

async function applyForJob(event) {
    const jobId = event.target.getAttribute("data-job-id");
    const response = await fetchData(`/api/jobapplication/apply-for-job/${jobId}`, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ jobID: jobId }),
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
        title: document.getElementById("jobTitle").value.trim(),
        description: document.getElementById("jobDescription").value.trim(),
        status: "Available",
        createdAt: new Date().toISOString(),
        categoryId: parseInt(document.getElementById("jobCategory").value.trim(), 10),
        cityId: parseInt(document.getElementById("jobCity").value.trim(), 10),
        price: parseFloat(document.getElementById("jobPrice").value) || 0,
    };

    const response = await fetchData(`/api/Job/create-job`, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${getCookie("jwtToken")}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify(jobData),
    });

    if (response) {
        const modal = new bootstrap.Modal(document.getElementById("jobModal"));
        modal.hide();
        document.getElementById("jobForm").reset();
        await getAllJobs();
    }

    isJobPostingInProgress = false;
}

function fetchCategories(dropdownId) {
    fetch("/api/categories/getCategories")
        .then((response) => response.json())
        .then((data) => {
            const dropdown = document.getElementById(dropdownId);
            data.forEach((category) => {
                const option = document.createElement("option");
                option.value = category.categoryID;
                option.textContent = category.name;
                dropdown.appendChild(option);
            });
        })
        .catch((error) => console.error("Error fetching categories:", error));
}

function fetchCities(dropdownId) {
    fetch("/api/city/getCities")
        .then((response) => response.json())
        .then((data) => {
            const dropdown = document.getElementById(dropdownId);
            data.forEach((city) => {
                const option = document.createElement("option");
                option.value = city.cityId;
                option.textContent = city.cityName;
                dropdown.appendChild(option);
            });
        })
        .catch((error) => console.error("Error fetching cities:", error));
}

document.getElementById("resetFiltersButton").addEventListener("click", () => {
    document.getElementById("filtersForm").reset();
    fetchAndFilterJobs({});
});

document.addEventListener("DOMContentLoaded", function () {
    fetchCategories("jobCategory");
    fetchCities("jobCity");

    fetchCategories("filterCategories");
    fetchCities("filterCities");

    getAllJobs();

    const jobForm = document.getElementById("jobForm");
    if (jobForm) {
        jobForm.removeEventListener("submit", submitJobForm);
        jobForm.addEventListener("submit", submitJobForm);
    }
});