// Basic JavaScript for Book Library
$(document).ready(function () {
    console.log('Book Library initialized');
    
    // Simple form validation
    setupFormValidation();
    
    // Basic event listeners
    setupEventListeners();
});

// Simple form validation
function setupFormValidation() {
    // Book form validation
    if ($('#bookForm').length) {
        $('#bookForm').submit(function (e) {
            var isValid = true;
            var errorMessages = [];
            
            // Check required fields
            $('.required').each(function () {
                if ($(this).val().trim() === '') {
                    isValid = false;
                    errorMessages.push($(this).prev('label').text() + ' is required');
                    $(this).addClass('is-invalid');
                } else {
                    $(this).removeClass('is-invalid');
                }
            });
            
            // Check price is a positive number
            var priceField = $('#Price');
            if (priceField.length) {
                var price = parseFloat(priceField.val());
                if (isNaN(price) || price < 0.01) {
                    isValid = false;
                    errorMessages.push('Price must be a positive number');
                    priceField.addClass('is-invalid');
                } else {
                    priceField.removeClass('is-invalid');
                }
            }
            
            // Show errors if any
            if (!isValid) {
                e.preventDefault();
                alert('Please fix the following errors:\n' + errorMessages.join('\n'));
            }
        });
    }
}

// Basic event listeners
function setupEventListeners() {
    // Add hover effect to cards
    $('.book-card').hover(
        function () {
            $(this).css('border-color', '#0d6efd');
        },
        function () {
            $(this).css('border-color', '#dee2e6');
        }
    );
    
    // Confirm delete
    $('.delete-link').click(function (e) {
        if (!confirm('Are you sure you want to delete this book?')) {
            e.preventDefault();
        }
    });
}
