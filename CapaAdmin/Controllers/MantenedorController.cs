using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

namespace CapaAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Ingredientes()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }


        #region CATEGORIA

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().RegistrarCategoria(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().EditarCategoria(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().EliminarCategoria(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region INGREDIENTES

        [HttpGet]
        public JsonResult ListarIngredientes()
        {
            List<Ingredientes> oLista = new List<Ingredientes>();

            oLista = new CN_Ingredientes().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarIngrediente(Ingredientes objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdIngrediente == 0)
            {
                resultado = new CN_Ingredientes().RegistrarIngrediente(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Ingredientes().EditarIngrediente(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EliminarIngrediente(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Ingredientes().EliminarIngrediente(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region PRODUCTO

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool operacion_existosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            try
            {
                oProducto = JsonConvert.DeserializeObject<Producto>(objeto);
            }
            catch (Exception)
            {

                
            }                
                

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, System.Globalization.NumberStyles.AllowDecimalPoint, new CultureInfo("es-CL"), out precio))
            {
                oProducto.Precio = precio;

            }
            else
            {

                try
                {
                    return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                    
                }
                
            }



            if (oProducto.IdProducto == 0)
            {
                int IdProductoGenerado = new CN_Producto().RegistrarProducto(oProducto, out mensaje);

                if (IdProductoGenerado != 0)
                {
                    oProducto.IdProducto = IdProductoGenerado;
                }
                else {
                    operacion_existosa = false;
                }

            }
            else
            {
                operacion_existosa = new CN_Producto().EditarProducto(oProducto, out mensaje);
            }

            if (operacion_existosa)
            {
                if(archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(),extension);

                    try {

                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;

                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardó el producto, pero hubo problemas al cargar la imagen.";
                    }

                }
            }
            return Json(new { operacionExitosa = operacion_existosa, igGenerado = oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen,oProducto.NombreImagen), out conversion);

            return Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oProducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().EliminarProducto(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    } 
}