using System;
using System.Collections.Generic;
using ProyectoGestionCanina_APIandMVC.Models;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoGestionCanina_APIandMVC.Logica
{
    public class CentrosLogica
    {
        private static CentrosLogica instancia = null;

        public CentrosLogica()
        {

        }

        public static CentrosLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CentrosLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Centros oCentro)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCentros", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oCentro.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oCentro.Direccion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Centros oCentro)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCentro", oConexion);
                    cmd.Parameters.AddWithValue("IdCentro", oCentro.IdCentro);
                    cmd.Parameters.AddWithValue("Nombre", oCentro.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oCentro.Direccion);
                    cmd.Parameters.AddWithValue("Estado",oCentro.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<Centros> Listar()
        {
            List<Centros> Lista = new List<Centros>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdCentro,Nombre,Direccion,Estado from Centros", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Centros()
                            {
                                IdCentro = Convert.ToInt32(dr["IdCentro"]),
                                Nombre = dr["Nombre"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Centros>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Centros where IdCentro = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

    }
}