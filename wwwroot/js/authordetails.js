// Author Details Page JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize page
    initializeAuthorDetails();

    // Initialize interactions
    initializeAuthorActions();

    // Initialize book filtering
    initializeBookFiltering();

    // Initialize book interactions
    initializeBookInteractions();

    // Initialize share functionality
    initializeShareFunctionality();

    // Initialize animations
    initializeAnimations();
});

function initializeAuthorDetails() {
    console.log('Author details page initialized');

    // Add loading animation to images
    const images = document.querySelectorAll('img');
    images.forEach(img => {
        img.addEventListener('load', function () {
            this.classList.add('loaded');
        });

        img.addEventListener('error', function () {
            console.log('Image failed to load:', this.src);
            if (this.classList.contains('author-main-image')) {
                this.src = '/Images/default-author.jpg';
            } else if (this.classList.contains('book-image')) {
                this.src = '/Images/default-book.jpg';
            }
            this.alt = 'صورة افتراضية';
        });
    });
}

function initializeAuthorActions() {
    // Follow author functionality
    const followBtn = document.querySelector('.follow-btn');
    if (followBtn) {
        followBtn.addEventListener('click', function () {
            const shareModal = new bootstrap.Modal(document.getElementById('shareModal'));
            shareModal.show();
        });
    }
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
            btn.innerHTML = '<i class="fas fa-user-plus"></i> متابعة المؤلف';
            showNotification('تم إلغاء متابعة المؤلف', 'info');
            updateFollowersCount(-1);
        } else {
            // Follow
            btn.classList.add('following');
            btn.innerHTML = '<i class="fas fa-user-check"></i> متابع';
            showNotification('تم متابعة المؤلف بنجاح', 'success');
            updateFollowersCount(1);
        }

        btn.disabled = false;
    }, 1000);

    console.log(`Author ${authorId} follow toggled: ${!isFollowing}`);
}

function updateFollowersCount(change) {
    const followersElement = document.querySelector('.stat-item:nth-child(3) .stat-number');
    if (followersElement) {
        const currentText = followersElement.textContent;
        let currentCount = parseFloat(currentText.replace('K', '')) * (currentText.includes('K') ? 1000 : 1);
        currentCount += change;

        if (currentCount >= 1000) {
            followersElement.textContent = (currentCount / 1000).toFixed(1) + 'K';
        } else {
            followersElement.textContent = currentCount.toString();
        }

        // Add animation
        followersElement.style.transform = 'scale(1.2)';
        followersElement.style.color = 'var(--emerald)';
        setTimeout(() => {
            followersElement.style.transform = 'scale(1)';
            followersElement.style.color = '';
        }, 300);
    }
}

function initializeBookFiltering() {
    const filterButtons = document.querySelectorAll('.filter-btn');
    const bookItems = document.querySelectorAll('.book-item');

    filterButtons.forEach(btn => {
        btn.addEventListener('click', function () {
            // Remove active class from all buttons
            filterButtons.forEach(b => b.classList.remove('active'));

            // Add active class to clicked button
            this.classList.add('active');

            // Get filter value
            const filter = this.getAttribute('data-filter');

            // Filter books
            filterBooks(filter, bookItems);
        });
    });
}

function filterBooks(filter, bookItems) {
    bookItems.forEach(item => {
        const bookCategory = item.getAttribute('data-category');

        if (filter === 'all' || bookCategory === filter) {
            item.classList.remove('hidden');
            item.classList.add('visible');
        } else {
            item.classList.add('hidden');
            item.classList.remove('visible');
        }
    });

    // Update books count
    const visibleBooks = document.querySelectorAll('.book-item.visible').length;
    const booksCountElement = document.querySelector('.books-count');
    if (booksCountElement) {
        booksCountElement.textContent = `(${visibleBooks})`;
    }
}

function initializeBookInteractions() {
    // Add to cart buttons
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('add-to-cart') || e.target.closest('.add-to-cart')) {
            e.preventDefault();
            const btn = e.target.classList.contains('add-to-cart') ? e.target : e.target.closest('.add-to-cart');
            const bookId = btn.getAttribute('data-book-id');
            addToCart(bookId, btn);
        }
    });
}

function addToCart(bookId, btn) {
    const originalContent = btn.innerHTML;

    // Show loading state
    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';
    btn.disabled = true;

    // Simulate API call
    setTimeout(() => {
        // Show success state
        btn.innerHTML = '<i class="fas fa-check"></i> تم الإضافة';
        btn.classList.remove('btn-success');
        btn.classList.add('btn-success');

        // Update cart count in header
        updateCartCount();

        // Show notification
        showNotification('تم إضافة الكتاب إلى السلة بنجاح', 'success');

        // Reset button after 3 seconds
        setTimeout(() => {
            btn.innerHTML = originalContent;
            btn.classList.remove('btn-success');
            btn.disabled = false;
        }, 3000);

    }, 1000);

    console.log(`Book ${bookId} added to cart`);
}

function updateCartCount() {
    const cartCount = document.querySelector('.cart-count');
    if (cartCount) {
        let currentCount = parseInt(cartCount.textContent) || 0;
        cartCount.textContent = currentCount + 1;

        // Add animation
        cartCount.style.transform = 'scale(1.3)';
        cartCount.style.background = 'var(--emerald)';
        setTimeout(() => {
            cartCount.style.transform = 'scale(1)';
        }, 200);
    }
}

function initializeShareFunctionality() {
    const shareButtons = document.querySelectorAll('.share-options .share-btn');

    shareButtons.forEach(btn => {
        btn.addEventListener('click', function () {
            const platform = this.getAttribute('data-platform');
            const authorName = document.querySelector('.author-name').textContent;
            const currentUrl = window.location.href;
            const shareText = `تعرف على المؤلف ${authorName} وكتبه المميزة`;

            switch (platform) {
                case 'facebook':
                    shareOnFacebook(currentUrl, shareText);
                    break;
                case 'twitter':
                    shareOnTwitter(currentUrl, shareText);
                    break;
                case 'whatsapp':
                    shareOnWhatsApp(currentUrl, shareText);
                    break;
                case 'copy':
                    copyToClipboard(currentUrl);
                    break;
            }
        });
    });
}

function shareOnFacebook(url, text) {
    const shareUrl = `https://www.facebook.com/sharer/sharer.php?u=${encodeURIComponent(url)}`;
    window.open(shareUrl, '_blank', 'width=600,height=400');
    showNotification('تم فتح نافذة المشاركة على فيسبوك', 'info');
}

function shareOnTwitter(url, text) {
    const shareUrl = `https://twitter.com/intent/tweet?url=${encodeURIComponent(url)}&text=${encodeURIComponent(text)}`;
    window.open(shareUrl, '_blank', 'width=600,height=400');
    showNotification('تم فتح نافذة المشاركة على تويتر', 'info');
}

function shareOnWhatsApp(url, text) {
    const shareUrl = `https://wa.me/?text=${encodeURIComponent(text + ' ' + url)}`;
    window.open(shareUrl, '_blank');
    showNotification('تم فتح واتساب للمشاركة', 'info');
}

function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(() => {
        showNotification('تم نسخ الرابط بنجاح', 'success');

        // Close modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('shareModal'));
        if (modal) {
            modal.hide();
        }
    }).catch(() => {
        showNotification('فشل في نسخ الرابط', 'error');
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
                entry.target.classList.add('fade-in-up');
            }
        });
    }, observerOptions);

    // Observe sections
    document.querySelectorAll('.about-card, .book-card, .stat-card').forEach(element => {
        observer.observe(element);
    });

    // Author image hover effect
    const authorImage = document.querySelector('.author-main-image');
    if (authorImage) {
        authorImage.addEventListener('mouseenter', function () {
            this.style.transform = 'scale(1.05) rotate(2deg)';
        });

        authorImage.addEventListener('mouseleave', function () {
            this.style.transform = 'scale(1) rotate(0deg)';
        });
    }

    // Parallax effect for hero section
    window.addEventListener('scroll', function () {
        const scrolled = window.pageYOffset;
        const heroSection = document.querySelector('.author-hero-section');
        if (heroSection) {
            heroSection.style.transform = `translateY(${scrolled * 0.3}px)`;
        }
    });
}

function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = 'notification scale-in';

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
        max-width: 350px;
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
    }, 4000);
}

// Utility functions
function formatNumber(num) {
    if (num >= 1000) {
        return (num / 1000).toFixed(1) + 'K';
    }
    return num.toString();
}

// Export functions for external use
window.AuthorDetails = {
    toggleFollowAuthor,
    filterBooks,
    addToCart,
    showNotification
};