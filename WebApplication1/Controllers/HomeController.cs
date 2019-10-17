using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            ViewBag.Message = "Tu Perfil";

            return View();
        }

        public ActionResult Tendencia()
        {
            ViewBag.Message = "Lo mas escuchado estara aqui";

            return View();
        }
    }
}