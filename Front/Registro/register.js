const formularioRegistro = document.getElementById("form-register");
const enviar = document.getElementById("enviarRegistro");

formularioRegistro.addEventListener("submit",(event) => {
    event.preventDefault();

   
  const Nickname = document.getElementById("Nickname").value;
  const Clave = document.getElementById("Clave").value;
  const Correo = document.getElementById("Correo").value;
  const ConfirmarClave = document.getElementById("ConfirmarClave").value;


  console.log(Clave);
  console.log(ConfirmarClave);

  if (Clave === ConfirmarClave) {
    const datos = 
    {
     Nickname: Nickname,
     Correo: Correo,
     Clave: Clave,
     ConfirmarClave: ConfirmarClave
    }

    // Guardar datos en la API 
  fetch("https://localhost:7011/Login/registrar", {
    method: "POST",
    body: JSON.stringify(datos),
    headers: {
      "Content-Type": "application/json"
    },
    })
    .then(response => response.json())
    .then(response => console.log('Success:', response))
    .catch(error => console.error('Error:', error));

    
  console.log(datos);
  alert('Usuario registrado');
  } 
  else {
    alert('Las Clave no coinciden');
    return;
  }
})