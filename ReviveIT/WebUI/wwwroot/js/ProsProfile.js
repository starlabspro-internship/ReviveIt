$(document).ready(function () {
    if (!technicianId) {
        return;
    }

    const profileUrl = `https://localhost:7018/api/prosprofileapi/GetTechnicianProfile/${technicianId}`;

    $.ajax({
        url: profileUrl,
        method: "GET",
        success: function (data) {
            if (data) {
                displayTechnicianProfile(data);
            }
        },
        error: function (xhr) {
        }
    });

    function displayTechnicianProfile(data) {
        $("#profilePicture").attr("src", data.profilePicture || "https://via.placeholder.com/150");
        $("#technicianFullName").text(data.fullName || "No Name Provided");
        $("#technicianExpertise").text(data.expertise || "No Expertise Provided");
        $("#technicianExperience").text(`Experience: ${data.experience || "No Experience Available"}`);

        displayPortfolios(data.portfolios);

        $("#technicianDescription").html(data.description ? `<h4>Description</h4><p>${data.description}</p>` : "<p>No description available.</p>");

        $("#technicianContact").html(`
            <h4>Contact Information</h4>
            <p>Email: ${data.email || "No Email Provided"}</p>
            <p>Phone: ${data.phoneNumber || "No Phone Number Provided"}</p>
        `);
    }

    function displayPortfolios(portfolios) {
        if (Array.isArray(portfolios) && portfolios.length > 0) {
            let portfolioHtml = portfolios.map(portfolio => {
                if (portfolio.filePath.match(/\.(jpeg|jpg|gif|png)$/)) {
                    return createPortfolioImage(portfolio);
                }
                if (portfolio.filePath.match(/\.(pdf|docx|pptx)$/)) {
                    return createPortfolioDocument(portfolio);
                }
                return createPortfolioLink(portfolio);
            }).join("");

            $("#technicianPortfolio").html(`<h4>Portfolio</h4>${portfolioHtml}`);
        } else {
            $("#technicianPortfolio").html("<p>No portfolios available.</p>");
        }
    }

    function createPortfolioImage(portfolio) {
        return `<div class="portfolio-item">
                    <h5>${portfolio.title}</h5>
                    <img src="${portfolio.filePath}" alt="${portfolio.title}" class="portfolio-image" />
                </div>`;
    }

    function createPortfolioDocument(portfolio) {
        return `<div class="portfolio-item">
                    <h5>${portfolio.title}</h5>
                    <embed src="${portfolio.filePath}" width="100%" height="400px" type="application/pdf" />
                    <p><a href="${portfolio.filePath}" target="_blank">View or Download Document</a></p>
                </div>`;
    }

    function createPortfolioLink(portfolio) {
        return `<div class="portfolio-item">
                    <h5>${portfolio.title}</h5>
                    <a href="${portfolio.filePath}" target="_blank">View File</a>
                </div>`;
    }
});