document.addEventListener("DOMContentLoaded", function () {
    getYear();
});

function getYear() {
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    var displayYearElement = document.querySelector("#displayYear");
    if (displayYearElement) {
        displayYearElement.innerHTML = currentYear;
    } else {
        console.error("Element with ID 'displayYear' not found.");
    }
}