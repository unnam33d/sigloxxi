using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente oCliente { get; set; }
        public int TotalProducto { get; set; }
        public double MontoTotal { get; set; }
        public string IdTransaccion { get; set; }
        public Usuario oUsuario { get; set; }
        public Pedidos oPedido { get; set; }
        public bool ValidacionPago { get; set; }
        public DateTime FechaVenta { get; set; }

    }
}
