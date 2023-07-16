using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

namespace CapaCliente.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {

            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();

            if(oProducto != null)
            {

                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);


            }

            return View(oProducto);
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            lista = new CN_Categoria().Listar();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria) {

            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                Receta = p.Receta,
                oCategoria = p.oCategoria,
                oIngrediente = p.oIngrediente,
                Precio = p.Precio,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo


            }).Where(p =>
            p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria) &&
            p.Activo == true
            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);

            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }


        [HttpPost]
        public JsonResult AgregarPedido(int idproducto) {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool existe = new CN_Pedido().ExistePedido(idcliente, idproducto);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {

                mensaje = "El producto ya está en el carrito, si desea agregar uno igual aumente su cantidad dentro del carrito";


            }
            else {

                respuesta = new CN_Pedido().OperacionPedido(idcliente, idproducto, true, out mensaje);



            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult CantidadEnPedido() {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Pedido().CantidadEnPedido(idcliente);
            return Json(new {cantidad = cantidad}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProductosPedido() {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<Pedidos> oLista = new List<Pedidos>();

            bool conversion;

            oLista = new CN_Pedido().ListarProducto(idcliente).Select(oc => new Pedidos()
            {

                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    NombreProducto = oc.oProducto.NombreProducto,
                    oCategoria = oc.oProducto.oCategoria,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)
                },

                    Cantidad = oc.Cantidad

            }).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OperacionPedido(int idproducto, bool sumar)
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new CN_Pedido().OperacionPedido(idcliente, idproducto, true, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarPedido(int idproducto) {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Pedido().EliminarPedido(idcliente, idproducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pedido() {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ProcesarPago(List<Pedidos> oListaPedido, Venta oVenta)
        {

            decimal total = 0;
            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("es-CL");

            detalle_venta.Columns.Add("IdProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(int));


            foreach (Pedidos oPedido in oListaPedido) {
                decimal subtotal = oPedido.Cantidad * oPedido.oProducto.Precio;

                total += subtotal;

                detalle_venta.Rows.Add(new object[] {
                    oPedido.oProducto.IdProducto,
                    oPedido.Cantidad,
                    subtotal
                });
            }

            oVenta.MontoTotal = total;
            oVenta.IdCliente = ((Cliente)Session["Cliente"]).IdCliente;

            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = detalle_venta;

            return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=code0001&status=true" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PagoEfectuado() {

            string idtransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;

            if (status) {
                Venta oVenta = (Venta)TempData["Venta"];

                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];

                oVenta.IdTransaccion = idtransaccion;

                string mensaje = string.Empty;

                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

                ViewData["IdTransaccion"] = oVenta.IdTransaccion;
            }

            return View();

        }



    }
}