using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_TESIS.Models
{
    public class Receta
    {
        public int RecetaId { get; set; }
        public int CuadroClinicoId { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Duracion { get; set; }
        public int Cantidad { get; set; }
    }
}
