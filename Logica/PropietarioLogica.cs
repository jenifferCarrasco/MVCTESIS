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
    public class PropietarioLogica
    {
        private static PropietarioLogica instancia = null;

        public PropietarioLogica()
        {

        }

        public static PropietarioLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PropietarioLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(PropietarioCanino oPropietario)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPropietario", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oPropietario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oPropietario.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", oPropietario.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oPropietario.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oPropietario.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oPropietario.Contrasena);
                    cmd.Parameters.AddWithValue("Direccion", oPropietario.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", oPropietario.Roles.IdRol);
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

        public bool Modificar(PropietarioCanino oPropietario)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarPropietario", oConexion);
                    cmd.Parameters.AddWithValue("IdPropietario", oPropietario.IdPropietario);
                    cmd.Parameters.AddWithValue("Nombre", oPropietario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oPropietario.Apellido);
                    cmd.Parameters.AddWithValue("Cedula", oPropietario.Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oPropietario.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oPropietario.Correo);
                    cmd.Parameters.AddWithValue("Contrasena", oPropietario.Contrasena);
                    cmd.Parameters.AddWithValue("Direccion", oPropietario.Direccion);
                    cmd.Parameters.AddWithValue("IdRol", oPropietario.Roles.IdRol);
                    cmd.Parameters.AddWithValue("Estado", oPropietario.Estado);
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


        public List<PropietarioCanino> Listar()
        {
            List<PropietarioCanino> Lista = new List<PropietarioCanino>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select l.IdPropietario,l.Nombre,l.Apellido,l.Cedula,l.Telefono,l.Correo,l.Contrasena,l.Direccion,a.IdRol,a.Descripcion, l.Estado from PropietarioCanino l");
                sb.AppendLine("inner join Roles a on a.IdRol = l.IdRol");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Lista.Add(new PropietarioCanino()
                        {
                            IdPropietario = Convert.ToInt32(dr["IdPropietario"]),
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
                    SqlCommand cmd = new SqlCommand("delete from PropietarioCanino where IdPropietario = @id", oConexion);
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