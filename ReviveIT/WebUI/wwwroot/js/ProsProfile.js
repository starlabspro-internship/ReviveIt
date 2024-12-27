$(document).ready(function () {
    function getCookie(name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(";").shift();
        return "";
    }

    async function fetchData(url, method = "GET", body = null, isFormData = false) {
        try {
            const headers = {
                Authorization: `Bearer ${getCookie("jwtToken")}`,
                ...(isFormData ? {} : { "Content-Type": "application/json" }),
            };
            const response = await fetch(url, {
                method,
                headers,
                body: body ? (isFormData ? body : JSON.stringify(body)) : null,
            });
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP Error ${response.status}: ${errorText}`);
            }
            return response.json();
        } catch (error) {
            showToast(`Error fetching data: ${error.message}`, true);
            throw error;
        }
    }

    const profilePictureSelector = "#profilePicture";
    const technicianFullNameSelector = "#technicianFullName";
    const technicianExpertiseSelector = "#technicianExpertise";
    const technicianExperienceSelector = "#technicianExperience";
    const technicianDescriptionSelector = "#technicianDescription";
    const technicianContactSelector = "#technicianContact";
    const technicianPortfolioSelector = "#technicianPortfolio";
    const addReviewButton = $("#addReviewButton");
    const reviewFormOverlay = $("#reviewFormOverlay");
    const reviewFormContainer = $("#reviewFormContainer");
    const closeFormBtn = $("#closeFormBtn");
    const reviewForm = $("#reviewForm");
    const starRating = $(".star-rating");
    const reviewRatingInput = $("#reviewRating");
    const addReviewButtonContainer = $(".add-review-button-container");
    const setElementText = (selector, text) => $(selector).text(text);
    const setElementHtml = (selector, html) => $(selector).html(html);
    const setElementAttribute = (selector, attribute, value) =>
        $(selector).attr(attribute, value);
    const defaultTechnicianProfilePicture = "/images/defaultProfilePicture.png";
    const defaultCompanyProfilePicture = "/images/defaultCompanyPicture.png";

    if (!technicianId) {
        showToast("No technician ID provided.", true);
        setElementAttribute(profilePictureSelector, "src", defaultTechnicianProfilePicture);
        setElementText(technicianFullNameSelector, "");
        setElementText(technicianExpertiseSelector, "No Expertise Provided");
        setElementText(
            technicianExperienceSelector,
            "Experience: No Experience Available"
        );
        setElementHtml(technicianDescriptionSelector, "<p>No description available.</p>");
        setElementHtml(
            technicianContactSelector,
            "<h4>Contact Information</h4><p>No Contact Information Available</p>"
        );
        setElementHtml(technicianPortfolioSelector, "<p>No portfolios available.</p>");
        return;
    }

    const fetchTechnicianProfile = async () => {
        try {
            const profileUrl = `/api/prosprofileapi/GetTechnicianProfile/${technicianId}`;
            const data = await fetchData(profileUrl);

            if (!data) {
                setElementHtml(technicianPortfolioSelector, `<p class="error-message">Failed to retrieve user data.</p>`);
                return;
            }


            let pfpUrl;
            if (data.profilePictureUrl) {
                pfpUrl = data.profilePictureUrl;
            }
            else {
                if (data.companyName) {
                    pfpUrl = defaultCompanyProfilePicture
                }
                else {
                    pfpUrl = defaultTechnicianProfilePicture;
                }
            }

            setElementAttribute(profilePictureSelector, "src", pfpUrl);
            setElementAttribute(profilePictureSelector, "style", "cursor:pointer");
            $(profilePictureSelector).on("click", () => openModal(pfpUrl, true))
            displayTechnicianProfile(data);
            await getUserReview();
            getReviews();
        } catch (error) {
            showToast(`Error fetching profile: ${error.message}`, true);
            setElementAttribute(profilePictureSelector, "src", defaultTechnicianProfilePicture);
            setElementHtml(
                technicianPortfolioSelector,
                `<p class="error-message">Failed to load profile data.</p>`
            );
        }
    };
    const displayTechnicianProfile = (data) => {
        setElementText(technicianFullNameSelector, data.companyName || data.fullName || "");
        setElementText(technicianExpertiseSelector, data.expertise || "No Expertise Provided");
        setElementText(
            technicianExperienceSelector,
            `Experience: ${data.experience || "No Experience Available"}`
        );
        displayPortfolios(data.portfolios);
        setElementHtml(
            technicianDescriptionSelector,
            data.description
                ? `<h4>Description</h4><p>${data.description}</p>`
                : "<p>No description available.</p>"
        );
        setElementHtml(
            technicianContactSelector,
            `
                 <h4>Contact Information</h4>
                 <p>Email: ${data.email || "No Email Provided"}</p>
                 <p>Phone: ${data.phoneNumber || "No Phone Number Provided"}</p>
               `
        );
    };

    const displayPortfolios = (portfolios) => {
        const portfolioContainer = $(technicianPortfolioSelector);
        portfolioContainer.empty();
        if (Array.isArray(portfolios) && portfolios.length > 0) {
            portfolioContainer.append("<h4>Portfolio</h4>");
            portfolios.forEach(portfolio => {
                let portfolioItem;
                if (portfolio.filePath.match(/\.(jpeg|jpg|gif|png)$/)) {
                    portfolioItem = createPortfolioImage(portfolio);
                } else if (portfolio.filePath.match(/\.(pdf|docx|pptx)$/)) {
                    portfolioItem = createPortfolioDocument(portfolio);
                } else {
                    portfolioItem = createPortfolioLink(portfolio);
                }
                portfolioContainer.append(portfolioItem);
            });
        }
        else {
            setElementHtml(technicianPortfolioSelector, "<p>No portfolios available.</p>");
        }
    };

    const createPortfolioImage = (portfolio) => {
        return `<div class="portfolio-item">
                  <a href="#" class="portfolio-image-link" data-file="${portfolio.filePath}">
                     <img src="${portfolio.filePath}" alt="${portfolio.description || "No Description"
            }" class="portfolio-image" />
                   </a>
                  <h5>${portfolio.description || "No Description"}</h5>
           </div>`;
    };

    const createPortfolioDocument = (portfolio) => {
        return `<div class="portfolio-item">
           <embed src="${portfolio.filePath}" width="100%" height="400px" type="application/pdf" />
           <h5>${portfolio.description || "No Description"}</h5>
          <p><a href="${portfolio.filePath}" target="_blank">View or Download Document</a></p>
       </div>`;
    };

    const createPortfolioLink = (portfolio) => {
        return `<div class="portfolio-item">
           <a href="${portfolio.filePath}" target="_blank">View File</a>
               <h5>${portfolio.description || "No Description"}</h5>
         </div>`;
    };

    $(document).on("click", ".portfolio-image-link", function (e) {
        e.preventDefault();
        const imageUrl = $(this).data("file");
        openModal(imageUrl, false);
    });

    const openModal = (imageUrl, isPfp) => {
        let modalClass = "modal-image"
        if (isPfp) {
            modalClass = "pfp-modal-image"
        }
        const modal = `
            <div class="modal-overlay">
                <div class="modal-content">
                    <span class="close-modal">×</span>
                     <img src="${imageUrl}" alt="Full Image" class="${modalClass}" />
               </div>
           </div>
        `;
        $("body").append(modal);
        attachCloseButtonListeners($(".modal-overlay").last()[0])
    };

    const getReviews = async () => {
        try {
            const reviewUrl = `/api/review/technicians/${technicianId}/reviews`;
            const data = await fetchData(reviewUrl);
            if (!data || !data.reviews || data.reviews.length === 0) {
                setElementHtml("#reviewsDropdown", `<p class='no-reviews-message' >No reviews available.</p>`);
                return;
            }
            displayReviews(data.reviews);
        } catch (error) {
            showToast(`Error fetching reviews: ${error.message}`, true);
            setElementHtml(
                "#reviewsDropdown",
                `<p class="error-message">Failed to load reviews.</p>`
            );
        }
    };

    const displayReviews = (reviews) => {
        const reviewsDropdown = $("#reviewsDropdown")
        reviewsDropdown.empty();
        if (reviews && reviews.length > 0) {
            let reviewsHTML = reviews
                .map(
                    (review) =>
                        `<div class="review-item">
                     <p class="rating">Rating: ${review.rating}/5</p>
                      <p class="comment">Comment: ${review.content}</p>
                     <p class="reviewer">Review by ${review.reviewerName || "Anonymous User"
                        }</p>
                   </div>`
                )
                .join("");
            reviewsDropdown.html(reviewsHTML)
        } else {
            setElementHtml(
                "#reviewsDropdown",
                `<p class='no-reviews-message' >No reviews available.</p>`
            );
        }
    };

    $("#toggleReviews").on("click", function () {
        const reviewsDropdown = $("#reviewsDropdown");
        const isVisible = reviewsDropdown.is(":visible");
        reviewsDropdown.slideToggle(400, () => {
            $(this).text(isVisible ? "Show Reviews" : "Hide Reviews");
            $(this).attr('aria-expanded', (!isVisible).toString());
        });
    });

    function toggleForm(show) {
        reviewFormOverlay.toggleClass("show", show);
    }

    function resetForm() {
        starRating.find("input[name='rating']").prop('checked', false);
        reviewRatingInput.val(0);
        $("#reviewComment").val("");
    }

    function handleRating() {
        const selectedRating = $("input[name='rating']:checked").val() || 0;
        reviewRatingInput.val(selectedRating);
    }
    starRating.on("change", "input[name='rating']", function () {
        handleRating();
    });

    addReviewButtonContainer.on("click", "#addReviewButton", function () {
        toggleForm(true);
    });

    addReviewButtonContainer.on("click", "#editReviewButton", async function () {
        toggleForm(true);
        await populateReviewForm();
    });

    closeFormBtn.on("click", function () {
        toggleForm(false);
        resetForm();
    });

    $(document).on("click", (event) => {
        if (reviewFormOverlay.is(event.target)) {
            toggleForm(false);
            resetForm();
        }
    });

    function attachCloseButtonListeners(modal) {
        const closeButtons = modal.querySelectorAll(".close-modal");

        closeButtons.forEach(button => {
            button.addEventListener('click', function () {
                const modal = button.closest('.modal-overlay, #profileModal, #zoomModal, #cropperModal, #editCategoriesModal, #editCitiesModal, #jobModal, .modal-overlay');
                if (modal) {
                    modal.style.display = "none";
                    document.body.classList.remove("modal-open");
                    document.querySelector(".modal-backdrop")?.remove();
                }
            });
        });
    }

    function showToast(message, isError = false) {
        const toast = $(
            `<div class="toast-message ${isError ? "error" : ""}">
          ${message}
        </div>`
        );
        $("#toast-container").append(toast);
        setTimeout(() => {
            toast.remove();
        }, 3000);
    }

    const deleteReview = async () => {
        try {
            const userReviewUrl = `/api/review/technicians/${technicianId}/reviews/user`;
            const userReviewData = await fetchData(userReviewUrl);
            if (userReviewData && userReviewData.review) {
                const deleteUrl = `/api/review/reviews/${userReviewData.review.reviewId}`;
                await fetchData(deleteUrl, "DELETE");
                showToast("Review deleted succesfully")
                await getUserReview();
                resetForm();
                getReviews();
            }
        }
        catch (error) {
            showToast(`Error deleting review: ${error.message}`, true);
        }
    };

    const getUserReview = async () => {
        try {
            const userReviewUrl = `/api/review/technicians/${technicianId}/reviews/user`;
            const userReviewData = await fetchData(userReviewUrl);
            if (userReviewData && userReviewData.review) {
                addReviewButtonContainer.empty();
                addReviewButtonContainer.append(`
               <button id="editReviewButton" class="add-review-button">
                <span class="star-icon">☆</span> Edit Review
                 </button>
                 <button id="deleteReviewButton" class="add-review-button">
                 <span class="star-icon">☆</span> Delete Review
                   </button>
             `);
                $("#deleteReviewButton").on("click", deleteReview);
            } else {
                addReviewButtonContainer.empty();
                addReviewButtonContainer.append(`
                  <button id="addReviewButton" class="add-review-button">
                 <span class="star-icon">☆</span> Add Review
                </button>
                 `);
            }
        } catch (error) {
            showToast(`Error fetching user review: ${error.message}`, true);
        }
    };

    const populateReviewForm = async () => {
        try {
            const userReviewUrl = `/api/review/technicians/${technicianId}/reviews/user`;
            const userReviewData = await fetchData(userReviewUrl);
            if (userReviewData && userReviewData.review) {
                $("#reviewComment").val(userReviewData.review.content);
                starRating.find(`input[value="${userReviewData.review.rating}"]`).prop("checked", true);
                reviewRatingInput.val(userReviewData.review.rating);
            } else {
                resetForm();
            }
        } catch (error) {
            showToast(`Error populating review form: ${error.message}`, true);
        }
    }

    reviewForm.on("submit", async function (event) {
        event.preventDefault();
        const comment = $("#reviewComment").val();
        const rating = reviewRatingInput.val();
        const userReviewUrl = `/api/review/technicians/${technicianId}/reviews/user`;
        const userReviewData = await fetchData(userReviewUrl);
        if (rating === "0") {
            showToast("Please select a rating", true);
            return;
        }
        if (!comment.trim()) {
            showToast("Comment is required", true);
            return;
        }
        try {
            if (userReviewData && userReviewData.review) {
                const updateReviewUrl = `/api/review/reviews/${userReviewData.review.reviewId}`;
                const reviewData = {
                    content: comment,
                    rating: parseInt(rating, 10),
                };
                await fetchData(
                    updateReviewUrl,
                    "PUT",
                    reviewData
                );
                showToast("Review updated Successfully");
            } else {
                const reviewData = {
                    reviewedUserId: technicianId,
                    content: comment,
                    rating: parseInt(rating, 10),
                };
                await fetchData(
                    `/api/review/users/${technicianId}/reviews`,
                    "POST",
                    reviewData
                );
                showToast("Review Added Successfully");
            }
            resetForm();
            toggleForm(false);
            await getUserReview();
            getReviews();
        } catch (error) {
            showToast(`Failed to submit review: ${error.message}`, true);
        }
    });
    fetchTechnicianProfile();
});