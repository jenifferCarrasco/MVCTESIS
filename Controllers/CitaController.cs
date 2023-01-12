using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoGestionCanina_APIandMVC.Models;
using ProyectoGestionCanina_APIandMVC.Logica;

namespace ProyectoGestionCanina_APIandMVC.Controllers
{
    public class CitaController : Controller
    {
        // GET: Citas
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Consultar()
        {
            return View();
        }

        public ActionResult ConsultarCitas()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GuardarCitas(Citas objeto)
        {
            bool _respuesta = false;
            string _mensaje = string.Empty;

            _respuesta = CitasLogica.Instancia.Existe(objeto);

            if (_respuesta)
            {
                _respuesta = false;
                _mensaje = "Ya tienes una cita agendada con el vacunador en el mismo horario";
            }
            else
            {
                _respuesta = CitasLogica.Instancia.Registrar(objeto);
                _mensaje = _respuesta ? "Registro completo" : "No se pudo registrar";
            }


            return Json(new { resultado = _respuesta, mensaje = _mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstados()
        {
            List<EstadoCitas> oLista = new List<EstadoCitas>();
            oLista = CitasLogica.Instancia.ListarEstados();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Listar(int idestadocita, int idcanino, int idvacunador)
        {
            List<Citas> oLista = new List<Citas>();
            oLista = CitasLogica.Instancia.Listar(idestadocita, idcanino, idvacunador);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCitasPorPropietarios(int idpropietario)
        {
            List<Citas> oLista = new List<Citas>();
            oLista = CitasLogica.Instancia.ListarCitasPorPropietarios(idpropietario);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult Devolver(int idcita)
        {
            bool respuesta = false;
            respuesta = CitasLogica.Instancia.Devolver(idcita);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}