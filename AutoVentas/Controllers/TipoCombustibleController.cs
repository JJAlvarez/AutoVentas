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
    public class TipoCombustibleController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: TipoCombustible
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
            List<TipoCombustible> tipos = new List<TipoCombustible>();
            if (filtro != null)
            {
                tipos = db.TipoCombustible.Where(v => v.Nombre.Contains(filtro)).ToList();
            }
            else
            {
                tipos = db.TipoCombustible.ToList();
            }
            if (filtro == "")
            {
                tipos = db.TipoCombustible.ToList();
            }
            return View(tipos.ToList());
        }

        // GET: TipoCombustible/Details/5
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
            TipoCombustible tipoCombustible = db.TipoCombustible.Find(id);
            if (tipoCombustible == null)
            {
                return HttpNotFound();
            }
            return View(tipoCombustible);
        }

        // GET: TipoCombustible/Create
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

        // POST: TipoCombustible/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoCombustible,Nombre")] TipoCombustible tipoCombustible)
        {
            if (ModelState.IsValid)
            {
                db.TipoCombustible.Add(tipoCombustible);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCombustible);
        }

        // GET: TipoCombustible/Edit/5
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
            TipoCombustible tipoCombustible = db.TipoCombustible.Find(id);
            if (tipoCombustible == null)
            {
                return HttpNotFound();
            }
            return View(tipoCombustible);
        }

        // POST: TipoCombustible/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTipoCombustible,Nombre")] TipoCombustible tipoCombustible)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCombustible).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoCombustible);
        }

        // GET: TipoCombustible/Delete/5
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
            TipoCombustible tipoCombustible = db.TipoCombustible.Find(id);
            if (tipoCombustible == null)
            {
                return HttpNotFound();
            }
            return View(tipoCombustible);
        }

        // POST: TipoCombustible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCombustible tipoCombustible = db.TipoCombustible.Find(id);
            db.TipoCombustible.Remove(tipoCombustible);
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
