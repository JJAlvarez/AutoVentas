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
    public class TipoVehiculoController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: TipoVehiculo
        public ActionResult Index(String filtro)
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            List<TipoVehiculo> tipos = new List<TipoVehiculo>();
            if (filtro != null)
            {
                tipos = db.TipoVehiculo.Where(v => v.Nombre.Contains(filtro)).ToList();
            }
            else
            {
                tipos = db.TipoVehiculo.ToList();
            }
            if (filtro == "")
            {
                tipos = db.TipoVehiculo.ToList();
            }
            return View(tipos.ToList());
        }

        // GET: TipoVehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Create
        public ActionResult Create()
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

        // POST: TipoVehiculo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoVehiculo,Nombre")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.TipoVehiculo.Add(tipoVehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTipoVehiculo,Nombre")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoVehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            if (tipoVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoVehiculo tipoVehiculo = db.TipoVehiculo.Find(id);
            db.TipoVehiculo.Remove(tipoVehiculo);
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
