const form = document.getElementById("form");
const formPut = document.getElementById("formPut");
const enviarP = document.getElementById("enviarP");
const editar = document.getElementById("editar");
const IDUsuario = localStorage.getItem('userId');
const formImg = document.getElementById("img");
const nick = localStorage.getItem('nicknameLog');
const modalSave = document.getElementById('modalSave');
const filtroButton = document.getElementById("filtroButton");

modalSave.addEventListener('click', async () => {
  editar.style.visibility = 'hidden';
  enviarP.style.visibility = 'visible';
  formImg.style.backgroundImage = 'url(imagenes/Back.jpg)';

  form.reset();
});

enviarP.addEventListener('click', () => {
  document.getElementById("closeModal").click();
  setTimeout(() => location.reload(), 2000);
});

form.addEventListener("submit", (event) => {
  event.preventDefault();
  fetch('poster.php', {method: 'POST', body: new FormData(form)})
  .then(response => response.json())
  .then(data => {
    const {rutaDestino: Poster} = data;
    const Titulo = document.getElementById("Titulo").value;
    const Año = document.getElementById("Año").value;
    const Director = document.getElementById("Director").value;
    const id_generos = document.getElementById("Genero").value;
    console.log(id_generos);
    fetch("https://localhost:7011/Peliculas/registrarPelicula", {
      method: "POST",
      body: JSON.stringify({IDUsuario, Titulo, Año, Director, id_generos, Poster}),
      headers: {"Content-Type": "application/json"},
    }).then(response => response.json())
    .then(response => console.log('Success:', response))
    .catch(error => console.error('Error:', error));
  }).catch(console.error);
});

const cargarPeliculas = async (url) => {
  console.log('q');

  const response = await fetch(url);
   const responseData = await response.json();
   console.log('Success:', responseData);

   const container = document.getElementById('peliculas-container');
   container.innerHTML = '';
   for (let i = 0; i < responseData.length; i++) {
     const pelicula = responseData[i];
     const peliculaElement = generarFilaPelicula(pelicula);
     container.appendChild(peliculaElement);

     const idPeliculas = pelicula.idpeliculas;
     const titulo = pelicula.titulo;
     const director = pelicula.director;
     const genero = pelicula.id_generos;
     const poster = pelicula.poster;

     const editarBtn = document.getElementById(`editar-${idPeliculas}`);
     const eliminarBtn = document.getElementById(`eliminar-${idPeliculas}`);

     editarBtn.addEventListener('click', (event) => {
      console.log('Click en el botón de editar:', event.currentTarget);
      const idPeliculasE = event.currentTarget.getAttribute('data-id');
      formImg.style.backgroundImage = `url(${poster})`;
      const Modal = document.getElementById("openmodal");
      Modal.click();

    //Rellena el formulario con los datos anteriores
      document.getElementById("Titulo").value = titulo;
      document.getElementById("Director").value = director;
      document.getElementById("Genero").value = genero;
      enviarP.style.visibility = 'hidden';
      editar.style.visibility = 'visible';

      formPut.addEventListener("submit", (event) => {
        event.preventDefault();
       
      const formData2 = new FormData(form);
    
        fetch('poster.php', {
          method: 'POST',
          body: formData2
        })
        .then(response => response.json())
        .then(data => {
            
            const Titulo = document.getElementById("Titulo").value;
            const Año = document.getElementById("Año").value;
            const Director = document.getElementById("Director").value;
            const id_generos = document.getElementById("Genero").value;
            const Poster = data.rutaDestino;

    
            const datos = {
                IDPeliculas: idPeliculasE,
                Titulo: Titulo,
                Año: Año,
                Director: Director,
                id_generos: id_generos,
                Poster: Poster
            };
    
            console.log(datos);
    
            fetch("https://localhost:7011/Peliculas/editarPeliculas", {
                method: "PUT",
                body: JSON.stringify(datos),
                headers: {
                    "Content-Type": "application/json"
                },
            })
            .then(response => response.json())
            .then(response => console.log('Success:', response))
            .catch(error => console.error('Error:', error));
        })
        .catch(error => {
            console.error(error);
        });
    });

    editar.addEventListener('click', () => {
      const ModalClose = document.getElementById("closeModal");
      ModalClose.click();
      setTimeout(() => {
        location.reload();
      }, 1000);
    });
    
    

  });
    //Eliminar consulta

    eliminarBtn.addEventListener('click', async () => {
      const id = eliminarBtn.getAttribute('data-id');
      await fetch(`https://localhost:7011/Peliculas/eliminarPeliculas/${id}`, { method: 'DELETE' });
      location.reload();
    });
    
   }
};

function generarFilaPelicula(pelicula) {
  const idPeliculas = pelicula.idpeliculas;


  const fecha = new Date(pelicula.año);
  const fechaString = fecha.toLocaleDateString('es-ES', {year: 'numeric' });

  const peliculaElement = document.createElement('div');
  peliculaElement.classList.add('col-md-4', 'mb-4'); // Añadir las clases de Bootstrap
  peliculaElement.setAttribute('data-id', pelicula.idpeliculas);
  peliculaElement.innerHTML = `
    <div class="contenedor">
      <div class="c-card">
        <div class="card">
          <div class="card">
            <div class="card_banner">
              <div class="background">
                <div class="c-background">
                  <div class="card__poster">
                    <img src="${pelicula.poster}" alt="${pelicula.titulo}">
                  </div>
                  <div class="card__banner__content">
                    <div class="__content">
                      <h3 class="__content--title">${pelicula.titulo}</h3>
                      <p class="__content--subtitle">${pelicula.director}-${pelicula.genero}</p>
                      <p class="__content--subtitle">${fechaString}</p>
                      <button id="editar-${idPeliculas}" data-id="${idPeliculas}"  class="__content--btn">Editar</button>
                      <div class="__content--widgets">
                        <i id="eliminar-${idPeliculas}" data-id="${idPeliculas}" class="fas fa-trash"></i>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <img src="${pelicula.poster}" alt="">
            </div>
          </div>
        </div>
      </div>
    </div>
  `;

  return peliculaElement;
}

filtroButton.addEventListener('click', async () => {
  const busqueda = document.getElementById("filtroInput").value;
  const filtro = document.getElementById("filtro").value;
  console.log(filtro);
  if(filtro === '1'){
    cargarPeliculas(`https://localhost:7011/Peliculas/mostrarPeliculas/${IDUsuario}`);
  }
  else {
    cargarPeliculas(`https://localhost:7011/filtroTitulo/${filtro}/${IDUsuario}/${busqueda}`);
  }
});

cargarPeliculas(`https://localhost:7011/Peliculas/mostrarPeliculas/${IDUsuario}`);














