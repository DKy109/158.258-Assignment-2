// Book Library MVC - Custom JavaScript

$(document).ready(function () {
    console.log('Book Library MVC initialized');

    // Initialize tooltips
    $('[data-bs-toggle="tooltip"]').tooltip();

    // Initialize popovers
    $('[data-bs-toggle="popover"]').popover();

    // Add animation classes to elements
    animateElements();

    // Setup event listeners
    setupEventListeners();

    // Initialize real-time search
    initializeRealTimeSearch();

    // Initialize client-side sorting
    initializeClientSorting();

    // Initialize filter functionality
    initializeFilters();

    // Setup form validation
    setupFormValidation();
});

// Function to animate elements on page load
function animateElements() {
    $('.book-card').each(function (index) {
        $(this).css('animation-delay', (index * 0.1) + 's');
        $(this).addClass('fade-in-up');
    });

    // Animate stats boxes
    $('.stat-box').each(function (index) {
        $(this).css('animation-delay', (index * 0.2) + 's');
        $(this).addClass('fade-in-up');
    });
}

// Setup event listeners for interactive elements
function setupEventListeners() {
    // Add hover effects to cards
    $('.book-card').hover(
        function () {
            $(this).find('.card-title').addClass('text-primary');
        },
        function () {
            $(this).find('.card-title').removeClass('text-primary');
        }
    );

    // Quick action buttons
    $('.quick-action').click(function (e) {
        e.preventDefault();
        const action = $(this).data('action');
        const bookId = $(this).data('book-id');

        switch (action) {
            case 'preview':
                showBookPreview(bookId);
                break;
            case 'add-to-cart':
                addToCart(bookId);
                break;
            case 'wishlist':
                addToWishlist(bookId);
                break;
        }
    });

    // Price range slider
    if ($('#priceRange').length) {
        const priceSlider = document.getElementById('priceRange');
        const priceValue = document.getElementById('priceValue');

        priceSlider.addEventListener('input', function () {
            priceValue.textContent = '$' + this.value;
            applyFilters();
        });
    }

    // Year range slider
    if ($('#yearRange').length) {
        const yearSlider = document.getElementById('yearRange');
        const yearValue = document.getElementById('yearValue');

        yearSlider.addEventListener('input', function () {
            yearValue.textContent = this.value;
            applyFilters();
        });
    }
}

// Real-time search functionality
function initializeRealTimeSearch() {
    const searchInput = $('#searchInput');
    const searchResults = $('#quickSearchResults');
    let searchTimeout;

    if (searchInput.length) {
        searchInput.on('input', function () {
            clearTimeout(searchTimeout);
            const searchTerm = $(this).val().trim();

            if (searchTerm.length < 2) {
                searchResults.html('');
                return;
            }

            // Show loading indicator
            searchResults.html('<div class="text-center"><div class="spinner-custom mx-auto"></div><p>Searching...</p></div>');

            searchTimeout = setTimeout(function () {
                performRealTimeSearch(searchTerm);
            }, 300);
        });
    }
}

// Perform AJAX search
async function performRealTimeSearch(searchTerm) {
    try {
        const response = await fetch('/Books/Search?term=' + encodeURIComponent(searchTerm));
        const data = await response.json();

        if (data.length === 0) {
            $('#quickSearchResults').html('<div class="alert alert-info">No books found matching your search.</div>');
            return;
        }

        let resultsHtml = '<div class="list-group">';
        data.forEach(function (book) {
            resultsHtml += `
                <a href="/Books/Details/${book.id}" class="list-group-item list-group-item-action">
                    <div class="d-flex w-100 justify-content-between">
                        <h6 class="mb-1">${book.title}</h6>
                        <small class="text-success">${book.price}</small>
                    </div>
                    <p class="mb-1">${book.author} | ${book.category}</p>
                </a>
            `;
        });
        resultsHtml += '</div>';

        $('#quickSearchResults').html(resultsHtml);
    } catch (error) {
        console.error('Search error:', error);
        $('#quickSearchResults').html('<div class="alert alert-danger">Error performing search. Please try again.</div>');
    }
}

// Client-side sorting
function initializeClientSorting() {
    $('.sort-btn').click(function (e) {
        e.preventDefault();
        const sortBy = $(this).data('sort');
        const currentOrder = $(this).data('order');
        const newOrder = currentOrder === 'asc' ? 'desc' : 'asc';

        // Update UI
        $('.sort-btn').removeClass('active');
        $(this).addClass('active');
        $(this).data('order', newOrder);

        // Update sort icon
        const icon = $(this).find('.sort-icon');
        icon.html(newOrder === 'asc' ? '↑' : '↓');

        // Perform sorting
        sortBooks(sortBy, newOrder);
    });
}

// Sort books function
function sortBooks(sortBy, order) {
    const container = $('#booksGrid');
    const items = container.find('.col-md-4').toArray();

    items.sort(function (a, b) {
        const aValue = getSortValue(a, sortBy);
        const bValue = getSortValue(b, sortBy);

        if (order === 'asc') {
            return aValue > bValue ? 1 : -1;
        } else {
            return aValue < bValue ? 1 : -1;
        }
    });

    // Reorder items
    items.forEach(function (item) {
        container.append(item);
    });

    // Re-add animations
    animateElements();
}

// Get sort value from element
function getSortValue(element, sortBy) {
    const $element = $(element);
    switch (sortBy) {
        case 'price':
            return parseFloat($element.data('price') || 0);
        case 'year':
            return parseInt($element.data('year') || 0);
        case 'rating':
            return parseFloat($element.data('rating') || 0);
        case 'title':
            return $element.data('title') || '';
        case 'author':
            return $element.data('author') || '';
        default:
            return 0;
    }
}

// Filter functionality
function initializeFilters() {
    // Category filters
    $('.category-filter').change(function () {
        applyFilters();
    });

    // Price range
    $('#priceRangeMin, #priceRangeMax').on('input', function () {
        applyFilters();
    });

    // Stock status
    $('#inStockFilter').change(function () {
        applyFilters();
    });
}

// Apply all active filters
function applyFilters() {
    const selectedCategories = [];
    $('.category-filter:checked').each(function () {
        selectedCategories.push($(this).val());
    });

    const minPrice = parseFloat($('#priceRangeMin').val()) || 0;
    const maxPrice = parseFloat($('#priceRangeMax').val()) || 1000;
    const showInStock = $('#inStockFilter').is(':checked');

    $('.book-item').each(function () {
        const $item = $(this);
        const category = $item.data('category');
        const price = parseFloat($item.data('price')) || 0;
        const inStock = $item.data('in-stock') === 'true';

        const categoryMatch = selectedCategories.length === 0 || selectedCategories.includes(category);
        const priceMatch = price >= minPrice && price <= maxPrice;
        const stockMatch = !showInStock || inStock;

        if (categoryMatch && priceMatch && stockMatch) {
            $item.show();
            $item.addClass('fade-in-up');
        } else {
            $item.hide();
            $item.removeClass('fade-in-up');
        }
    });

    updateResultsCount();
}

// Update results count
function updateResultsCount() {
    const visibleCount = $('.book-item:visible').length;
    const totalCount = $('.book-item').length;
    $('#resultsCount').text(`${visibleCount} of ${totalCount} books`);
}

// Form validation setup
function setupFormValidation() {
    // Book form validation
    $('#bookForm').validate({
        rules: {
            Title: {
                required: true,
                minlength: 2,
                maxlength: 200
            },
            Author: {
                required: true,
                minlength: 2
            },
            Price: {
                required: true,
                min: 0.01,
                number: true
            },
            PublishedYear: {
                required: true,
                min: 1900,
                max: new Date().getFullYear(),
                digits: true
            },
            ISBN: {
                pattern: /^(?:\d{3}-)?\d{10}$/
            }
        },
        messages: {
            Title: {
                required: "Please enter a book title",
                minlength: "Title must be at least 2 characters long"
            },
            Author: {
                required: "Please enter an author name",
                minlength: "Author name must be at least 2 characters long"
            },
            Price: {
                required: "Please enter a price",
                min: "Price must be at least $0.01"
            },
            PublishedYear: {
                required: "Please enter a published year",
                min: "Year must be 1900 or later"
            },
            ISBN: {
                pattern: "Please enter a valid ISBN (10 or 13 digits)"
            }
        },
        errorClass: "text-danger",
        errorElement: "span",
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).addClass('is-valid').removeClass('is-invalid');
        }
    });
}

// Export functionality
function exportData(format) {
    showLoading('Exporting data...');

    setTimeout(function () {
        switch (format) {
            case 'csv':
                window.location.href = '/Books/ExportToCsv';
                break;
            case 'json':
                exportToJson();
                break;
            case 'print':
                window.print();
                break;
        }
        hideLoading();
    }, 1000);
}

// Export to JSON
function exportToJson() {
    const books = [];
    $('.book-item').each(function () {
        const book = {
            title: $(this).data('title'),
            author: $(this).data('author'),
            category: $(this).data('category'),
            price: $(this).data('price'),
            year: $(this).data('year')
        };
        books.push(book);
    });

    const dataStr = JSON.stringify(books, null, 2);
    const dataUri = 'data:application/json;charset=utf-8,' + encodeURIComponent(dataStr);

    const exportFileDefaultName = 'books_export.json';

    const linkElement = document.createElement('a');
    linkElement.setAttribute('href', dataUri);
    linkElement.setAttribute('download', exportFileDefaultName);
    linkElement.click();
}

// Show loading overlay
function showLoading(message) {
    if (!$('#loadingOverlay').length) {
        $('body').append(`
            <div id="loadingOverlay" class="position-fixed top-0 start-0 w-100 h-100 d-flex justify-content-center align-items-center" style="background: rgba(0,0,0,0.5); z-index: 9999;">
                <div class="text-center bg-white p-4 rounded shadow-lg">
                    <div class="spinner-custom mx-auto mb-3"></div>
                    <p class="mb-0">${message || 'Loading...'}</p>
                </div>
            </div>
        `);
    }
}

// Hide loading overlay
function hideLoading() {
    $('#loadingOverlay').remove();
}

// Quick actions
function showBookPreview(bookId) {
    showLoading('Loading preview...');

    $.get(`/Books/Details/${bookId}`, function (data) {
        const $data = $(data);
        const modalContent = $data.find('.container').html();

        $('#previewModal .modal-body').html(modalContent);
        $('#previewModal').modal('show');
        hideLoading();
    });
}

function addToCart(bookId) {
    showLoading('Adding to cart...');

    // Simulate API call
    setTimeout(function () {
        hideLoading();
        showNotification('Book added to cart successfully!', 'success');
    }, 500);
}

function addToWishlist(bookId) {
    showLoading('Adding to wishlist...');

    // Simulate API call
    setTimeout(function () {
        hideLoading();
        showNotification('Book added to wishlist!', 'info');
    }, 500);
}

// Notification system
function showNotification(message, type = 'info') {
    const notificationId = 'notification-' + Date.now();
    const icon = type === 'success' ? 'check-circle' :
        type === 'error' ? 'exclamation-circle' :
            type === 'warning' ? 'exclamation-triangle' : 'info-circle';

    const notification = $(`
        <div id="${notificationId}" class="position-fixed end-0 top-0 m-4 alert alert-${type} alert-dismissible fade show shadow" style="z-index: 9999; min-width: 300px;" role="alert">
            <i class="fas fa-${icon} me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `);

    $('body').append(notification);

    // Auto-remove after 5 seconds
    setTimeout(function () {
        $(`#${notificationId}`).alert('close');
    }, 5000);
}

// Responsive table helper
function makeTableResponsive() {
    $('.table-responsive table').each(function () {
        const $table = $(this);
        const $headers = $table.find('th');

        $table.find('td').each(function (index) {
            const $cell = $(this);
            const headerText = $headers.eq(index).text();

            if ($(window).width() < 768) {
                $cell.attr('data-label', headerText);
            }
        });
    });
}

// Initialize on window resize
$(window).resize(function () {
    makeTableResponsive();
});

// Dark mode toggle (optional feature)
function toggleDarkMode() {
    $('body').toggleClass('dark-mode');
    const isDarkMode = $('body').hasClass('dark-mode');
    localStorage.setItem('darkMode', isDarkMode);

    if (isDarkMode) {
        $('.navbar').removeClass('bg-primary').addClass('bg-dark');
    } else {
        $('.navbar').removeClass('bg-dark').addClass('bg-primary');
    }
}

// Check for saved dark mode preference
$(document).ready(function () {
    if (localStorage.getItem('darkMode') === 'true') {
        toggleDarkMode();
    }
});