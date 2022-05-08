using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_TESIS.Models
{
    public class Persona
    {
        public int PersonaId { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string DNI { get; set; }
        public string NroCelular { get; set; }
        [Required]
        public string EstadoCivil { get; set; }
        [Required]
        public string Sexo { get; set; }
        public string TelefonoEmergencia { get; set; }
        public string CorreoElectronico { get; set; }
        public string CondicionDeRiesgo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Vacunas { get; set; }
        public string Alergias { get; set; }
        public string Ocupacion { get; set; }
        public string Responsable { get; set; }
        public decimal Talla { get; set; }
        public decimal Peso { get; set; }
        public List<CuadroClinico> CuadroClinico { get; set; }
    }
}
