using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using AutoVentas.Models;

namespace AutoVentas.Controllers
{
    public class ReservacionController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Reservacion
        public ActionResult Index(String fechaInicio, String fechaFinal)
        {
            if (Session["IDUsuario"] == null)
            {
                return RedirectToAction("Iniciar", "Cuenta");
            }
            else if (!Session["IDRol"].Equals(1))
            {
                return RedirectToAction("Inicio", "Cliente");
            }
            var reservacion = db.Reservacion.Include(r => r.FormaDePago).Include(r => r.Usuario).Include(r => r.Vehiculo).ToList();
            DateTime fechaI = new DateTime();
            DateTime fechaF = new DateTime();
            if (fechaFinal != null && fechaFinal != null)
            {
                if (fechaFinal != "" && fechaFinal != "")
                {
                    fechaI = Convert.ToDateTime(fechaInicio + " 01:00:00");
                    fechaF = Convert.ToDateTime(fechaFinal + " 23:59:59");
                    reservacion = db.Reservacion.Where(r => r.FechaReserva >= fechaI && r.FechaReserva <= fechaF).ToList();
                    ViewBag.fechaInicio = fechaI.ToString();
                    ViewBag.fechaFinal = fechaF.ToString();
                }
            }
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

        // GET: Reservacion/Delete/5
        public ActionResult Vender(int? id)
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
        [HttpPost, ActionName("Vender")]
        [ValidateAntiForgeryToken]
        public ActionResult VentaConfirmada(int id, String nit)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
            VentasRealizadas ventaRealizada = new VentasRealizadas();
            ventaRealizada.IDVehiculo = reservacion.IDVehiculo;
            ventaRealizada.IDUsuario = reservacion.IDUsuario;
            ventaRealizada.IDFormaDePago = reservacion.IDFormaDePago;
            ventaRealizada.FechaVenta = DateTime.Now;
            ventaRealizada.nit = nit;
            ventaRealizada.Usuario = db.Usuario.FirstOrDefault(u => u.IDUsuario == reservacion.IDUsuario);
            ventaRealizada.Vehiculo = db.Vehiculo.FirstOrDefault(u => u.IDVehiculo == reservacion.IDVehiculo);
            db.VentasRealizadas.Add(ventaRealizada);
            Vehiculo vendido = db.Vehiculo.Find(reservacion.IDVehiculo);
            vendido.Estado = "Vendido";
            db.Entry(vendido).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Reservacion");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GenerarReporte(int id)
        {
            Reservacion compra = db.Reservacion.Include(v => v.Usuario).Include(v => v.Vehiculo).FirstOrDefault(v => v.IDReservacion == id);
            String fecha = DateTime.Now.ToString();
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + NombreArchivo();
            Document doc = new Document(PageSize.LETTER);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.AddCreator("Auto Ventas Car´s");
                doc.Open();

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                Paragraph title;
                title = new Paragraph("Factura de compra " + fecha, titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                doc.Add(Chunk.NEWLINE);

                PdfPTable tabla = new PdfPTable(7);
                tabla.WidthPercentage = 100;

                PdfPCell colVendedor = new PdfPCell(new Phrase("Nombre del Comprador", _titleFont));
                colVendedor.BorderWidthBottom = 0.25f;
                PdfPCell colDireccion = new PdfPCell(new Phrase("Direccion del Comprador", _titleFont));
                colDireccion.BorderWidthBottom = 0.25f;
                PdfPCell clTel = new PdfPCell(new Phrase("Telefono del Comprador", _titleFont));
                clTel.BorderWidthBottom = 0.25f;
                PdfPCell clNomVehiculo = new PdfPCell(new Phrase("Nombre del Vehiculo", _titleFont));
                clNomVehiculo.BorderWidthBottom = 0.25f;
                PdfPCell clMarca = new PdfPCell(new Phrase("Marca Vehiculo", _titleFont));
                clMarca.BorderWidthBottom = 0.25f;
                PdfPCell clModelo = new PdfPCell(new Phrase("Modelo Vehiculo", _titleFont));
                clModelo.BorderWidthBottom = 0.25f;
                PdfPCell clCosto = new PdfPCell(new Phrase("Costo Vehiculo", _titleFont));
                clCosto.BorderWidthBottom = 0.25f;
                PdfPCell clFecha = new PdfPCell(new Phrase("Fecha Compra", _titleFont));
                clFecha.BorderWidthBottom = 0.25f;

                tabla.AddCell(colVendedor);
                tabla.AddCell(colDireccion);
                tabla.AddCell(clTel);
                tabla.AddCell(clNomVehiculo);
                tabla.AddCell(clMarca);
                tabla.AddCell(clModelo);
                tabla.AddCell(clCosto);
                tabla.AddCell(clFecha);

                colVendedor = new PdfPCell(new Phrase(compra.Usuario.Nombre, _standardFont));
                colDireccion = new PdfPCell(new Phrase(compra.Usuario.Direccion, _standardFont));
                clTel = new PdfPCell(new Phrase(compra.Usuario.Telegono.ToString(), _standardFont));
                clMarca = new PdfPCell(new Phrase(compra.Vehiculo.Marca.Nombre, _standardFont));
                clModelo = new PdfPCell(new Phrase(compra.Vehiculo.Modelo.ToString(), _standardFont));
                clCosto = new PdfPCell(new Phrase("Q." + compra.Vehiculo.Precio, _standardFont));
                clFecha = new PdfPCell(new Phrase(Convert.ToString(compra.FechaReserva), _standardFont));
                tabla.AddCell(colVendedor);
                tabla.AddCell(colDireccion);
                tabla.AddCell(clTel);
                tabla.AddCell(clMarca);
                tabla.AddCell(clModelo);
                tabla.AddCell(clCosto);
                tabla.AddCell(clFecha);

                doc.Add(tabla);

                doc.Close();
                writer.Close();

                Response.Redirect(path);
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }

        public String NombreArchivo()
        {
            String seg = Convert.ToString(DateTime.Now.Second);
            String fecha = Convert.ToString(DateTime.Now);
            fecha = fecha + seg;
            String nombre = "";

            fecha = fecha.Replace('/', '_');
            fecha = fecha.Replace(' ', '_');
            fecha = fecha.Replace(':', '_');
            fecha = fecha.Replace('.', '_');

            nombre = "Factura de compra _" + fecha + ".pdf";
            return nombre;
        }
    }
}
