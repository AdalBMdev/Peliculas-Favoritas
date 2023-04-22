const formulario = document.getElementById("form-genero");
const formedit = document.getElementById("form-edit");
const enviar = document.getElementById("enviarLogin");
const enviarE = document.getElementById("enviarEdit");
const editar = document.getElementById("seccionEdit");
const agregar = document.getElementById("seccionAdd");


// Volver la sección invisible
editar.style.display = "none";
agregar.style.display = "block";

document.getElementById('agregar').addEventListener('click', function(event) {
    event.preventDefault();
    editar.style.display = "none";
agregar.style.display = "block";
});

formulario.addEventListener("submit",(event) => {
    event.preventDefault();

    const generos = document.getElementById("genero").value;

  
// Guardar datos en la API 
  fetch("https://localhost:7011/Generos/agregarGenero", {
  method: "POST",
  body: JSON.stringify({generos}),
  headers: {
    "Content-Type": "application/json"
  },
})
.then(response => response.json())
.then(data => {
  console.log('Success:', data);
  location.reload();
  
})
.catch(error => console.error('Error:', error));

})

async function getData() {
    const response = await fetch("https://localhost:7011/Generos/mostrarGeneros");
    const responseData = await response.json();
    console.log('Success:', responseData);
    
    
    const container = document.getElementById('tabla');
    let tableBodyHtml = '';
    for(let i = 0; i < responseData.length; i++){
        const genero = responseData[i];
        
        const idgender = genero.id_generos;
        tableBodyHtml += `
          <tr>
            <td>${genero.generos}</td>
            <td>
              <i id="editar-${genero.id_generos}" data-id="${genero.id_generos}" class='bx bx-edit-alt btn btn-primary btn-sm' style="margin-right: 5px;"></i>
              <i id="eliminar-${genero.id_generos}" data-id="${genero.id_generos}" class='bx bx-trash btn btn-danger btn-sm'></i>
            </td>
          </tr>`;

          const generos = genero.generos;

          setTimeout(() => {
            const eliminarBtn = document.getElementById(`eliminar-${idgender}`);
            const editarBtn = document.getElementById(`editar-${idgender}`);
  
            editarBtn.addEventListener('click', async () => {
            document.getElementById("generoE").value = generos;
            agregar.style.display = "none";
            editar.style.display = "block";
console.log(idgender);

            formedit.addEventListener("submit",(event) => {
                event.preventDefault();
            
                const editGender = document.getElementById("generoE").value;
                const id = editarBtn.getAttribute('data-id');

                const datos = {

                    id_generos: id,
                    generos: editGender
                }
                fetch('https://localhost:7011/Generos/editarGenero', {
                    method: "PUT",
                    body: JSON.stringify(datos),
                    headers: {
                        "Content-Type": "application/json"
                    },
                })
                .then(response => response.json())
                .then(response => console.log('Success:', response))
                .catch(error => console.error('Error:', error));
                location.reload();
            })
            

                
              });

          eliminarBtn.addEventListener('click', async () => {
            const id = eliminarBtn.getAttribute('data-id');
            await fetch(`https://localhost:7011/Generos/eliminarGenero/${id}`, { method: 'DELETE' });
            location.reload();
          });
          }, 2000);
          
          
    }
    container.innerHTML = `
    <div class="container d-flex justify-content-center align-items-center">
    <div class="row">
      <div class="col-md-12">
        <table class="table mx-auto">
          <thead>
            <tr>
              <th>Nombre</th>
              <th>Botones</th>
            </tr>
          </thead>
          <tbody> 
            ${tableBodyHtml}
          </tbody>
        </table>
      </div>
    </div>
    
      </div>
        `;
    }
  getData();