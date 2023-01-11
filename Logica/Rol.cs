using System;
using System.Collections.Generic;
using ProyectoGestionCanina_APIandMVC.Models;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoGestionCanina_APIandMVC.Logica
{
    public class Rol
    {
        private static Rol instancia = null;

        public Rol()
        {

        }

        public static Rol Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Rol();
                }

                return instancia;
            }
        }

        public List<Roles> Listar()
        {
            List<Roles> Lista = new List<Roles>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdRol,Descripcion from Roles", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Roles()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Roles>();
                }
            }
            return Lista;
        }
    }
}
