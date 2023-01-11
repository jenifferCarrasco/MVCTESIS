using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using ProyectoGestionCanina_APIandMVC.Models;

namespace ProyectoGestionCanina_APIandMVC.Logica
{
    public class VacunacionesLogica
    {
        private static VacunacionesLogica instancia = null;

        public VacunacionesLogica()
        {

        }

        public static VacunacionesLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VacunacionesLogica();
                }

                return instancia;
            }
        }

        public List<Vacunaciones> Listar()
        {

            List<Vacunaciones> rptListaLibro = new List<Vacunaciones>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select l.IdVacunacion");
                sb.AppendLine("a.IdCentro,a.Nombre[NombreCentro],");
                sb.AppendLine("b.IdVacuna,b.Nombre[NombreVacuna],");
                sb.AppendLine("c.IdCanino,c.Nombre[NombreCanino],");
                sb.AppendLine("d.IdVacunador,d.Nombre[NombreVacunador],d.Apellido[ApellidoVacunador]");
                sb.AppendLine("l.Dosis,l.FechaCreacion,l.FechaProxima");
                sb.AppendLine("from Vacunaciones l");
                sb.AppendLine("inner join Centros a on a.IdCentro = l.IdCentro");
                sb.AppendLine("inner join Vacunas b on b.IdVacuna = l.IdVacuna");
                sb.AppendLine("inner join Canino c on c.IdCanino = l.IdCanino");
                sb.AppendLine("inner join Vacunadores d on d.IdVacunador = l.IdVacunador");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaLibro.Add(new Vacunaciones()
                        {
                            IdVacunacion = Convert.ToInt32(dr["IdVacunacion"].ToString()),
                            Centros = new Centros() { IdCentro = Convert.ToInt32(dr["IdCentro"].ToString()), Nombre = dr["NombreCentro"].ToString() },
                            Vacunas = new Vacunas() { IdVacuna = Convert.ToInt32(dr["IdVacuna"].ToString()), Nombre = dr["NombreVacuna"].ToString() },
                            Canino = new Canino() { IdCanino = Convert.ToInt32(dr["IdCanino"].ToString()), Nombre = dr["NombreCanino"].ToString() },
                            Vacunadores = new Vacunadores() { IdVacunador = Convert.ToInt32(dr["IdVacunador"].ToString()), Nombre = dr["NombreVacunador"].ToString(), Apellido = dr["ApellidoVacunador"].ToString() },
                            Dosis = Convert.ToInt32(dr["Dosis"]),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString()),
                            FechaProxima = Convert.ToDateTime(dr["FechaProxima"].ToString()),
                           
                        });
                    }
                    dr.Close();

                    return rptListaLibro;

                }
                catch (Exception ex)
                {
                    rptListaLibro = null;
                    return rptListaLibro;
                }
            }
        }


        public int Registrar(Vacunaciones objeto)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarVacunacion", oConexion);
                    cmd.Parameters.AddWithValue("IdCentro", objeto.Centros.IdCentro);
                    cmd.Parameters.AddWithValue("IdVacuna", objeto.Vacunas.IdVacuna);
                    cmd.Parameters.AddWithValue("IdCanino", objeto.Canino.IdCanino);
                    cmd.Parameters.AddWithValue("IdVacunador", objeto.Vacunadores.IdVacunador);
                    cmd.Parameters.AddWithValue("Dosis", objeto.Dosis);
                    cmd.Parameters.AddWithValue("FechaProxima", objeto.FechaProxima);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }


        public bool Modificar(Vacunaciones objeto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_modificarVacunacion", oConexion);
                    cmd.Parameters.AddWithValue("IdVacunacion", objeto.IdVacunacion);
                    cmd.Parameters.AddWithValue("IdCentro", objeto.Centros.IdCentro);
                    cmd.Parameters.AddWithValue("IdVacuna", objeto.Vacunas.IdVacuna);
                    cmd.Parameters.AddWithValue("IdCanino", objeto.Canino.IdCanino);
                    cmd.Parameters.AddWithValue("IdVacunador", objeto.Vacunadores.IdVacunador);
                    cmd.Parameters.AddWithValue("Dosis", objeto.Dosis);
                    cmd.Parameters.AddWithValue("FechaProxima", objeto.FechaProxima);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        


        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Vacunaciones where IdVacunacion = @id", oConexion);
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