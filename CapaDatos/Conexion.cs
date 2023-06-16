using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CapaEntidad;
using CapaDatos;

namespace CapaDatos
{
    public class Conexion
    {
        public static string Cn = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
    }
}
