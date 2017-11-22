using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoVentas.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Inicio()
        {
            if (Session["IDRol"] != null)
            {
                if (Session["IDRol"].Equals(1))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}