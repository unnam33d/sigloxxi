using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Ingresos
    {
        public int IdIngreso { get; set; }
        public Venta oVenta { get; set; }
    }
}
