using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Bodega
    {
        public int IdIngrediente { get; set; }
        public string NombreIngrediente { get; set; }
        public int Stock { get; set; }
        public string Lugar { get; set; }
    }
}
