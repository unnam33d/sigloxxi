using System;
using System.Collections.Generic;
using CapaEntidad;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    IdPedido = dr["IdPedido"].ToString(),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-CL")),
                                    IdTransaccion = dr["IdTransaccion"].ToString()
                                });
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                lista = new List<Reporte>();
                Console.WriteLine(ex);
            }


            return lista;
        }


        public Dashboard VerDashBoard()
        {
            Dashboard objeto = new Dashboard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.Cn))
                {
                    

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                                objeto = new Dashboard()
                                {
                                    TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                    TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                    TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                };
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                objeto = new Dashboard();
            }


            return objeto;
        }
    }
}
