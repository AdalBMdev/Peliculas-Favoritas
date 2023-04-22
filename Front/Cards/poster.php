<?php

if (isset($_FILES['Poster'])) {
    $nombreArchivo = $_FILES['Poster']['name'];
    $tipoArchivo = $_FILES['Poster']['type'];
    $tamanoArchivo = $_FILES['Poster']['size'];
    $rutaTemporal = $_FILES['Poster']['tmp_name'];

    $directorioDestino = "../Posters/";
    $rutaDestino = $directorioDestino . $nombreArchivo;

    // Cargar imagen desde ruta temporal
    $imagen = imagecreatefromjpeg($rutaTemporal);
    
    // Obtener dimensiones de la imagen original
    $anchoOriginal = imagesx($imagen);
    $altoOriginal = imagesy($imagen);
    
    // Definir nuevas dimensiones
    $anchoNuevo = 800;
    $altoNuevo = 1200;
    
    // Crear nueva imagen con las dimensiones deseadas
    $imagenNueva = imagecreatetruecolor($anchoNuevo, $altoNuevo);
    
    // Copiar imagen original a la nueva imagen redimensionada
    imagecopyresampled($imagenNueva, $imagen, 0, 0, 0, 0, $anchoNuevo, $altoNuevo, $anchoOriginal, $altoOriginal);
    
    // Guardar nueva imagen en el directorio de destino
    imagejpeg($imagenNueva, $rutaDestino);
    
    if (file_exists($rutaDestino)) {
        $respuesta = [
            "mensaje" => "El archivo se guardó correctamente en: " . $rutaDestino,
            "rutaDestino" => $rutaDestino
        ];
    } else {
        $respuesta = [
            "mensaje" => "Hubo un error al guardar el archivo."
        ];
    }

    header("Content-Type: application/json");
    echo json_encode($respuesta);
}


?>
