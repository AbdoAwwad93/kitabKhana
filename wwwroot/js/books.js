// Books Page JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize page
    initializeBooksPage();

    // Search functionality
    initializeSearch();

    // Filter functionality
    initializeFilters();

    // Book interactions
    initializeBookInteractions();

    // Modal functionality
    initializeModal();

    // Animations
    initializeAnimations();
});

function initializeBooksPage() {
    console.log('Books page initialized');

    // Add loading animation to images
    const bookImages = document.querySelectorAll('.book-image');
    bookImages.forEach(img => {
        // Add loading class
        img.classList.add('loading-image');

        img.addEventListener('load', function () {
            this.classList.remove('loading-image');
            this.classList.add('loaded');
        });

        img.addEventListener('error', function () {
            console.log('Image failed to load:', this.src);
            this.classList.remove('loading-image');
            this.src = '/Images/default-book.jpg'; // Updated path
            this.alt = 'صورة افتراضية للكتاب';
        });
    });
}

function initializeSearch() {
    const searchInput = document.getElementById('searchInput');
    let searchTimeout;

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                performSearch(this.value.trim());
            }, 300);
        });

        // Clear search on escape
        searchInput.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') {
                this.value = '';
                performSearch('');
            }
        });
    }
}

function performSearch(searchTerm) {
    const bookCards = document.querySelectorAll('.book-card');
    const categorySections = document.querySelectorAll('.category-section');
    const noResults = document.getElementById('noResults');
    let hasResults = false;

    // Clear previous highlights
    clearHighlights();

    if (searchTerm === '') {
        // Show all books and categories
        bookCards.forEach(card => {
            card.style.display = 'block';
            card.classList.add('fade-in');
        });
        categorySections.forEach(section => {
            section.style.display = 'block';
        });
        noResults.style.display = 'none';
        return;
    }

    bookCards.forEach(card => {
        const title = card.querySelector('.book-title').textContent.toLowerCase();
        const description = card.querySelector('.book-description').textContent.toLowerCase();
        const authors = card.querySelector('.book-authors')?.textContent.toLowerCase() || '';
        const category = card.querySelector('.category-badge').textContent.toLowerCase();

        const searchLower = searchTerm.toLowerCase();

        if (title.includes(searchLower) ||
            description.includes(searchLower) ||
            authors.includes(searchLower) ||
            category.includes(searchLower)) {
            card.style.display = 'block';
            card.classList.add('fade-in');
            hasResults = true;
        } else {
            card.style.display = 'none';
        }
    });

    // Hide empty categories
    categorySections.forEach(section => {
        const visibleBooks = section.querySelectorAll('.book-card[style*="block"]');
        if (visibleBooks.length === 0) {
            section.style.display = 'none';
        } else {
            section.style.display = 'block';
        }
    });

    // Show/hide no results message
    noResults.style.display = hasResults ? 'none' : 'block';

    // Highlight search terms
    highlightSearchTerms(searchTerm);
}

function highlightSearchTerms(searchTerm) {
    if (!searchTerm) return;

    const bookCards = document.querySelectorAll('.book-card[style*="block"]');
    bookCards.forEach(card => {
        const title = card.querySelector('.book-title');
        const description = card.querySelector('.book-description');

        [title, description].forEach(element => {
            if (element && !element.hasAttribute('data-original-text')) {
                element.setAttribute('data-original-text', element.textContent);
                const text = element.textContent;
                const regex = new RegExp(`(${escapeRegExp(searchTerm)})`, 'gi');
                element.innerHTML = text.replace(regex, '<mark>$1</mark>');
            }
        });
    });
}

function clearHighlights() {
    const highlightedElements = document.querySelectorAll('[data-original-text]');
    highlightedElements.forEach(element => {
        element.textContent = element.getAttribute('data-original-text');
        element.removeAttribute('data-original-text');
    });
}

function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}

function initializeFilters() {
    const filterButtons = document.querySelectorAll('.filter-btn');

    filterButtons.forEach(btn => {
        btn.addEventListener('click', function () {
            // Remove active class from all buttons
            filterButtons.forEach(b => b.classList.remove('active'));

            // Add active class to clicked button
            this.classList.add('active');

            // Get category
            const category = this.getAttribute('data-category');

            // Filter books
            filterBooksByCategory(category);

            // Clear search
            const searchInput = document.getElementById('searchInput');
            if (searchInput) {
                searchInput.value = '';
            }

            // Clear highlights
            clearHighlights();
        });
    });
}

function filterBooksByCategory(categoryId) {
    const categorySections = document.querySelectorAll('.category-section');
    const noResults = document.getElementById('noResults');

    if (categoryId === 'all') {
        // Show all categories
        categorySections.forEach(section => {
            section.style.display = 'block';
            section.classList.add('fade-in');
        });
        noResults.style.display = 'none';
    } else {
        // Show only selected category
        let hasVisibleCategory = false;
        categorySections.forEach(section => {
            if (section.getAttribute('data-category-id') === categoryId) {
                section.style.display = 'block';
                section.classList.add('fade-in');
                hasVisibleCategory = true;
            } else {
                section.style.display = 'none';
            }
        });

        noResults.style.display = hasVisibleCategory ? 'none' : 'block';
    }

    // Smooth scroll to content
    document.querySelector('.books-content').scrollIntoView({
        behavior: 'smooth',
        block: 'start'
    });
}

function initializeBookInteractions() {
    // Quick view buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('quick-view') || e.target.closest('.quick-view')) {
            e.preventDefault();
            const btn = e.target.classList.contains('quick-view') ? e.target : e.target.closest('.quick-view');
            const bookId = btn.getAttribute('data-book-id');
            showQuickView(bookId);
        }
    });

    // Add to cart buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('add-to-cart') || e.target.closest('.add-to-cart')) {
            e.preventDefault();
            const btn = e.target.classList.contains('add-to-cart') ? e.target : e.target.closest('.add-to-cart');
            const bookId = btn.getAttribute('data-book-id');
            addToCart(bookId);
        }
    });
}

function showQuickView(bookId) {
    const bookCard = document.querySelector(`[data-book-id="${bookId}"]`);
    if (!bookCard) return;

    // Get book informtion
    const title = bookCard.querySelector('.book-title').getAttribute('data-original-text') ||
        bookCard.querySelector('.book-title').textContent;
    const image = bookCard.querySelector('.book-image').src;
    const authors = bookCard.querySelector('.book-authors')?.textContent || 'غير محدد';
    const description = bookCard.querySelector('.book-description').getAttribute('data-original-text') ||
        bookCard.querySelector('.book-description').textContent;
    const pages = bookCard.querySelector('.pages-count').textContent;
    const category = bookCard.querySelector('.category-badge').textContent;

    // Populate modal
    document.getElementById('modalBookTitle').textContent = title;
    document.getElementById('modalBookImage').src = image;
    document.getElementById('modalBookAuthors').textContent = authors;
    document.getElementById('modalBookDescription').textContent = description;
    document.getElementById('modalBookPages').textContent = pages;
    document.getElementById('modalBookCategory').textContent = category;

    // Set book ID for add to cart button
    document.getElementById('modalAddToCart').setAttribute('data-book-id', bookId);

    // Show modal
    const modalElement = document.getElementById('quickViewModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
}

function addToCart(bookId) {
    // Get book information
    const bookCard = document.querySelector(`[data-book-id="${bookId}"]`);
    const title = bookCard.querySelector('.book-title').getAttribute('data-original-text') ||
        bookCard.querySelector('.book-title').textContent;

    // Add animation to button
    const btn = bookCard.querySelector('.add-to-cart');
    const originalContent = btn.innerHTML;
    btn.innerHTML = '<i class="fas fa-check"></i> تم الإضافة';
    btn.classList.remove('btn-success');
    btn.classList.add('btn-success');
    btn.disabled = true;

    // Reset button after 2 seconds
    setTimeout(() => {
        btn.innerHTML = originalContent;
        btn.classList.remove('btn-success');
        btn.disabled = false;
    }, 2000);

    // Update cart count
    updateCartCount();

    // Show notification
    showNotification(`تم إضافة "${title}" إلى السلة بنجاح`);

    //console.log(`Book ${bookId} added to cart`);
}

function updateCartCount() {
    const cartCount = document.querySelector('.cart-count');
    if (cartCount) {
        let currentCount = parseInt(cartCount.textContent) || 0;
        cartCount.textContent = currentCount + 1;

        // Add animation
        cartCount.style.transform = 'scale(1.3)';
        setTimeout(() => {
            cartCount.style.transform = 'scale(1)';
        }, 200);
    }
}

function initializeModal() {
    // Modal add to cart button
    document.getElementById('modalAddToCart').addEventListener('click', function () {
        const bookId = this.getAttribute('data-book-id');
        addToCart(bookId);

        // Close modal
        const modalElement = document.getElementById('quickViewModal');
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    });
}

function initializeAnimations() {
    // Intersection Observer for scroll animations
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function (entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('slide-up');
            }
        });
    }, observerOptions);

    // Observe book cards
    document.querySelectorAll('.book-card').forEach(card => {
        observer.observe(card);
    });

    // Observe category headers
    document.querySelectorAll('.category-header').forEach(header => {
        observer.observe(header);
    });
}

function showNotification(message) {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = 'notification';
    notification.innerHTML = `
        <i class="fas fa-check-circle"></i>
        <span>${message}</span>
    `;

    // Style the notification
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: linear-gradient(45deg, var(--emerald), var(--primary-gold));
        color: white;
        padding: 15px 20px;
        border-radius: 10px;
        box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        z-index: 1000;
        transform: translateX(100%);
        transition: transform 0.3s ease;
        display: flex;
        align-items: center;
        gap: 10px;
        font-weight: 600;
        max-width: 300px;
    `;

    document.body.appendChild(notification);

    // Show notification
    setTimeout(() => {
        notification.style.transform = 'translateX(0)';
    }, 100);

    // Hide notification
    setTimeout(() => {
        notification.style.transform = 'translateX(100%)';
        setTimeout(() => {
            if (document.body.contains(notification)) {
                document.body.removeChild(notification);
            }
        }, 300);
    }, 3000);
}

// Utility functions
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Export functions for external use
window.BooksPage = {
    performSearch,
    filterBooksByCategory,
    addToCart,
    showQuickView
};