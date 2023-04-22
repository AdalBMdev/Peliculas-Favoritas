using System;
using System.Collections.Generic;

namespace Peliculas.Models
{
    public partial class Usuario
    {
        public int Idusuario { get; set; }
        public string? Nickname { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string ConfirmarClave { get; set; }

    }
}
