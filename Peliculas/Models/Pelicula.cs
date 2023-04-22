using System;
using System.Collections.Generic;

namespace Peliculas.Models
{
    public partial class Pelicula
    {
        public int Idpeliculas { get; set; }
        public int? Idusuario { get; set; }
        public string? Titulo { get; set; }
        public DateTime? Año { get; set; }
        public string? Director { get; set; }
        public string? Genero { get; set; }

        public int id_generos { get; set; }
        public string? Poster { get; set; }

    }
}
