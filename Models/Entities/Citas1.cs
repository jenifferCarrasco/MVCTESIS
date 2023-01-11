using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Models
{
    public class Citas1
    {
        public int IdCita { get; set; }

        public Vacunadores oIdVacunador { get; set; }

        public PropietarioCanino oIdPropietario { get; set; }

        public Centros oIdCentro { get; set; }

        public Canino oIdCanino { get; set; }

        public EstadoCitas oIdEstadoCita { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hota { get; set; }

        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; internal set; }
    }
}