using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGestionCanina_APIandMVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Usuario()
        {
            return View();
        }

        

        public ActionResult Propietario()
        {
            return View();
        }
    }
}