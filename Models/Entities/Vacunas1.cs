using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Models
{
    public class Vacunas1
    {
        public int IdVacuna { get; set; }
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Lote { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaCaducidad { get; set; }

        public bool Estado { get; set; }
    }
}