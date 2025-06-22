// Book Details Page JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize page
    initializeBookDetails();

    // Initialize interactions
    initializeBookActions();

    // Initialize animations
    initializeAnimations();

    // Initialize rating system
    initializeRatingSystem();

    // AJAX for Add/Remove from Cart
    const bookActionBtn = document.querySelector('.book-actions button');
    if (bookActionBtn) {
        bookActionBtn.addEventListener('click', function () {
            const bookId = this.getAttribute('data-book-id');
            const isAdding = this.classList.contains('btn-add-to-cart');
            const url = isAdding ? '/Cart/AddToCart' : '/Cart/RemoveFromCart';

            this.disabled = true;
            this.innerHTML = `<i class="fas fa-spinner fa-spin"></i> <span>...</span>`;

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'RequestVerificationToken': getAntiForgeryToken()
                },
                body: JSON.stringify(bookId)
            })
            .then(response => {
                if (response.status === 401) {
                    window.location.href = '/Account/LoginView';
                    return null;
                }
                if (!response.ok) throw new Error('Request failed');
                return response.json();
            })
            .then(data => {
                if (data && data.success) {
                    const cartCount = document.querySelector('.cart-count');
                    if (cartCount) {
                        cartCount.textContent = data.count;
                    }

                    this.disabled = false;
                    const icon = this.querySelector('i');
                    const text = this.querySelector('span');

                    if (isAdding) {
                        this.classList.remove('btn-add-to-cart');
                        this.classList.add('btn-remove-from-cart');
                        icon.className = 'fas fa-trash';
                        text.textContent = 'إزالة من السلة';
                    } else {
                        this.classList.remove('btn-remove-from-cart');
                        this.classList.add('btn-add-to-cart');
                        icon.className = 'fas fa-cart-plus';
                        text.textContent = 'أضف إلى السلة';
                    }
                }
            })
            .catch(() => {
                window.location.reload(); // Fallback
            });
        });
    }

    // AJAX for Add Comment
    const commentForm = document.getElementById('commentForm');
    if (commentForm) {
        commentForm.addEventListener('submit', function (e) {
            e.preventDefault();

            const formData = new FormData(this);
            const submitBtn = this.querySelector('button[type="submit"]');
            const originalBtnHtml = submitBtn.innerHTML;

            submitBtn.disabled = true;
            submitBtn.innerHTML = `<i class="fas fa-spinner fa-spin"></i> إرسال...`;

            fetch(this.action, {
                method: 'POST',
                body: formData,
                headers: {
                    'Request-Verification-Token': getAntiForgeryToken()
                }
            })
            .then(response => {
                if (response.status === 401) {
                    window.location.href = '/Account/LoginView';
                    return null;
                }
                if (!response.ok) throw new Error('Network response was not ok.');
                return response.text();
            })
            .then(html => {
                if (html) {
                    const commentsList = document.getElementById('commentsList');
                    const noCommentsMessage = document.getElementById('noCommentsMessage');
                    if (noCommentsMessage) {
                        noCommentsMessage.remove();
                    }

                    commentsList.insertAdjacentHTML('afterbegin', html);
                    
                    const commentsCountSpan = document.querySelector('.comments-count');
                    if (commentsCountSpan) {
                        const currentCount = parseInt(commentsCountSpan.textContent.match(/\d+/)[0], 10);
                        commentsCountSpan.textContent = `(${currentCount + 1})`;
                    }

                    this.reset();
                    const stars = this.querySelectorAll('.star-rating input');
                    stars.forEach(s => s.checked = false);
                }
            })
            .catch(error => {
                console.error('Error submitting comment:', error);
                alert('حدث خطأ أثناء إضافة التعليق.');
            })
            .finally(() => {
                submitBtn.disabled = false;
                submitBtn.innerHTML = originalBtnHtml;
            });
        });
    }

    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }
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

function handleAddToCart(e) {
    e.preventDefault();
    const btn = e.target.closest('.add-to-cart-btn');
    const bookId = btn.getAttribute('data-book-id');
    const originalContent = btn.innerHTML;

    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';
    btn.disabled = true;

    fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(bookId)
    })
    .then(response => {
        if (!response.ok) throw new Error('Network response was not ok');
        return response.json();
    })
    .then(data => {
        if (data.success) {
            btn.innerHTML = '<i class="fas fa-check"></i> تم الإضافة';
            btn.classList.add('btn-success');
            updateCartCount(data.count);
            showNotification('تم إضافة الكتاب إلى السلة بنجاح', 'success');
        } else {
            btn.innerHTML = originalContent;
            showNotification(data.message || 'حدث خطأ أثناء الإضافة', 'error');
        }
        setTimeout(() => {
            btn.innerHTML = originalContent;
            btn.classList.remove('btn-success');
            btn.disabled = false;
        }, 2000);
    })
    .catch(() => {
        btn.innerHTML = originalContent;
        btn.disabled = false;
        showNotification('حدث خطأ أثناء الإضافة', 'error');
    });
}

function updateCartCount(newCount) {
    const cartCount = document.querySelector('.cart-count');
    if (cartCount && typeof newCount === 'number') {
        cartCount.textContent = newCount;
        cartCount.style.transform = 'scale(1.3)';
        cartCount.style.background = 'var(--emerald)';
        setTimeout(() => {
            cartCount.style.transform = 'scale(1)';
        }, 200);
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
    showNotification
};