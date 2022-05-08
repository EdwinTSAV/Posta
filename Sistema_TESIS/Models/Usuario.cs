using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_TESIS.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int TipoId { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
        public Tipo Tipo { get; set; }
    }
}
