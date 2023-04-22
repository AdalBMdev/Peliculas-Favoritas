const formularioLogin = document.getElementById("form-login");
const enviarLogin = document.getElementById("enviarLogin");

formularioLogin.addEventListener("submit",(event) => {
    event.preventDefault();
   
    const Clave = document.getElementById("ClaveL").value;
    const Correo = document.getElementById("CorreoL").value;
   
    // Crear un objeto JSON con los valores del formulario
    const datos = {
      correo: Correo,
      clave: Clave
    }
  
     console.log(datos);
  
// Guardar datos en la API 
  fetch("https://localhost:7011/Login/login", {
  method: "POST",
  body: JSON.stringify(datos),
  headers: {
    "Content-Type": "application/json"
  },
})
.then(response => response.json())
.then(data => {
  console.log('Success:', data);
  // Acceder al ID del usuario desde la respuesta de la API
  const userId = data.id;
  const nicknameLog = data.nickname;
  
  
  if(userId != undefined) {

    // Guardar el id y nickname de usuario en localStorage
    localStorage.setItem('userId', userId);
    localStorage.setItem('nicknameLog', nicknameLog);

    window.location.replace("../Cards/index.html");
    
  }
  
})
.catch(error => console.error('Error:', error));

})


