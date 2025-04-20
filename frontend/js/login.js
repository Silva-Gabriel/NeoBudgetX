const passwordField = document.getElementById("password");
const togglePassword = document.querySelector(".password-toggle-icon");

// Inicializa a senha mascarada
passwordField.setAttribute("data-mask", "true");
passwordField.style.webkitTextSecurity = "disc";

function showPassword() {
    if (togglePassword.classList.contains("fa-eye")) {
        togglePassword.classList.add("fa-eye-slash");
        togglePassword.classList.remove("fa-eye");
        passwordField.setAttribute("data-mask", "true"); // Custom attribute to indicate masked
        passwordField.style.webkitTextSecurity = "disc"; // Enable masking
    } else {
        togglePassword.classList.remove("fa-eye-slash");
        togglePassword.classList.add("fa-eye");
        passwordField.setAttribute("data-mask", "false"); // Custom attribute to indicate unmasked
        passwordField.style.webkitTextSecurity = "none"; // Disable masking
    }
}