

   async function getData() {
    const response = await fetch("https://localhost:7011/Generos/mostrarGeneros");
   const responseData = await response.json();
   console.log('Success:', responseData);

   const container = document.getElementById('Genero');
   container.innerHTML = '';
   for (let i = 0; i < responseData.length; i++) {
     const genero = responseData[i];
     const option = document.createElement('option');
     option.text = genero.generos;
     option.value = genero.id_generos;
     container.add(option);
   }
  }
  
  getData();
  