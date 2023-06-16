using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; }
        public string Receta { get; set; }
        public string Tipo { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }
        public Ingredientes oIngrediente { get; set; }
        public Ingredientes oIngrediente1 { get; set; }
        public Ingredientes oIngrediente2 { get; set; }
        public Ingredientes oIngrediente3 { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Categoria oCategoria { get; set; }
    }
}
