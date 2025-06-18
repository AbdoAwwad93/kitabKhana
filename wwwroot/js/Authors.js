// Authors Page JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize page
    initializeAuthorsPage();

    // Search functionality
    initializeSearch();

    // Author interactions
    initializeAuthorInteractions();

    // Modal functionality
    initializeModal();

    // Animations
    initializeAnimations();
});

function initializeAuthorsPage() {
    console.log('Authors page initialized');

    // Add loading animation to images
    const authorImages = document.querySelectorAll('.author-image');
    authorImages.forEach(img => {
        img.addEventListener('load', function () {
            this.classList.add('loaded');
        });

        img.addEventListener('error', function () {
            console.log('Author image failed to load:', this.src);
            this.src = '/Images/default-author.jpg';
            this.alt = 'صورة افتراضية للمؤلف';
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
    const authorCards = document.querySelectorAll('.author-card');
    const noResults = document.getElementById('noResults');
    let hasResults = false;

    if (searchTerm === '') {
        // Show all authors
        authorCards.forEach(card => {
            card.parentElement.style.display = 'block';
            card.classList.add('fade-in');
        });
        noResults.style.display = 'none';
        return;
    }

    authorCards.forEach(card => {
        const authorName = card.querySelector('.author-name').textContent.toLowerCase();
        const authorAbout = card.querySelector('.author-about').textContent.toLowerCase();
        const bookTags = Array.from(card.querySelectorAll('.book-tag')).map(tag => tag.textContent.toLowerCase());

        const searchLower = searchTerm.toLowerCase();

        if (authorName.includes(searchLower) ||
            authorAbout.includes(searchLower) ||
            bookTags.some(book => book.includes(searchLower))) {
            card.parentElement.style.display = 'block';
            card.classList.add('fade-in');
            hasResults = true;
        } else {
            card.parentElement.style.display = 'none';
        }
    });

    // Show/hide no results message
    noResults.style.display = hasResults ? 'none' : 'block';
}

function initializeAuthorInteractions() {
    // Follow author buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('follow-author') || e.target.closest('.follow-author')) {
            e.preventDefault();
            const btn = e.target.classList.contains('follow-author') ? e.target : e.target.closest('.follow-author');
            const authorId = btn.getAttribute('data-author-id');
            toggleFollowAuthor(authorId, btn);
        }
    });

    // View books buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('view-books') || e.target.closest('.view-books')) {
            e.preventDefault();
            const btn = e.target.classList.contains('view-books') ? e.target : e.target.closest('.view-books');
            const authorId = btn.getAttribute('data-author-id');
            viewAuthorBooks(authorId);
        }
    });

    // Author details buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('author-details') || e.target.closest('.author-details')) {
            e.preventDefault();
            const btn = e.target.classList.contains('author-details') ? e.target : e.target.closest('.author-details');
            const authorId = btn.getAttribute('data-author-id');
            showAuthorDetails(authorId);
        }
    });
}

function toggleFollowAuthor(authorId, btn) {
    const isFollowing = btn.classList.contains('following');

    // Show loading state
    const originalContent = btn.innerHTML;
    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> جاري...';
    btn.disabled = true;

    // Simulate API call
    setTimeout(() => {
        if (isFollowing) {
            // Unfollow
            btn.classList.remove('following');
            btn.innerHTML = '<i class="fas fa-user-plus"></i> متابعة';
            showNotification('تم إلغاء متابعة المؤلف', 'info');
        } else {
            // Follow
            btn.classList.add('following');
            btn.innerHTML = '<i class="fas fa-user-check"></i> متابع';
            showNotification('تم متابعة المؤلف بنجاح', 'success');
        }

        btn.disabled = false;
    }, 1000);

    console.log(`Author ${authorId} follow toggled: ${!isFollowing}`);
}

function viewAuthorBooks(authorId) {
    // This would typically redirect to a books page filtered by author
    // For now, we'll show a notification
    showNotification('سيتم توجيهك لصفحة كتب المؤلف', 'info');

    // You could redirect like this:
    // window.location.href = `/Book?authorId=${authorId}`;

    console.log(`Viewing books for author ${authorId}`);
}

function showAuthorDetails(authorId) {
    const authorCard = document.querySelector(`[data-author-id="${authorId}"]`);
    if (!authorCard) return;

    // Get author information from the card
    const authorName = authorCard.querySelector('.author-name').textContent;
    const authorImage = authorCard.querySelector('.author-image').src;
    const authorAbout = authorCard.querySelector('.author-about').textContent;
    const booksCount = authorCard.querySelector('.books-count')?.textContent.match(/\d+/)?.[0] || '0';
    const bookTags = Array.from(authorCard.querySelectorAll('.book-tag')).map(tag => tag.textContent);

    // Populate modal
    document.getElementById('modalAuthorName').textContent = authorName;
    document.getElementById('modalAuthorImage').src = authorImage;
    document.getElementById('modalAuthorAbout').textContent = authorAbout;
    document.getElementById('modalAuthorBooksCount').textContent = booksCount;

    // Populate books list
    const booksList = document.getElementById('modalAuthorBooksList');
    booksList.innerHTML = '';

    if (bookTags.length > 0) {
        bookTags.forEach(bookTitle => {
            const bookItem = document.createElement('div');
            bookItem.className = 'modal-book-item';
            bookItem.innerHTML = `<p class="modal-book-title">${bookTitle}</p>`;
            booksList.appendChild(bookItem);
        });
    } else {
        booksList.innerHTML = '<p class="text-muted text-center">لا توجد كتب متاحة</p>';
    }

    // Set author ID for follow button
    document.getElementById('modalFollowAuthor').setAttribute('data-author-id', authorId);

    // Show modal
    const modalElement = document.getElementById('authorDetailsModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
}

function initializeModal() {
    // Modal follow button
    document.getElementById('modalFollowAuthor').addEventListener('click', function () {
        const authorId = this.getAttribute('data-author-id');
        const correspondingBtn = document.querySelector(`.follow-author[data-author-id="${authorId}"]`);

        if (correspondingBtn) {
            toggleFollowAuthor(authorId, correspondingBtn);

            // Update modal button to match
            setTimeout(() => {
                if (correspondingBtn.classList.contains('following')) {
                    this.innerHTML = '<i class="fas fa-user-check"></i> متابع';
                    this.classList.add('following');
                } else {
                    this.innerHTML = '<i class="fas fa-user-plus"></i> متابعة';
                    this.classList.remove('following');
                }
            }, 1100);
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

    // Observe author cards
    document.querySelectorAll('.author-card').forEach(card => {
        observer.observe(card);
    });

    // Observe stat cards
    document.querySelectorAll('.stat-card').forEach(card => {
        observer.observe(card);
    });

    // Add hover effects to author cards
    document.querySelectorAll('.author-card').forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-10px) scale(1.02)';
        });

        card.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });
}

function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = 'notification';

    const icons = {
        success: 'fas fa-check-circle',
        error: 'fas fa-exclamation-circle',
        warning: 'fas fa-exclamation-triangle',
        info: 'fas fa-info-circle'
    };

    const colors = {
        success: 'var(--emerald)',
        error: '#e74c3c',
        warning: '#f39c12',
        info: 'var(--primary-gold)'
    };

    notification.innerHTML = `
        <i class="${icons[type]}"></i>
        <span>${message}</span>
    `;

    // Style the notification
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: ${colors[type]};
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
window.AuthorsPage = {
    performSearch,
    toggleFollowAuthor,
    viewAuthorBooks,
    showAuthorDetails,
    showNotification
};