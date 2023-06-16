using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reposicion
    {
        public int IdReposicion { get; set; }
        public int PrecioReposicion { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
    }
}
