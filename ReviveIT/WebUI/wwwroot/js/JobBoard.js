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
            <p class="mb-3"><strong>${jobDetails.pay}</strong> • <i class="fa fa-level-up-alt" aria-hidden="true"></i> ${jobDetails.level} • <i class="fa fa-briefcase" aria-hidden="true"></i> ${jobDetails.type}</p>
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

document.addEventListener("DOMContentLoaded", () => {
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
};