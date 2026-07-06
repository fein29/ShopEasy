function checkPasswordStrength(password) {
    const strengthMessage = document.getElementById('strengthMessage');
    let strength = 0;

    // Check length
    if (password.length >= 8) strength++;
    if (password.length >= 12) strength++;

    // Check for uppercase
    if (/[A-Z]/.test(password)) strength++;

    // Check for lowercase
    if (/[a-z]/.test(password)) strength++;

    // Check for numbers
    if (/[0-9]/.test(password)) strength++;

    // Check for special characters
    if (/[!@#$%^&*]/.test(password)) strength++;

    if (strength < 2) {
        strengthMessage.innerHTML = '❌ Weak password';
        strengthMessage.className = 'password-strength strength-weak';
    } else if (strength < 4) {
        strengthMessage.innerHTML = '⚠️ Medium strength';
        strengthMessage.className = 'password-strength strength-medium';
    } else {
        strengthMessage.innerHTML = '✅ Strong password';
        strengthMessage.className = 'password-strength strength-strong';
    }
}

// Validate passwords match on form submit
document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    if (form) {
        form.addEventListener('submit', function(e) {
            const passwordInput = document.getElementById('Password');
            const confirmPasswordInput = document.getElementById('ConfirmPassword');

            if (passwordInput && confirmPasswordInput) {
                const password = passwordInput.value;
                const confirmPassword = confirmPasswordInput.value;

                if (password !== confirmPassword) {
                    e.preventDefault();
                    alert('Passwords do not match!');
                    return false;
                }
            }
        });
    }
});
