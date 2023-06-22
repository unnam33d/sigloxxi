using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Producto
    {
        //public List<Producto> Listar()
        //{
        //    List<Producto> lista = new List<Producto>();

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_ListarProductos", oconexion);

        //            cmd.CommandType = CommandType.Text;

        //            oconexion.Open();

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    lista.Add(
        //                        new Producto()
        //                        {
        //                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
        //                            NombreProducto = dr["NombreProducto"].ToString(),
        //                            Receta = dr["Receta"].ToString(),
        //                            oIngrediente = new Ingredientes { IdIngrediente = Convert.ToInt32(dr["IngredientesIds"]), Descripcion = dr["NombresIngredientes"].ToString() },
        //                            oCategoria = new Categoria { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["Categoria"].ToString() },
        //                            Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CL")),
        //                            RutaImagen = dr["RutaImagen"].ToString(),
        //                            NombreImagen = dr["NombreImagen"].ToString(),
        //                            Activo = Convert.ToBoolean(dr["Activo"])
        //                        });
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lista = new List<Producto>();
        //        Console.WriteLine(ex);
        //    }
        //    return lista;
        //}

        //public int RegistrarProducto(Producto obj, out string Mensaje)
        //{
        //    int idautogenerado = 0;
        //    Mensaje = string.Empty;

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
        //            cmd.Parameters.AddWithValue("Nombre", obj.NombreProducto);
        //            cmd.Parameters.AddWithValue("Receta", obj.Receta);
        //            cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
        //            cmd.Parameters.AddWithValue("Precio", obj.Precio);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.AddWithValue("IngredientesIds", obj.oIngrediente.IdIngrediente);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();
        //            cmd.ExecuteNonQuery();

        //            idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        idautogenerado = 0;
        //        Mensaje = ex.Message;
        //    }
        //    return idautogenerado;
        //}


        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT p.IdProducto, p.NombreProducto, p.Receta,");
                    sb.AppendLine("i.IdIngrediente, i.Descripcion AS Ingrediente,");
                    sb.AppendLine("c.IdCategoria, c.Descripcion AS Categoria,");
                    sb.AppendLine("p.Precio, p.RutaImagen, p.NombreImagen, p.Activo");
                    sb.AppendLine("FROM producto p");
                    sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria");
                    sb.AppendLine("INNER JOIN INGREDIENTES i ON i.IdIngrediente = p.IdIngrediente");                    

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Receta = dr["Receta"].ToString(),
                                oIngrediente = new Ingredientes() { IdIngrediente = Convert.ToInt32(dr["IdIngrediente"]), Descripcion = dr["Ingrediente"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["Categoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["IdProducto"], new CultureInfo("es-CL")),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            }
                            );
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
                Console.WriteLine(ex);
            }
            return lista;
        }


        public int RegistrarProducto(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto2", oconexion);
                    cmd.Parameters.AddWithValue("NombreProducto", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Receta", obj.Receta);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdIngrediente", obj.oIngrediente.IdIngrediente);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        //public bool EditarProducto(Producto obj, out string Mensaje)
        //{
        //    bool resultado = false;
        //    Mensaje = string.Empty;

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
        //            cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
        //            cmd.Parameters.AddWithValue("Nombre", obj.NombreProducto);
        //            cmd.Parameters.AddWithValue("Receta", obj.Receta);
        //            cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
        //            cmd.Parameters.AddWithValue("Precio", obj.Precio);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.AddWithValue("IngredientesIds", obj.oIngrediente.IdIngrediente);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();
        //            cmd.ExecuteNonQuery();

        //            resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        Mensaje = ex.Message;
        //    }
        //    return resultado;
        //}

        public bool EditarProducto(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto2", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("NombreProducto", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("Receta", obj.Receta);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdIngrediente", obj.oIngrediente.IdIngrediente);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool GuardarDatosImagen(Producto oProducto, out string Mensaje) {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    string query = "UPDATE PRODUCTO SET RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where IdProducto = @IdProducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("rutaimagen", oProducto.RutaImagen);
                    cmd.Parameters.AddWithValue("nombreimagen", oProducto.NombreImagen);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.IdProducto);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la imagen";
                        resultado = false;

                    }
                    
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;

        }

        public bool EliminarProducto(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }




    }
}
