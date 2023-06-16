using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int Rut { get; set; }
        public string NombreCliente { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool reestablecer { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
