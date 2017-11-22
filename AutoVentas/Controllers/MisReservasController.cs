using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVentas.Models;

namespace AutoVentas.Controllers
{
    public class MisReservasController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Reservacion
        public ActionResult Index()
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Index", "Home");
            }
            int idUsuario = Convert.ToInt32(Session["IDUsuario"]);
            var reservacion = db.Reservacion.Where(r=> r.IDUsuario == idUsuario).Include(r => r.FormaDePago).Include(r => r.Usuario).Include(r => r.Vehiculo);
            return View(reservacion.ToList());
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.IDFormaDePago = new SelectList(db.FormaDePago, "IDFormaDePago", "Nombre");
            ViewBag.IDUsuario = new SelectList(db.Usuario, "IDUsuario", "Nombre");
            ViewBag.IDVehiculo = new SelectList(db.Vehiculo, "IDVehiculo", "Nombre");
            return View();
        }

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}