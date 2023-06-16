using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Ingredientes
    {

        private CD_Ingredientes objCapaDato = new CD_Ingredientes();

        public List<Ingredientes> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarIngrediente(Ingredientes obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Ingresa una Descripción";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.RegistrarIngrediente(obj, out Mensaje);

            }
            else
            {
                return 0;
            }


        }

        public bool EditarIngrediente(Ingredientes obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El Ingrediente ya esta en uso.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.EditarIngrediente(obj, out Mensaje);
            }
            else
            {
                return false;
            }





        }

        public bool EliminarIngrediente(int id, out string Mensaje)
        {
            return objCapaDato.EliminarIngrediente(id, out Mensaje);
        }
    }
}
