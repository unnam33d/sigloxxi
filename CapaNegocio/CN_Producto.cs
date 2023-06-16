using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {

        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarProducto(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreProducto) || string.IsNullOrWhiteSpace(obj.NombreProducto))
            {
                Mensaje = "Ingresa un nombre al producto";
            }

            else if (string.IsNullOrEmpty(obj.Receta) || string.IsNullOrWhiteSpace(obj.Receta))
            {
                Mensaje = "Ingresa una receta al producto";
            }

            else if(obj.oIngrediente.IdIngrediente == 0)
            {
                Mensaje = "Ingresa al menos un ingrediente al producto";
            }

            else if(obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Ingresa el producto a una categoria";
            }

            else if(obj.Precio == 0)
            {
                Mensaje = "Ingresa un precio al producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.RegistrarProducto(obj, out Mensaje);

            }
            else
            {
                return 0;
            }


        }

        public bool EditarProducto(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreProducto) || string.IsNullOrWhiteSpace(obj.NombreProducto))
            {
                Mensaje = "Ingresa un nombre al producto";
            }

            else if (string.IsNullOrEmpty(obj.Receta) || string.IsNullOrWhiteSpace(obj.Receta))
            {
                Mensaje = "Ingresa una receta al producto";
            }

            else if (obj.oIngrediente.IdIngrediente == 0)
            {
                Mensaje = "Ingresa al menos un ingrediente al producto";
            }

            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Ingresa el producto a una categoria";
            }

            else if (obj.Precio == 0)
            {
                Mensaje = "Ingresa un precio al producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.EditarProducto(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto oProducto, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(oProducto, out Mensaje);
        }

        public bool EliminarProducto(int id, out string Mensaje)
        {
            return objCapaDato.EliminarProducto(id, out Mensaje);
        }

    }
}
