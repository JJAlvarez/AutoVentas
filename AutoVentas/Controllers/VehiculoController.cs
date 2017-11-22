using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVentas.Models;
using System.Data.Entity.Infrastructure;

namespace AutoVentas.Controllers
{
    public class VehiculoController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Vehiculo
        public ActionResult Index(String filtro)
        {
            if (Session["IDUsuario"] == null || !Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            var vehiculo = db.Vehiculo.Include(v => v.Marca).Include(v => v.TipoCombustible).Include(v => v.TipoVehiculo);
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            if (filtro != null)
            {
                vehiculos = db.Vehiculo.Where(v => v.TipoVehiculo.Nombre.Contains(filtro)
                    || v.TipoCombustible.Nombre.Contains(filtro)
                    || v.Modelo.ToString().Contains(filtro)
                    || v.Marca.Nombre.Contains(filtro)).ToList();
            }else
            {
                vehiculos = db.Vehiculo.ToList();
            }
            if (filtro == "")
            {
                vehiculos = db.Vehiculo.ToList();
            }
            return View(vehiculos.ToList());
        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["IDUsuario"] == null || !Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            if (Session["IDUsuario"] == null || !Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            ViewBag.IDMarca = new SelectList(db.Marca, "IDMarca", "Nombre");
            ViewBag.IDTipoCombustible = new SelectList(db.TipoCombustible, "IDTipoCombustible", "Nombre");
            ViewBag.IDTipoVehiculo = new SelectList(db.TipoVehiculo, "IDTipoVehiculo", "Nombre");
            return View();
        }

        // POST: Vehiculo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                if (archivo != null && archivo.ContentLength > 0)
                {
                    var image = new Archivo {
                            Nombre = System.IO.Path.GetFileName(archivo.FileName),
                            ContentType = archivo.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(archivo.InputStream))
                    {
                        image.Contenido = reader.ReadBytes(archivo.ContentLength);
                    };
                    vehiculo.Estado = "Disponible";
                    vehiculo.Archivos = new List<Archivo> { image };
                }
                db.Vehiculo.Add(vehiculo);
                ComprasRealizadas compra = new ComprasRealizadas();
                compra.FechaCompra = DateTime.Now;
                compra.Vehiculo = vehiculo;
                db.ComprasRealizadas.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDMarca = new SelectList(db.Marca, "IDMarca", "Nombre", vehiculo.IDMarca);
            ViewBag.IDTipoCombustible = new SelectList(db.TipoCombustible, "IDTipoCombustible", "Nombre", vehiculo.IDTipoCombustible);
            ViewBag.IDTipoVehiculo = new SelectList(db.TipoVehiculo, "IDTipoVehiculo", "Nombre", vehiculo.IDTipoVehiculo);
            return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["IDUsuario"] == null || !Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMarca = new SelectList(db.Marca, "IDMarca", "Nombre", vehiculo.IDMarca);
            ViewBag.IDTipoCombustible = new SelectList(db.TipoCombustible, "IDTipoCombustible", "Nombre", vehiculo.IDTipoCombustible);
            ViewBag.IDTipoVehiculo = new SelectList(db.TipoVehiculo, "IDTipoVehiculo", "Nombre", vehiculo.IDTipoVehiculo);
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase archivo)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculo = db.Vehiculo.Find(id);
            if (TryUpdateModel(vehiculo, "", new String[] { "IDVehiculo, Nombre, IDMarca, Modelo, Precio, Estado, IDTipoCombustible, IDTipoVehiculo"}))
            {
                try
                {
                    if(archivo != null && archivo.ContentLength > 0)
                    {
                        db.Archivo.Remove(vehiculo.Archivos.First());
                        var imagen = new Archivo
                        {
                            Nombre = System.IO.Path.GetFileName(archivo.FileName),
                            ContentType = archivo.ContentType
                        };
                        using(var reader = new System.IO.BinaryReader(archivo.InputStream))
                        {
                            imagen.Contenido = reader.ReadBytes(archivo.ContentLength);
                        }
                        vehiculo.Archivos = new List<Archivo> { imagen };
                    }
                    db.Entry(vehiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(RetryLimitExceededException /* e */)
                {
                    ModelState.AddModelError("", "Error al actualizar los datos, por favor intentalo de nuevo.");
                }
            }
            ViewBag.IDMarca = new SelectList(db.Marca, "IDMarca", "Nombre", vehiculo.IDMarca);
            ViewBag.IDTipoCombustible = new SelectList(db.TipoCombustible, "IDTipoCombustible", "Nombre", vehiculo.IDTipoCombustible);
            ViewBag.IDTipoVehiculo = new SelectList(db.TipoVehiculo, "IDTipoVehiculo", "Nombre", vehiculo.IDTipoVehiculo);
            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["IDUsuario"] == null || !Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            db.Vehiculo.Remove(vehiculo);
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
