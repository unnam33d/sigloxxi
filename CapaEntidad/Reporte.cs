﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {

        public string FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string IdPedido { get; set; }
        public decimal Total { get; set; }
        public string IdTransaccion { get; set; }

    }
}
