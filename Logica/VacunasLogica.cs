using System;
using System.Collections.Generic;
using ProyectoGestionCanina_APIandMVC.Models;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ProyectoGestionCanina_APIandMVC.Logica
{
    public class VacunasLogica
    {
        private static VacunasLogica instancia = null;

        public VacunasLogica()
        {

        }

        public static VacunasLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VacunasLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Vacunas oVacunas)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVacuna", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oVacunas.Nombre);
                    cmd.Parameters.AddWithValue("Laboratorio", oVacunas.Laboratorio);
                    cmd.Parameters.AddWithValue("Lote", oVacunas.Lote);
                    cmd.Parameters.AddWithValue("Descripcion", oVacunas.Descripcion);
                    cmd.Parameters.AddWithValue("FechaCaducidad", oVacunas.FechaCaducidad);
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

        public bool Modificar(Vacunas oVacuna)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarVacuna", oConexion);
                    cmd.Parameters.AddWithValue("IdVacuna", oVacuna.IdVacuna);
                    cmd.Parameters.AddWithValue("Nombre", oVacuna.Nombre);
                    cmd.Parameters.AddWithValue("Laboratorio", oVacuna.Laboratorio);
                    cmd.Parameters.AddWithValue("Lote", oVacuna.Lote);
                    cmd.Parameters.AddWithValue("Descripcion", oVacuna.Descripcion);
                    cmd.Parameters.AddWithValue("FechaCaducacion", oVacuna.FechaCaducidad);
                    cmd.Parameters.AddWithValue("Estado", oVacuna.Estado);
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


        public List<Vacunas> Listar()
        {
            List<Vacunas> Lista = new List<Vacunas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdVacuna,Nombre,Laboratorio,Lote,Descripcion,FechaCaducidad,Estado from Vacunas", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Vacunas()
                            {
                                IdVacuna = Convert.ToInt32(dr["IdVacuna"]),
                                Nombre = dr["Nombre"].ToString(),
                                Laboratorio = dr["Laboratorio"].ToString(),
                                Lote = dr["Lote"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaCaducidad = Convert.ToDateTime(dr["FechaCaducidad"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Vacunas>();
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
                    SqlCommand cmd = new SqlCommand("delete from Vacunas where IdVacuna = @id", oConexion);
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