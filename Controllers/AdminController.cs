using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGestionCanina_APIandMVC.Models;

namespace ProyectoGestionCanina_APIandMVC.Controllers
{
    public class AdminController : Controller
    {
        private static Usuario oUsuario;
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            oUsuario = (Usuario)Session["Usuario"];

            return View();
        }

        public ActionResult Password() {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}