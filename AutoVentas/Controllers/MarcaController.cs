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
    public class MarcaController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Marca
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
            List<Marca> marcas = new List<Marca>();
            if (filtro != null)
            {
                marcas = db.Marca.Where(v => v.Nombre.Contains(filtro)).ToList();
            }
            else
            {
                marcas = db.Marca.ToList();
            }
            if (filtro == "")
            {
                marcas = db.Marca.ToList();
            }
            return View(marcas.ToList());
        }

        // GET: Marca/Details/5
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
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: Marca/Create
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

        // POST: Marca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMarca,Nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Marca.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }

        // GET: Marca/Edit/5
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
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMarca,Nombre")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        // GET: Marca/Delete/5
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
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marca marca = db.Marca.Find(id);
            db.Marca.Remove(marca);
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
