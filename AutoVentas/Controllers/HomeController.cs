using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoVentas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            return View();
        }
    }
}