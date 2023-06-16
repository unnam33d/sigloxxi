using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Mesas
    {
        public int IdMesa { get; set; }
        public int Capacidad { get; set; }
        public string EstadoMesa { get; set; }
        public Pedidos oPedido { get; set; }
    }
}
