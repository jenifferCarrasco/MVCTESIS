using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGestionCanina_APIandMVC.Logica.Common
{
    public class RespuestaSistema
    {
        public String Mensaje { get; set; }
        public Boolean Correcto { get; set; }
        public Int32 LlaveInsertada { get; set; }
        public Int32 LlaveModificada { get; set; }
        public Int16 Accion { get; set; }
    }
}