$(document).ready(function () {
    const token = localStorage.getItem('jwtToken');
    if (!token) {
        alert("You are not authenticated. Please log in.");
        window.location.href = '/login';
        return;
    }

    const cityDropdown = $('#city');

    function fetchCities() {
        $.ajax({
            url: '/api/City/getCities',
            type: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (cities) {
                populateCityDropdown(cities);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching cities:", error);
                alert("Could not load cities. Please try again later.");
            }
        });
    }

    function populateCityDropdown(cities) {
        cityDropdown.empty();

        cityDropdown.append('<option value="selectAll">Select All</option>');

        cities.forEach(city => {
            cityDropdown.append(new Option(city.cityName, city.cityId));
        });

        initializeSelect2();
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

        if (!cityIds || cityIds.length === 0) {
            alert("Please select at least one city.");
            return;
        }

        const profileData = {
            phone: phone,
            description: description,
            cities: cityIds
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
});