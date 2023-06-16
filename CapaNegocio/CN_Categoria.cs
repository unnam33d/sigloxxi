using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {

        private CD_Categoria objCapaDato = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarCategoria(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Ingresa una Descripción";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.RegistrarCategoria(obj, out Mensaje); 

            }
            else
            {
                return 0;
            }


        }

        public bool EditarCategoria(Categoria obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La Descripción de la categoría ya está en uso.";
            }
            
            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.EditarCategoria(obj, out Mensaje);
            }
            else
            {
                return false;
            }





        }

        public bool EliminarCategoria(int id, out string Mensaje)
        {
            return objCapaDato.EliminarCategoria(id, out Mensaje);
        }

    }
}
