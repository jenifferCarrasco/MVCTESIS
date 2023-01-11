using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Models
{
    public class Canino1
    {
        public int IdCanino { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public PropietarioCanino oIdPropietario { get; set; }
        public decimal Peso { get; set; }
        public string Color { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}