using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using ProyectoGestionCanina_APIandMVC.Models;

namespace ProyectoGestionCanina_APIandMVC.Logica
{
    public class CitasLogica
    {
        private static CitasLogica instancia = null;

        public CitasLogica()
        {

        }

        public static CitasLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CitasLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Citas objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCitas", oConexion);
                    cmd.Parameters.AddWithValue("IdVacunador", objeto.Vacunadores.IdVacunador);
                    cmd.Parameters.AddWithValue("IdPropietario", objeto.PropietarioCanino.IdPropietario);
                    cmd.Parameters.AddWithValue("IdCanino", objeto.Canino.IdCanino);
                    cmd.Parameters.AddWithValue("IdCentro", objeto.Centros.IdCentro);
                    cmd.Parameters.AddWithValue("IdEstadoCita", objeto.EstadoCitas.IdEstadoCita);
                    cmd.Parameters.AddWithValue("Fecha", Convert.ToDateTime(objeto.Fecha, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Hota", objeto.Hota);
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
       
        public bool Existe(Citas objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_existeCita", oConexion);
                    cmd.Parameters.AddWithValue("IdCanino", objeto.Canino.IdCanino);
                    cmd.Parameters.AddWithValue("IdCita", objeto.IdCita);
                    cmd.Parameters.AddWithValue("IdVacunador", objeto.Vacunadores.IdVacunador);
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

        public List<EstadoCitas> ListarEstados()
        {
            List<EstadoCitas> Lista = new List<EstadoCitas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdEstadoCita,Descripcion from EstadoCitas", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new EstadoCitas()
                            {
                                IdEstadoCita = Convert.ToInt32(dr["IdEstadoCita"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<EstadoCitas>();
                }
            }
            return Lista;
        }


        public List<Citas> Listar(int idestadocita, int idcanino, int idvacunador)
        {
            List<Citas> Lista = new List<Citas>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("set dateformat dmy");
                    query.AppendLine("select p.IdCita,ec.IdEstadoCita,ec.Descripcion,c.Nombre[NombreC],v.Nombre[NombreV],v.Apellido[ApellidoV],pc.Nombre[NombreP],pc.Apellido[ApellidoP],convert(char(10),p.Fecha,103)[Fecha],p.Hota from Citas p");
                    query.AppendLine("inner join EstadoCitas ec on ec.IdEstadoCita = p.IdEstadoCita");
                    query.AppendLine("inner join Canino c on c.IdCanino = p.IdCanino");
                    query.AppendLine("inner join Vacunador v on v.IdVacunador = p.IdVacunador");
                    query.AppendLine("inner join PropietarioCanino pc on pc.IdPropietario = p.IdPropietario");
                    query.AppendLine("where ec.IdEstadoCita = iif(@idestadocita=0,ec.IdEstadoCita,@idestadocita) and c.IdCanino = iif(@idcanino=0,c.IdCanino,@idcanino) and v.IdVacunador = iif(@idvacunador=0,v.IdVacunador,@idvacunador)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@idestadocita", idestadocita);
                    cmd.Parameters.AddWithValue("@idcanino", idcanino);
                    cmd.Parameters.AddWithValue("@idvacunador", idvacunador);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Citas()
                            {
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                EstadoCitas = new EstadoCitas() { IdEstadoCita = Convert.ToInt32(dr["IdEstadoCita"]), Descripcion = dr["Descripcion"].ToString() },
                                Canino = new Canino() { IdCanino = Convert.ToInt32(dr["IdCanino"]), Nombre = dr["NombreC"].ToString()},
                                PropietarioCanino = new PropietarioCanino() { IdPropietario = Convert.ToInt32(dr["IdPropietario"]), Nombre = dr["NombreP"].ToString(), Apellido = dr["ApellidoP"].ToString() },
                                Vacunadores = new Vacunadores() { IdVacunador = Convert.ToInt32(dr["IdVacunador"]), Nombre = dr["NombreV"].ToString(), Apellido = dr["ApellidoV"].ToString() },
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                Hota = (TimeSpan)dr["Hota"],
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Citas>();
                }
            }
            return Lista;
        }

        public bool Devolver(int idcita)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Citas set IdEstadoCita = 2");
                    query.AppendLine("where IdCita = @idcita");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@idcita", idcita);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
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