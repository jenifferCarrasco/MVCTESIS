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
    
    public partial class Vacunas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacunas()
        {
            this.Vacunaciones = new HashSet<Vacunaciones>();
        }
    
        public int IdVacuna { get; set; }
        public string Nombre { get; set; }
        public string Laboratorio { get; set; }
        public string Lote { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaCaducidad { get; set; }
        public bool Estado { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacunaciones> Vacunaciones { get; set; }
    }
}
