$(document).ready(function () {
    const loadingState = () => {
        $("#profilePicture").attr("src", "https://via.placeholder.com/150");
        $("#technicianFullName").text("Loading...");
        $("#technicianExpertise").text("Loading...");
        $("#technicianExperience").text("Experience: Loading...");
        $("#technicianDescription").html("<p>Loading description...</p>");
        $("#technicianContact").html("<h4>Contact Information</h4><p>Loading contact info...</p>");
        $("#technicianPortfolio").html("<p>Loading portfolios...</p>");
    };
    if (!technicianId) {
        console.error("No technician ID provided.");
        $("#profilePicture").attr("src", "https://via.placeholder.com/150");
        $("#technicianFullName").text("No Name Provided");
        $("#technicianExpertise").text("No Expertise Provided");
        $("#technicianExperience").text("Experience: No Experience Available");
        $("#technicianDescription").html("<p>No description available.</p>");
        $("#technicianContact").html("<h4>Contact Information</h4><p>No Contact Information Available</p>");
        $("#technicianPortfolio").html("<p>No portfolios available.</p>");
        return;
    }
    const profileUrl = `/api/prosprofileapi/GetTechnicianProfile/${technicianId}`;

    loadingState();
    $.ajax({
        url: profileUrl,
        method: "GET",
        success: function (data) {
            if (data) {
                displayTechnicianProfile(data);
                getReviews();
            }
        },
        error: function (xhr) {
            console.error("Error fetching profile:", xhr.statusText);
            $("#technicianPortfolio").html(`<p class="error-message">Failed to load profile data.</p>`);
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
                  <h5>${portfolio.description || "No Description"}</h5>
           </div>`;
    }
    function createPortfolioDocument(portfolio) {
        return `<div class="portfolio-item">
             <embed src="${portfolio.filePath}" width="100%" height="400px" type="application/pdf" />
              <h5>${portfolio.description || "No Description"}</h5>
             <p><a href="${portfolio.filePath}" target="_blank">View or Download Document</a></p>
            </div>`;
    }
    function createPortfolioLink(portfolio) {
        return `<div class="portfolio-item">
                  <a href="${portfolio.filePath}" target="_blank">View File</a>
                    <h5>${portfolio.description || "No Description"}</h5>
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
                    <span class="close-modal">×</span>
                     <img src="${imageUrl}" alt="Full Image" class="modal-image" />
               </div>
           </div>
        `;
        $('body').append(modal);
        $('.close-modal').click(function () {
            $('.modal-overlay').remove();
        });
    }
    function getReviews() {
        const reviewUrl = `/api/review/technicians/${technicianId}/reviews`;
        $.ajax({
            url: reviewUrl,
            method: "GET",
            success: function (data) {
                if (data && data.reviews) {
                    displayReviews(data.reviews);
                }
                else {
                    $("#reviewsDropdown").html(`<p class='no-reviews-message' >No reviews available.</p>`);
                }
            },
            error: function (xhr) {
                console.error("Error fetching reviews:", xhr.statusText);
                $("#reviewsDropdown").html(`<p class="error-message">Failed to load reviews.</p>`);
            }
        });
    }
    function displayReviews(reviews) {
        if (reviews && reviews.length > 0) {
            let reviewsHTML = reviews.map(review =>
                `<div class="review-item">
                     <p class="rating">Rating: ${review.rating}/5</p>
                    <p class="comment">Comment: ${review.content}</p>
                    <p class="reviewer">Review by ${review.reviewerName || 'Anonymous User'}</p>
                </div>`
            ).join("");
            $("#reviewsDropdown").html(reviewsHTML);
        } else {
            $("#reviewsDropdown").html(`<p class='no-reviews-message' >No reviews available.</p>`);
        }
    }
    $('#toggleReviews').on('click', function () {
        $('#reviewsDropdown').slideToggle(400, () => {
            const buttonText = $('#reviewsDropdown').is(':visible') ? "Hide Reviews" : "Show Reviews";
            $(this).text(buttonText)
        });
    });
});