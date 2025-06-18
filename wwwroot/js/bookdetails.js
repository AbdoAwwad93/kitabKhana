// Book Details Page JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize page
    initializeBookDetails();

    // Initialize interactions
    initializeBookActions();

    // Initialize comment form
    initializeCommentForm();

    // Initialize animations
    initializeAnimations();

    // Initialize rating system
    initializeRatingSystem();
});

function initializeBookDetails() {
    console.log('Book details page initialized');

    // Add loading animation to images
    const images = document.querySelectorAll('img');
    images.forEach(img => {
        img.addEventListener('load', function () {
            this.classList.add('loaded');
        });

        img.addEventListener('error', function () {
            console.log('Image failed to load:', this.src);
            if (this.classList.contains('book-main-image')) {
                this.src = '/Images/default-book.jpg';
            } else if (this.closest('.author-image')) {
                this.src = '/Images/default-author.jpg';
            }
            this.alt = 'صورة افتراضية';
        });
    });
}

function initializeBookActions() {
    // Add to cart functionality
    const addToCartBtn = document.querySelector('.add-to-cart-btn');
    if (addToCartBtn) {
        addToCartBtn.addEventListener('click', handleAddToCart);
    }

    // Favorite functionality
    const favoriteBtn = document.querySelector('.favorite-btn');
    if (favoriteBtn) {
        favoriteBtn.addEventListener('click', function () {
            const bookId = this.getAttribute('data-book-id');
            toggleFavorite(bookId, this);
        });
    }
}

async function handleAddToCart(event) {
    const button = event.currentTarget;
    const bookId = button.dataset.bookId;
    
    try {
        const response = await fetch('/Cart/AddToCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ bookId: bookId, quantity: 1 })
        });

        const result = await response.json();
        
        if (result.success) {
            showNotification(result.message, 'success');
        } else {
            showNotification(result.message, 'error');
        }
    } catch (error) {
        showNotification('حدث خطأ أثناء إضافة الكتاب إلى السلة', 'error');
    }
}

function toggleFavorite(bookId, btn) {
    const isActive = btn.classList.contains('active');

    if (isActive) {
        // Remove from favorites
        btn.classList.remove('active');
        btn.innerHTML = '<i class="far fa-heart"></i> إضافة للمفضلة';
        showNotification('تم إزالة الكتاب من المفضلة', 'info');
    } else {
        // Add to favorites
        btn.classList.add('active');
        btn.innerHTML = '<i class="fas fa-heart"></i> في المفضلة';
        showNotification('تم إضافة الكتاب للمفضلة', 'success');
    }

    console.log(`Book ${bookId} favorite toggled: ${!isActive}`);
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

function initializeCommentForm() {
    const commentForm = document.getElementById('commentForm');
    if (commentForm) {
        commentForm.addEventListener('submit', function (e) {
            e.preventDefault();
            submitComment();
        });
    }
}

function submitComment() {
    const rating = document.querySelector('input[name="rating"]:checked');
    const commentText = document.getElementById('commentText');

    if (!rating) {
        showNotification('يرجى اختيار تقييم للكتاب', 'warning');
        return;
    }

    if (!commentText.value.trim()) {
        showNotification('يرجى كتابة تعليق', 'warning');
        return;
    }

    const commentForm = document.getElementById('commentForm');
    const submitBtn = commentForm.querySelector('button[type="submit"]');
    const originalContent = submitBtn.innerHTML;

    // Show loading state
    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> جاري الإرسال...';
    submitBtn.disabled = true;

    // Simulate API call
    setTimeout(() => {
        // Add comment to the list
        addCommentToList({
            rating: parseInt(rating.value),
            content: commentText.value.trim(),
            date: new Date()
        });

        // Reset form
        commentForm.reset();

        // Reset button
        submitBtn.innerHTML = originalContent;
        submitBtn.disabled = false;

        // Show success message
        showNotification('تم إرسال تعليقك بنجاح', 'success');

        // Scroll to comments section
        document.querySelector('.comments-section').scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        });

    }, 1500);
}

function addCommentToList(comment) {
    const commentsList = document.querySelector('.comments-list');
    if (!commentsList) return;

    const commentCard = document.createElement('div');
    commentCard.className = 'comment-card fade-in-up';

    const starsHtml = Array.from({ length: 5 }, (_, i) =>
        `<i class="fas fa-star ${i < comment.rating ? 'filled' : ''}"></i>`
    ).join('');

    commentCard.innerHTML = `
        <div class="comment-header">
            <div class="commenter-info">
                <div class="commenter-avatar">
                    <i class="fas fa-user-circle"></i>
                </div>
                <div class="commenter-details">
                    <h5 class="commenter-name">أنت</h5>
                    <div class="comment-rating">
                        ${starsHtml}
                    </div>
                </div>
            </div>
            <div class="comment-date">
                <i class="fas fa-clock"></i>
                <span>الآن</span>
            </div>
        </div>
        <div class="comment-content">
            <p>${comment.content}</p>
        </div>
    `;

    // Add to top of comments list
    commentsList.insertBefore(commentCard, commentsList.firstChild);

    // Update comments count
    const commentsCount = document.querySelector('.comments-count');
    if (commentsCount) {
        const currentCount = parseInt(commentsCount.textContent.match(/\d+/)[0]) || 0;
        commentsCount.textContent = `(${currentCount + 1})`;
    }
}

function initializeRatingSystem() {
    const starInputs = document.querySelectorAll('.star-rating input');
    const starLabels = document.querySelectorAll('.star-rating label');

    starLabels.forEach((label, index) => {
        label.addEventListener('mouseenter', function () {
            highlightStars(starLabels.length - index);
        });

        label.addEventListener('mouseleave', function () {
            const checkedInput = document.querySelector('.star-rating input:checked');
            if (checkedInput) {
                highlightStars(parseInt(checkedInput.value));
            } else {
                highlightStars(0);
            }
        });

        label.addEventListener('click', function () {
            const value = parseInt(this.getAttribute('for').replace('star', ''));
            highlightStars(value);
        });
    });

    function highlightStars(count) {
        starLabels.forEach((label, index) => {
            if (starLabels.length - index <= count) {
                label.style.color = 'var(--primary-gold)';
            } else {
                label.style.color = '#ddd';
            }
        });
    }
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
    document.querySelectorAll('.authors-section, .book-meta-section, .book-description-section, .comment-card, .add-comment-card').forEach(element => {
        observer.observe(element);
    });

    // Image hover effects
    const bookImage = document.querySelector('.book-main-image');
    if (bookImage) {
        bookImage.addEventListener('mouseenter', function () {
            this.style.transform = 'scale(1.02) rotate(1deg)';
        });

        bookImage.addEventListener('mouseleave', function () {
            this.style.transform = 'scale(1) rotate(0deg)';
        });
    }
}

function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.textContent = message;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.remove();
    }, 3000);
}

// Utility functions
function formatDate(date) {
    const now = new Date();
    const diffTime = Math.abs(now - date);
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    if (diffDays === 1) {
        return 'أمس';
    } else if (diffDays < 7) {
        return `منذ ${diffDays} أيام`;
    } else {
        return date.toLocaleDateString('ar-SA');
    }
}

// Export functions for external use
window.BookDetails = {
    handleAddToCart,
    toggleFavorite,
    submitComment,
    showNotification
};