using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_TESIS.Models
{
    public class Tipo
    {
        public int TipoId { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario> Usuario { get; set; }
    }
}
