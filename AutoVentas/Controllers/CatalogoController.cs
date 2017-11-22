using AutoVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace AutoVentas.Controllers
{
    public class CatalogoController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Catalogo
        public ActionResult Inicio(String filtro)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            if (filtro != null)
            {
                vehiculos = db.Vehiculo.Where(v => v.Estado != "Vendido").ToList();
                vehiculos = vehiculos.Where(v => v.TipoVehiculo.Nombre.Contains(filtro)
                    || v.TipoCombustible.Nombre.Contains(filtro)
                    || v.Modelo.ToString().Contains(filtro)
                    || v.Marca.Nombre.Contains(filtro)).ToList();
            }
            else
            {
                vehiculos = db.Vehiculo.Where(v => v.Estado != "Vendido").ToList();
            }
            if (filtro == "")
            {
                vehiculos = db.Vehiculo.Where(v => v.Estado != "Vendido").ToList();
            }
            return View(vehiculos);
        }

        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculo = db.Vehiculo.FirstOrDefault(v => v.IDVehiculo == id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDFormaPago = new SelectList(db.FormaDePago, "IDFormaPago", "Forma de Pago");
            return View(vehiculo);
        }

        public ActionResult Reservar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = new Reservacion();
            var vehiculo = db.Vehiculo.Find(id);
            var usuario = db.Usuario.Find(Session["IDUsuario"]);
            reservacion.Usuario = usuario;
            reservacion.Vehiculo = vehiculo;
            reservacion.IDUsuario = usuario.IDUsuario;
            reservacion.IDVehiculo = vehiculo.IDVehiculo;
            ViewBag.IDFormaDePago = new SelectList(db.FormaDePago, "IDFormaDePago", "Nombre");
            return View(reservacion);
        }

        [HttpPost]
        public ActionResult Reservar(Reservacion reservacion)
        {
            DateTime fechaActual = DateTime.Now;
            reservacion.FechaReserva = fechaActual;
            db.Reservacion.Add(reservacion);
            Vehiculo reservado = db.Vehiculo.Find(reservacion.IDVehiculo);
            reservado.Estado = "Reservado";
            db.Entry(reservado).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.reservacion = "Su reservacion ha sido realizada exitosamente, lo esperamos en la agencia, gracias.";
            return View(reservacion);
        }
    }
}