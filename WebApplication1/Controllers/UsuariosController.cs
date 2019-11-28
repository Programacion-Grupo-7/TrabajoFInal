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
        
       
        public ActionResult Index(Cancion c,string busqueda)
        {
            if (Session["id"] != null)
            {
                List<Cancion> Lista = BD.TraerMusica();
                ViewBag.ListaCanciones = Lista;
                return View();
            }
            else
            {
                return View("LogInError");
            }
        }


        public ActionResult Perfil(Usuario u)
        {
            if (Session["id"] != null)
            {
                u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
                u = BD.ObtenerUsuario(u.IdUsuario);
                List<Cancion> Lista = BD.TraerMiMusica(u);
                ViewBag.Lista = Lista;
                u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
                ViewBag.Usuario = BD.ObtenerUsuario(u.IdUsuario);
                return View();
            }
            else
            {
                return View("LogInError");
            }
            
            
        }

        public ActionResult Tendencia()
        {
            if (Session["id"] != null)
            {
                List<Cancion> Lista = BD.TOP50();
                ViewBag.ListaCanciones = Lista;
                return View();
            }
            else
            {
                return View("LogInError");
            }
        }
        [HttpPost]
        public ActionResult Tendencia(string busqueda)
        {
            List<Cancion> c = new List<Cancion>();
            c = BD.Filtrar(busqueda);
            ViewBag.ListaCanciones = c;
            return View();
        }
        public ActionResult Registrarse()
        {
            return View();
        }
   
        public ActionResult CerrarSesion()
        {
            Session["id"] = null;
            if(Session["id"] == null)
            {
                return View("LogIn");
            } else
            {
                return View();
            }
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult SubirMusica()
        {
            return View();
        }
            public ActionResult EliminarUsuario(int id,Usuario c)
        {
            c.IdUsuario = Convert.ToInt32(Session["id"].ToString());
            c = BD.ObtenerUsuario(c.IdUsuario);
            BD.EliminarUsuario(id,c);
            return RedirectToAction("CerrarSesion");
        }
        public ActionResult EditarPerfil()
        {
            return View();
        }
        public ActionResult EliminarCancion(int id)
        {
            BD.EliminarCancion(id);
            return RedirectToAction("Perfil");
        }
        [HttpPost]
        public ActionResult SubirMusica(Cancion n, HttpPostedFileBase Audio,Usuario c)
        {
               if (Audio != null && Audio.ContentLength > 0)
                {
                c.IdUsuario = Convert.ToInt32(Session["id"].ToString());
                c = BD.ObtenerUsuario(c.IdUsuario);
                    string newUbication = Server.MapPath("~/Musica/") + Audio.FileName;
                    Audio.SaveAs(newUbication);
                    n.Ubicacion = Audio.FileName;
                    n.Artista = c.NombreUsuario;
                    BD.IngresarMusica(n);
                }
                return RedirectToAction("Index");
        }
        

        [HttpPost]
        public ActionResult Registrarse(Usuario u)
        {
            ViewBag.Reg=BD.Registrarse(u);
            if (ViewBag.Reg == null)
            {
                return View("RegistrarError");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        [HttpPost]
            public ActionResult Index(string busqueda)
            {
            List<Cancion> c=new List<Cancion>();
            c = BD.Filtrar(busqueda);
            ViewBag.ListaCanciones = c;
            return View();
            }
    [HttpPost]
        public ActionResult EditarPerfil(Usuario C)
        {
            C.IdUsuario= Convert.ToInt32(Session["id"].ToString());
            BD.EditarPerfil(C);
            return RedirectToAction("Perfil");
        }
        [HttpPost]
        public ActionResult LogIn(Usuario c)
        {  
            ViewBag.LogIn = BD.LogIn(c);
            if (ViewBag.LogIn != null)
            {
                Session["id"] =ViewBag.LogIn.IdUsuario;
                c.IdUsuario=ViewBag.LogIn.IdUsuario ;

                return RedirectToAction("Perfil");
            }
            else
            {
                return View("LogInError");
            }
            
        }
        public ActionResult Favoritos(Usuario u)
        {
            u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
            u = BD.ObtenerUsuario(u.IdUsuario);
            List<Cancion> Lista = BD.TraerFav(u);
            ViewBag.ListaCanciones = Lista;
            return View();
        }
        public ActionResult AgregarFavoritos(Cancion c,Usuario u)
        {
            u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
            u = BD.ObtenerUsuario(u.IdUsuario);
            BD.Favorito(c,u);
           
                return RedirectToAction("Favoritos");
           
        }
        public ActionResult EliminarFavoritos(Cancion c, Usuario u)
        {
            u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
            u = BD.ObtenerUsuario(u.IdUsuario);
            BD.EliminarFavorito(c, u);

            return RedirectToAction("Favoritos");
        }

        public ActionResult MICUENTA(Usuario u)
        {
            if (Session["id"] != null)
            {
                u.IdUsuario = Convert.ToInt32(Session["id"].ToString());
                ViewBag.Usuario = BD.ObtenerUsuario(u.IdUsuario);
                return View();
            }
            else
            {
                return View("LogInError");
            }
        }
    }
}