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
    public class VacunadoresLogica
    {
        private static VacunadoresLogica instancia = null;

        public VacunadoresLogica()
        {

        }

        public static VacunadoresLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VacunadoresLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Vacunadores oVacunadores)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVacunador", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oVacunadores.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oVacunadores.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", oVacunadores.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oVacunadores.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oVacunadores.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oVacunadores.Contrasena);
                    cmd.Parameters.AddWithValue("Ocupacion", oVacunadores.Ocupacion);
                    cmd.Parameters.AddWithValue("Direccion", oVacunadores.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", oVacunadores.Roles.IdRol);
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

        public bool Modificar(Vacunadores oVacunadores)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarVacunador", oConexion);
                    cmd.Parameters.AddWithValue("IdVacunador", oVacunadores.IdVacunador);
                    cmd.Parameters.AddWithValue("Nombre", oVacunadores.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oVacunadores.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", oVacunadores.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oVacunadores.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oVacunadores.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oVacunadores.Contrasena);
                    cmd.Parameters.AddWithValue("Ocupacion", oVacunadores.Ocupacion);
                    cmd.Parameters.AddWithValue("Direccion", oVacunadores.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", oVacunadores.Roles.IdRol);
                    cmd.Parameters.AddWithValue("Estado", oVacunadores.Estado);
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

        public Vacunadores ObtenerMedico(Int32 medicoId)
        {
            SqlCommand cmd = null;
            Vacunadores item = null;
            try
            {
                SqlConnection cn = new SqlConnection(Conexion.CN);
                cmd = new SqlCommand("GC_LEER_MEDICO_SP", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", medicoId);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    item = new Vacunadores();
                    item.IdVacunador = Convert.ToInt32(dr["IdVacunador"]);
                    item.Nombre = dr["Nombre"].ToString();
                    item.Apellido = dr["Apellido"].ToString();
                    item.Cedula = dr["Cedula"].ToString();
                    item.Telefono = dr["Telefono"].ToString();
                    item.Correo = dr["Correo"].ToString();
                    item.Contrasena = dr["Contrasena"].ToString();
                    item.Ocupacion = dr["Ocupacion"].ToString();
                    item.Direccion = dr["Direccion"].ToString();
                    item.Roles = new Roles() { IdRol = Convert.ToInt32(dr["IdRol"].ToString()), Descripcion = dr["Descripcion"].ToString() };
                    item.Estado = Convert.ToBoolean(dr["Estado"]);


                }
                return item;
            }
            catch (Exception e) { throw e; }
            finally { if (cmd != null) { cmd.Connection.Close(); } }
        }
        public List<Vacunadores> Listar()
        {
            List<Vacunadores> Lista = new List<Vacunadores>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
               
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select l.IdVacunador,l.Nombre,l.Apellido,l.Cedula,l.Telefono,l.Correo,l.Contrasena,l.Ocupacion,l.Direccion,a.IdRol,a.Descripcion, l.Estado from Vacunadores l");
                    sb.AppendLine("inner join Roles a on a.IdRol = l.IdRol");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    try
                    {
                        oConexion.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Lista.Add(new Vacunadores()
                            {
                                IdVacunador = Convert.ToInt32(dr["IdVacunador"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Ocupacion = dr["Ocupacion"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Roles = new Roles() { IdRol = Convert.ToInt32(dr["IdRol"].ToString()), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                        dr.Close();

                        return Lista;

                    }
                    catch (Exception ex)
                    {
                        Lista = null;
                        return Lista;
                    }
                    ///////

                  
            }
           
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Vacunadores where IdVacunador = @id", oConexion);
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
        public List<Vacunadores> ListarVacunadorPorHorario(String opcionBusqueda, DateTime fechaAtencion, String filtro)
        {
            SqlCommand cmd = null;
            List<Vacunadores> lista = new List<Vacunadores>();
            try
            {

                SqlConnection cn = new SqlConnection(Conexion.CN);
                cmd = new SqlCommand("GC_BUSCAR_HORARIO", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TIPOBUSQUEDA", opcionBusqueda);
                cmd.Parameters.AddWithValue("@FECHAATENCION", fechaAtencion);
                cmd.Parameters.AddWithValue("@FILTRO", filtro);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Vacunadores objMedico = new Vacunadores();
                    objMedico.IdVacunador = Convert.ToInt32(dr["IdVacunador"]);
                    objMedico.Nombre = dr["Nombre"].ToString();
                    objMedico.Apellido = dr["Apellido"].ToString();
                    lista.Add(objMedico);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return lista;
        }
    }
}