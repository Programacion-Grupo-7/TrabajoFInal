using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
       
        public ActionResult Index(Cancion c)
        { 
                List<Cancion> Lista = BD.TraerMusica();
                ViewBag.ListaCanciones = Lista;
                return View();   
        }

        public ActionResult Perfil()
        {
                ViewBag.Message = "Tu Perfil";
                return View();
        }

        public ActionResult Tendencia()
        {
            List<Cancion> Lista = BD.TOP50();
            ViewBag.ListaCanciones = Lista;
            return View();
        }
        public ActionResult Registrarse()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult SubirMusica()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubirMusica(Cancion n, HttpPostedFileBase Audio)
        {
               if (Audio != null && Audio.ContentLength > 0)
                {
                    string newUbication = Server.MapPath("~/Musica/") + Audio.FileName;
                    Audio.SaveAs(newUbication);
                    n.Ubicacion = Audio.FileName;
                    BD.IngresarMusica(n);
                }
                /*else
                {

                }*/
                return RedirectToAction("Index");
        }
        

        [HttpPost]
        public ActionResult Registrarse(Usuario u)
        {
            BD.Registrarse(u);
            return RedirectToAction("Perfil");
        }

        [HttpPost]
        public ActionResult LogIn(Usuario c)
        {
            Session["id"] = c.IdUsuario;
            ViewBag.LogIn = BD.LogIn(c);
            if(ViewBag.LogIn != null)
            {
                return View("Perfil");
            }
            else
            {
                return View("LogInError");
            }
            
        }

    }
}