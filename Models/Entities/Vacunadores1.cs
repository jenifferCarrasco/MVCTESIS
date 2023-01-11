using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Models
{
    public class Vacunadores1
    {
        public int IdVacunador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Ocupacion { get; set; }
        public string Direccion { get; set; }
        public Roles oIdRol { get; set; }
        public bool Estado { get; set; }
    }
}