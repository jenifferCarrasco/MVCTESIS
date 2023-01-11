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
    public class UsuarioLogica
    {
        private static UsuarioLogica instancia = null;

        public UsuarioLogica()
        {

        }

        public static UsuarioLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new UsuarioLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Usuario ouser)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", ouser.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", ouser.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", ouser.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", ouser.Telefono);
                    cmd.Parameters.AddWithValue("Correo", ouser.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", ouser.Contrasena);
                    cmd.Parameters.AddWithValue("Direccion", ouser.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", ouser.Roles.IdRol);
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

        public bool Modificar(Usuario oUser)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oUser.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", oUser.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oUser.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", oUser.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oUser.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oUser.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oUser.Contrasena);
                    cmd.Parameters.AddWithValue("Direccion", oUser.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", oUser.Roles.IdRol);
                    cmd.Parameters.AddWithValue("Estado", oUser.Estado);
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
        public List<Usuario> LoginUsuario()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    //sb.AppendLine("select l.IdUsuario,l.Nombre,l.Apellido,l.Cedula,l.Telefono,l.Correo,l.Contrasena,l.Direccion,l.IdRol,a.Descripcion,l.Estado from Usuario l inner join Roles a on a.IdRol = l.IdRol");
                    //sb.AppendLine("select * from Usuario");
                    sb.AppendLine("select l.IdUsuario,l.Nombre,l.Apellido,l.Cedula,l.Telefono,l.Correo,l.Contrasena,l.Direccion,l.IdRol,a.Descripcion, l.Estado from Usuario l inner join Roles a on a.IdRol = l.IdRol");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;


                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                //Ocupacion = dr["Ocupacion"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Roles = new Roles() { IdRol = Convert.ToInt32(dr["IdRol"].ToString()), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                }

            }
            return Lista;
        }


        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select l.IdUsuario,l.Nombre,l.Apellido,l.Cedula,l.Telefono,l.Correo,l.Contrasena,l.Direccion,l.IdRol,a.Descripcion, l.Estado from Usuario l inner join Roles a on a.IdRol = l.IdRol");
                    //sb.AppendLine("select * from Usuario");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;


                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                //Ocupacion = dr["Ocupacion"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Roles = new Roles() { IdRol = Convert.ToInt32(dr["IdRol"].ToString()), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
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
                    SqlCommand cmd = new SqlCommand("delete from Usuario where IdUsuario = @id", oConexion);
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