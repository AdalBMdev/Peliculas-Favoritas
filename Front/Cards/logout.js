const logoutBtn = document.getElementById("logout");
const nicknameEl = document.getElementById('nickname');
const gender = document.getElementById("gender");



const logout = async () => {
  try {
    const response = await fetch("https://localhost:7011/Login/logout");
    const data = await response.json();
    console.log('Success:', data);
    localStorage.removeItem('userId');
    localStorage.removeItem('nicknameLog');
    window.location.replace("../Registro/index.html");
  } catch (error) {
    console.error('Error:', error);
  }
}

nicknameEl.textContent = localStorage.getItem('nicknameLog');

logoutBtn.addEventListener("click", (event) => {
  event.preventDefault();
  logout();
});

gender.addEventListener("click", (event) => {
  event.preventDefault();
  window.location.replace("../Generos/generosForm.html");
});