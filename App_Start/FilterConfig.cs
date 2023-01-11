using System.Web;
using System.Web.Mvc;

namespace ProyectoGestionCanina_APIandMVC
{
    
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
