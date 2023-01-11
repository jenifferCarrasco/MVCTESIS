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
    public class CaninoLogica
    {
        private static CaninoLogica instancia = null;

        public CaninoLogica()
        {

        }

        public static CaninoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CaninoLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Canino operro)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCanino", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", operro.Nombre);
                    cmd.Parameters.AddWithValue("Raza", operro.Raza);
                    cmd.Parameters.AddWithValue("Sexo",operro.Sexo);
                    cmd.Parameters.AddWithValue("IdPropietario", operro.PropietarioCanino.IdPropietario);
                    cmd.Parameters.AddWithValue("Peso", operro.Peso);
                    cmd.Parameters.AddWithValue("Color", operro.Color);
                    cmd.Parameters.AddWithValue("FechaNacimiento",operro.FechaNacimiento);
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

        public bool Modificar(Canino operro)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCanino", oConexion);
                    cmd.Parameters.AddWithValue("IdCanino", operro.IdCanino);
                    cmd.Parameters.AddWithValue("Nombre", operro.Nombre);
                    cmd.Parameters.AddWithValue("Raza", operro.Raza);
                    cmd.Parameters.AddWithValue("Sexo", operro.Sexo);
                    cmd.Parameters.AddWithValue("IdPropietario", operro.PropietarioCanino.IdPropietario);
                    cmd.Parameters.AddWithValue("Peso", operro.Peso);
                    cmd.Parameters.AddWithValue("Color", operro.Color);
                    cmd.Parameters.AddWithValue("FechaNacimiento", Convert.ToDateTime(operro.FechaNacimiento));
                    cmd.Parameters.AddWithValue("Estado", operro.Estado);
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


        public List<Canino> Listar()
        {
            List<Canino> Lista = new List<Canino>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select l.IdCanino,l.Nombre,l.Raza,l.Sexo,l.IdPropietario,a.Nombre[NombrePropietario],a.Apellido,l.Peso,l.Color,l.FechaNacimiento,l.Estado from Canino l");
                sb.AppendLine("inner join PropietarioCanino a on a.IdPropietario = l.IdPropietario");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                     oConexion.Open();
                     SqlDataReader dr = cmd.ExecuteReader();
                     while (dr.Read())
                     {
                        Lista.Add(new Canino()
                        {
                            IdCanino = Convert.ToInt32(dr["IdCanino"]),
                            Nombre = dr["Nombre"].ToString(),
                            Raza = dr["Raza"].ToString(),
                            Sexo = (dr["Sexo"]).ToString(),
                            PropietarioCanino = new PropietarioCanino() { IdPropietario = Convert.ToInt32(dr["IdPropietario"].ToString()), Nombre = dr["NombrePropietario"].ToString(), Apellido = dr["Apellido"].ToString() },
                            Peso = Convert.ToDecimal(dr["Peso"]),
                            Color = dr["Color"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        }) ;
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
                    SqlCommand cmd = new SqlCommand("delete from Canino where IdCanino = @id", oConexion);
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