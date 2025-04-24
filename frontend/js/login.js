const passwordField = document.getElementById("password");
const userField = document.getElementById("user");
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

function Login(event) {
    event.preventDefault();
    const user = userField.value;
    const password = passwordField.value;

    fetch("http://localhost:5000/v1/auth", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ user, password }),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            return response.json();
        })
        .then((data) => {
            if (data.isValid) {
                alert("Login successful!");
                localStorage.setItem("authToken", data.token); // Armazena o token no localStorage
                // Redirecionar ou realizar outras ações
            } else {
                alert("Invalid user or password.");
            }
        })
        .catch((error) => {
            console.error("There was a problem with the fetch operation:", error);
            alert("An error occurred. Please try again later.");
        });
}

function fetchProtectedData() {
    const token = localStorage.getItem("authToken");

    fetch("http://localhost:5000/v1/protected-endpoint", {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`, // Envia o token no cabeçalho
        },
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Unauthorized");
            }
            return response.json();
        })
        .then((data) => {
            console.log("Protected data:", data);
        })
        .catch((error) => {
            console.error("Error fetching protected data:", error);
        });
}