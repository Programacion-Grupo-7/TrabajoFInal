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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrarse(Usuario c)
        {
            ViewBag.Registro = BD.Registrarse(c); 
            return View();
        }
        public ActionResult LogIn(Usuario c)
        {
            Session["id"] = c.IdUsuario;
            ViewBag.LogIn = BD.LogIn(c); 
            return View();
        }
        [HttpPost]
        public ActionResult ingresarMusica(Cancion n, HttpPostedFileBase Audio)
        {
            n.Ubicacion = Audio.FileName;
            ViewBag.Cancion = BD.IngresarMusica(n);
            Audio.SaveAs(Server.MapPath("~/Musica/") + Audio.FileName);
            return RedirectToAction("Index");
            
        }
    }
}