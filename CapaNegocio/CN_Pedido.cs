using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Pedido
    {

        private CD_Pedido objCapaDato = new CD_Pedido();

        public bool ExistePedido(int idcliente, int idproducto) {
                    
            return objCapaDato.ExistePedido(idcliente, idproducto);

        }

        public bool OperacionPedido(int idcliente, int idproducto, bool sumar, out string Mensaje) {

            return objCapaDato.OperacionPedido(idcliente, idproducto, sumar, out Mensaje);
            
        }

        public int CantidadEnPedido(int idcliente) {

            return objCapaDato.CantidadEnPedido(idcliente);

        }



    }
}
