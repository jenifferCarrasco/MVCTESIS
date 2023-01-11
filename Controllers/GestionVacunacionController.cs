using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGestionCanina_APIandMVC.Models;
using ProyectoGestionCanina_APIandMVC.Logica;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoGestionCanina_APIandMVC.Controllers
{
    public class GestionVacunacionController : Controller
    {
        // GET: GestionVacunacion
        public ActionResult Centros()
        {
            return View();
        }
        public ActionResult Vacunadores()
        {
            return View();
        }

        public ActionResult Vacunas()
        {
            return View();
        }

        public ActionResult Vacunaciones()
        {
            return View();
        }

        public ActionResult Caninos()
        {
            return View();
        }

        public ActionResult Perfil() {
            return View();
        }
        public ActionResult PerfilEdit()
        {
            return View();
        }

        /// <summary>
        /// ////////CENTROOOOOS
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarCentros()
        {
            List<Centros> oLista = new List<Centros>();
            oLista = CentrosLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarCentro(Centros objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCentro == 0) ? CentrosLogica.Instancia.Registrar(objeto) : CentrosLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarCentro(int id)
        {
            bool respuesta = false;
            respuesta = CentrosLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// //////////VACUNAAAAAAA
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarVacunas()
        {
            List<Vacunas> oLista = new List<Vacunas>();
            oLista = VacunasLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarVacunas(Vacunas objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdVacuna == 0) ? VacunasLogica.Instancia.Registrar(objeto) : VacunasLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarVacunas(int id)
        {
            bool respuesta = false;
            respuesta = VacunasLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// //////////////////CANINOOOOOOOOO
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarCanino()
        {
            List<Canino> oLista = new List<Canino>();
            oLista = CaninoLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarCanino(Canino objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCanino == 0) ? CaninoLogica.Instancia.Registrar(objeto) : CaninoLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarCanino(int id)
        {
            bool respuesta = false;
            respuesta = CaninoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ///////
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarVacunaciones()
        {
            List<Vacunaciones> oLista = new List<Vacunaciones>();

            oLista = VacunacionesLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarVacunaciones(string objeto)
        {

            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Vacunaciones oVacunaciones = new Vacunaciones();
                oVacunaciones = JsonConvert.DeserializeObject<Vacunaciones>(objeto);

               

                if (oVacunaciones.IdVacunacion == 0)
                {
                    int id = VacunacionesLogica.Instancia.Registrar(oVacunaciones);
                    oVacunaciones.IdVacunacion = id;
                    oresponse.resultado = oVacunaciones.IdVacunacion == 0 ? false : true;

                }
                else
                {
                    oresponse.resultado = VacunacionesLogica.Instancia.Modificar(oVacunaciones);
                }


              

            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarVacunacion(int id)
        {
            bool respuesta = false;
            respuesta = VacunacionesLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// /ROLEESSSSSSS
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Roles> oLista = new List<Roles>();
            oLista = Rol.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// USUARRIOOOOOOOO
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<Usuario> oLista = new List<Usuario>();

            oLista = UsuarioLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            bool respuesta = false;
            objeto.Contrasena = objeto.Contrasena == null ? "" : objeto.Contrasena;
            respuesta = (objeto.IdUsuario == 0) ? UsuarioLogica.Instancia.Registrar(objeto) : UsuarioLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            respuesta = UsuarioLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        ///////////PROPIEEETARIOOOOOOOO
        //////////////

        [HttpGet]
        public JsonResult ListarPropietario()
        {
            List<PropietarioCanino> oLista = new List<PropietarioCanino>();

            oLista = PropietarioLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPropietario(PropietarioCanino objeto)
        {
            bool respuesta = false;
            objeto.Contrasena = objeto.Contrasena == null ? "" : objeto.Contrasena;
            respuesta = (objeto.IdPropietario == 0) ? PropietarioLogica.Instancia.Registrar(objeto) : PropietarioLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPropietario(int id)
        {
            bool respuesta = false;
            respuesta = PropietarioLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// ///VACUNADORRRRRRRR
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ListarVacunador()
        {
            List<Vacunadores> oLista = new List<Vacunadores>();

            oLista = VacunadoresLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarVacunador(Vacunadores objeto)
        {
            bool respuesta = false;
            objeto.Contrasena = objeto.Contrasena == null ? "" : objeto.Contrasena;
            respuesta = (objeto.IdVacunador == 0) ? VacunadoresLogica.Instancia.Registrar(objeto) : VacunadoresLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarVacunador(int id)
        {
            bool respuesta = false;
            respuesta = VacunadoresLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

    }
    
    public class Response
    {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
}