$(document).ready(function () {
    function getCookie(name) {
        const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
        return match ? match[2] : null;
    }

    const token = getCookie('jwtToken'); 
    const cityDropdown = $('#city');
    const categoryDropdown = $('#category');

    function fetchCities() {
        $.ajax({
            url: '/api/City/getCities',
            type: 'GET',
            success: function (cities) {
                populateCityDropdown(cities);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching cities:", error);
                alert("Could not load cities. Please try again later.");
            }
        });
    }

    function fetchCategories() {
        $.ajax({
            url: '/api/Categories/getCategories',
            type: 'GET',
            success: function (categories) {
                populateCategoryDropdown(categories);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching cities:", error);
                alert("Could not load cities. Please try again later.");
            }
        });
    }

    function populateCategoryDropdown(categories) {
        categoryDropdown.empty();

        categories.forEach(category => {
            categoryDropdown.append(new Option(category.name, category.categoryID));
        });

        initializeSelect2Categories();
    }


    function populateCityDropdown(cities) {
        cityDropdown.empty();

        cityDropdown.append('<option value="selectAll">Select All</option>');

        cities.forEach(city => {
            cityDropdown.append(new Option(city.cityName, city.cityId));
        });

        initializeSelect2();
    }

    function initializeSelect2Categories() {
        categoryDropdown.select2({
            placeholder: "Select your categories",
            allowClear: true
        });
    }

    function initializeSelect2() {
        cityDropdown.select2({
            placeholder: "Select your cities",
            allowClear: true
        });

        cityDropdown.on('change', function () {
            const selectedValues = $(this).val();

            if (selectedValues && selectedValues.includes('selectAll')) {
                const allCityIds = [];
                $('#city option').each(function () {
                    const cityId = $(this).val();
                    if (cityId !== 'selectAll') {
                        allCityIds.push(cityId);
                    }
                });

                cityDropdown.val(allCityIds).trigger('change');
            } else if (!selectedValues) {
                cityDropdown.val(null).trigger('change');
            }
        });
    }

    $('#profileForm').on('submit', function (event) {
        event.preventDefault();

        const phone = $('#phone').val();
        const description = $('#description').val();
        const cityIds = $('#city').val();
        const categoryIds = $('#category').val();
        const experience = $('#experience').val();

        if (!cityIds || cityIds.length === 0) {
            alert("Please select at least one city.");
            return;
        }

        const profileData = {
            phone: phone,
            description: description,
            cities: cityIds,
            categories: categoryIds,
            experience: experience
        };

        updateProfile(profileData);
    });

    function updateProfile(profileData) {
        $.ajax({
            url: '/api/CompleteProfileApi/CompleteProfile',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            data: JSON.stringify(profileData),
            success: function (response) {
                alert(response.message || "Profile updated successfully!");
                window.location.href = '/';
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText || "An error occurred while updating your profile.");
            }
        });
    }

    fetchCities();
    fetchCategories();
});