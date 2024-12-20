async function fetchSelectedCategories() {
    try {
        const response = await fetch('/api/Profile/get-selected-categories', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch selected categories");

        const categories = await response.json();

        const categoriesList = document.getElementById('selectedCategoriesList');
        categoriesList.innerHTML = '';

        categories.forEach(category => {
            const listItem = document.createElement('li');
            listItem.textContent = category.name;
            categoriesList.appendChild(listItem);
        });
    } catch (error) {
        console.error("Error fetching selected categories:", error);
        alert("An error occurred while fetching the categories.");
    }
}
async function editCategories() {
    try {
        const response = await fetch('/api/categories/getCategories', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`
            }
        });

        if (!response.ok) throw new Error("Failed to fetch categories");

        const categories = await response.json();

        const allCategoriesList = document.getElementById('allCategoriesList');
        allCategoriesList.innerHTML = '';

        categories.forEach(category => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <label>
                    <input type="checkbox" value="${category.categoryID}" /> ${category.name}
                </label>`;
            allCategoriesList.appendChild(listItem);
        });

        document.getElementById('editCategoriesModal').style.display = 'block';

        const selectedCategories = Array.from(document.querySelectorAll('#selectedCategoriesList li')).map(li => li.textContent.trim());
        document.querySelectorAll('#allCategoriesList input').forEach(input => {
            if (selectedCategories.includes(input.nextSibling.textContent.trim())) {
                input.checked = true;
            }
        });
    } catch (error) {
        console.error("Error fetching categories:", error);
        alert("An error occurred while fetching categories.");
    }
}

function closeEditCategoriesModal() {
    document.getElementById('editCategoriesModal').style.display = 'none';
}

async function saveCategories() {
    try {
        const selectedCategoryIds = Array.from(document.querySelectorAll('#allCategoriesList input:checked'))
            .map(input => parseInt(input.value))
            .filter(id => !isNaN(id));

        if (!selectedCategoryIds.length) {
            alert("Please select at least one category.");
            return;
        }

        const response = await fetch('/api/Profile/updateCategories', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${getCookie('jwtToken')}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(selectedCategoryIds)
        });

        if (!response.ok) throw new Error("Failed to update categories");

        alert("Categories updated successfully!");
        closeEditCategoriesModal();
        fetchSelectedCategories();
    } catch (error) {
        console.error("Error updating categories:", error);
        alert("An error occurred while updating categories.");
    }
}

document.addEventListener('DOMContentLoaded', () => {
    fetchSelectedCategories();
});