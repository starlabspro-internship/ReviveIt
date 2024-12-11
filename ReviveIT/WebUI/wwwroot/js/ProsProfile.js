$(document).ready(function () {
    if (!technicianId) {
        return;
    }

    const profileUrl = `https://reviveit.devops99.pro/api/prosprofileapi/GetTechnicianProfile/${technicianId}`;

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
                    <a href="#" class="portfolio-image-link" data-file="${portfolio.filePath}">
                        <img src="${portfolio.filePath}" alt="${portfolio.description || "No Description"}" class="portfolio-image" />
                    </a>
                    <h5>${portfolio.description || "No Description"}</h5> <!-- Display description below image -->
                </div>`;
    }

    function createPortfolioDocument(portfolio) {
        return `<div class="portfolio-item">
                    <embed src="${portfolio.filePath}" width="100%" height="400px" type="application/pdf" />
                    <h5>${portfolio.description || "No Description"}</h5> <!-- Display description below document -->
                    <p><a href="${portfolio.filePath}" target="_blank">View or Download Document</a></p>
                </div>`;
    }

    function createPortfolioLink(portfolio) {
        return `<div class="portfolio-item">
                    <a href="${portfolio.filePath}" target="_blank">View File</a>
                    <h5>${portfolio.description || "No Description"}</h5> <!-- Display description below link -->
                </div>`;
    }

    $(document).on('click', '.portfolio-image-link', function (e) {
        e.preventDefault();
        const imageUrl = $(this).data('file');
        openModal(imageUrl);
    });

    function openModal(imageUrl) {
        const modal = `
            <div class="modal-overlay">
                <div class="modal-content">
                    <span class="close-modal">&times;</span>
                    <img src="${imageUrl}" alt="Full Image" class="modal-image" />
                </div>
            </div>
        `;
        $('body').append(modal);

        $('.close-modal').click(function () {
            $('.modal-overlay').remove();
        });
    }
});