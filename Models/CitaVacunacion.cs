//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoGestionCanina_APIandMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CitaVacunacion
    {
        public int IdCita { get; set; }
        public int IdVacunador { get; set; }
        public int IdPropietario { get; set; }
        public int IdCentro { get; set; }
        public int IdCanino { get; set; }
        public string EstadoCita { get; set; }
        public string Observacion { get; set; }
        public System.DateTime FechaAtencion { get; set; }
        public System.DateTime FechaAtencionI { get; set; }
        public System.DateTime FechaAtencionF { get; set; }
        public System.TimeSpan HoraI { get; set; }
        public System.TimeSpan HoraF { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    
        public virtual Canino Canino { get; set; }
        public virtual Centros Centros { get; set; }
        public virtual PropietarioCanino PropietarioCanino { get; set; }
        public virtual Vacunadores Vacunadores { get; set; }
    }
}