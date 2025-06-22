// Signup Page JavaScript
document.addEventListener('DOMContentLoaded', function () {
    initializeSignupForm();
});

function validateUserName(userName) {
    const field = document.getElementById('UserName');
    const userNameRegex = /^[a-zA-Z0-9_-]{3,20}$/;

    if (!userName) {
        showFieldError('UserName', 'اسم المستخدم مطلوب');
        field.style.borderColor = '#dc3545';
        return false;
    }

    if (!userNameRegex.test(userName)) {
        showFieldError('UserName', 'اسم المستخدم يجب أن يحتوي على أحرف إنجليزية وأرقام وعلامات - _ فقط');
        field.style.borderColor = '#dc3545';
        return false;
    }

    // Check username availability via AJAX
    $.get('/Account/CheckUserNameAvailability', { userName: userName })
        .done(function(isAvailable) {
            if (!isAvailable) {
                showFieldError('UserName', 'اسم المستخدم مستخدم بالفعل');
                field.style.borderColor = '#dc3545';
            } else {
                hideFieldError('UserName');
                field.style.borderColor = '#28a745';
            }
        });

    return true;
}

function initializeSignupForm() {
    const form = document.getElementById('signupForm');
    const passwordInput = document.getElementById('Password');
    const confirmPasswordInput = document.getElementById('ConfirmPassword');
    const submitBtn = document.getElementById('submitBtn');

    // Password toggle functionality
    initializePasswordToggle();

    // Password strength meter
    if (passwordInput) {
        passwordInput.addEventListener('input', function () {
            updatePasswordStrength(this.value);
        });
    }

    // Real-time password confirmation
    if (confirmPasswordInput) {
        confirmPasswordInput.addEventListener('input', function () {
            validatePasswordMatch();
        });
    }

    // Form submission
    // if (form) {
    //     form.addEventListener('submit', function (e) {
    //         e.preventDefault();
    //         handleFormSubmission();
    //     });
    // }

    // Social buttons
    initializeSocialButtons();
    const userNameInput = document.getElementById('UserName');
    if (userNameInput) {
        userNameInput.addEventListener('input', function() {
            validateUserName(this.value.trim());
        });

        // Add blur event for immediate validation
        userNameInput.addEventListener('blur', function() {
            validateUserName(this.value.trim());
        });
    }
}

function initializePasswordToggle() {
    const toggleButtons = document.querySelectorAll('.password-toggle');

    toggleButtons.forEach(button => {
        button.addEventListener('click', function () {
            const targetId = this.getAttribute('data-target');
            const passwordInput = document.getElementById(targetId);
            const icon = this.querySelector('i');

            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                passwordInput.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        });
    });
}

function updatePasswordStrength(password) {
    const strengthFill = document.querySelector('.strength-fill');
    const strengthText = document.querySelector('.strength-text');

    if (!strengthFill || !strengthText) return;

    // Remove previous classes
    strengthFill.classList.remove('weak', 'fair', 'good', 'strong');

    // Calculate strength
    let score = 0;
    let feedback = '';

    // Length check
    if (password.length >= 8) score += 25;
    if (password.length >= 12) score += 10;

    // Character variety checks
    if (/[a-z]/.test(password)) score += 15;
    if (/[A-Z]/.test(password)) score += 15;
    if (/[0-9]/.test(password)) score += 15;
    if (/[^A-Za-z0-9]/.test(password)) score += 20;

    // Update UI based on score
    if (score < 30) {
        strengthFill.classList.add('weak');
        feedback = 'كلمة مرور ضعيفة';
    } else if (score < 60) {
        strengthFill.classList.add('fair');
        feedback = 'كلمة مرور مقبولة';
    } else if (score < 80) {
        strengthFill.classList.add('good');
        feedback = 'كلمة مرور جيدة';
    } else {
        strengthFill.classList.add('strong');
        feedback = 'كلمة مرور قوية';
    }

    strengthText.textContent = feedback;
}

function validatePasswordMatch() {
    const password = document.getElementById('Password').value;
    const confirmPassword = document.getElementById('ConfirmPassword').value;
    const confirmInput = document.getElementById('ConfirmPassword');

    if (confirmPassword && password !== confirmPassword) {
        confirmInput.style.borderColor = '#dc3545';
        showFieldError('ConfirmPassword', 'كلمات المرور غير متطابقة');
    } else {
        confirmInput.style.borderColor = '#28a745';
        hideFieldError('ConfirmPassword');
    }
}

function showFieldError(fieldName, message) {
    const field = document.getElementById(fieldName);
    const existingError = field.parentNode.parentNode.querySelector('.validation-error');

    if (existingError) {
        existingError.textContent = message;
        existingError.style.display = 'block';
    }
}

function hideFieldError(fieldName) {
    const field = document.getElementById(fieldName);
    const existingError = field.parentNode.parentNode.querySelector('.validation-error');

    if (existingError) {
        existingError.style.display = 'none';
    }
}


// function validateForm() {
//     const fullName = document.getElementById('FullName').value.trim();
//     const email = document.getElementById('Email').value.trim();
//     const password = document.getElementById('Password').value;
//     const confirmPassword = document.getElementById('ConfirmPassword').value;

//     let isValid = true;

//     // Validate full name
//     if (!fullName) {
//         showFieldError('FullName', 'الاسم الكامل مطلوب');
//         isValid = false;
//     }

//     // Validate email
//     if (!email || !isValidEmail(email)) {
//         showFieldError('Email', 'البريد الإلكتروني غير صحيح');
//         isValid = false;
//     }

//     // Validate password
//     if (!password || password.length < 6) {
//         showFieldError('Password', 'كلمة المرور يجب أن تكون 6 أحرف على الأقل');
//         isValid = false;
//     }

//     // Validate password confirmation
//     if (password !== confirmPassword) {
//         showFieldError('ConfirmPassword', 'كلمات المرور غير متطابقة');
//         isValid = false;
//     }

//     // Validate terms agreement
  
//     return isValid;
// }


function initializeSocialButtons() {
    const facebookBtn = document.querySelector('.social-btn.facebook');
    const googleBtn = document.querySelector('.social-btn.google');


}


