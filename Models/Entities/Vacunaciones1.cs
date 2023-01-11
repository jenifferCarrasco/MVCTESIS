using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Models
{
    public class Vacunaciones1
    {
        public int IdVacunacion { get; set; }
        public Centros oIdCentro { get; set; }

        public Vacunas oIdVacuna { get; set; }

        public Canino oIdCanino { get; set; }

        public Vacunadores oIdVacunador { get; set; }

        public int Dosis { get; set; }

        public DateTime FechaProxima { get; set; }
        public DateTime FechaCreacion { get; internal set; }
    }
}