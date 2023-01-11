using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGestionCanina_APIandMVC.Models;
using ProyectoGestionCanina_APIandMVC.Logica;

namespace ProyectoGestionCanina_APIandMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string contrasena)
        {

            Usuario ousuario = UsuarioLogica.Instancia.LoginUsuario().Where(u => u.Correo == correo && u.Contrasena == contrasena && u.Roles.IdRol != 3).FirstOrDefault();
            
            //PropietarioCanino oprop = PropietarioLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Contrasena == contrasena && u.Roles.IdRol == 4).FirstOrDefault();
            //Vacunadores ovac = VacunadoresLogica.Instancia.Listar().Where(u => u.Correo == correo && u.Contrasena == contrasena && u.Roles.IdRol == 3).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrecta!";
                return View();
            }
            //if (oprop == null)
            //{
            //    ViewBag.Error = "Usuario o contraseña incorrecta!";
            //    return View();
            //}
            //if (ovac == null)
            //{
            //    ViewBag.Error = "Usuario o contraseña incorrecta!";
            //    return View();
            //}

            Session["Usuario"] = ousuario;
            //Session["Vacunadores"] = ovac;
            //Session["PropietarioCanino"] = oprop;

            return RedirectToAction("Index", "Admin");
        }
    }
}