using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using AutoVentas.Models;

namespace AutoVentas.Controllers
{
    public class VentaController : Controller
    {
        private DB_Carros db = new DB_Carros();

        // GET: Venta
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
            var ventas = db.VentasRealizadas.ToList();
            DateTime fechaI = new DateTime();
            DateTime fechaF = new DateTime();
            if (fechaFinal != null && fechaFinal != null)
            {
                if (fechaFinal != "" && fechaFinal != "")
                {
                    fechaI = Convert.ToDateTime(fechaInicio + " 01:00:00");
                    fechaF = Convert.ToDateTime(fechaFinal + " 23:59:59");
                    ventas = db.VentasRealizadas.Where(v => v.FechaVenta >= fechaI && v.FechaVenta <= fechaF).ToList();
                    ViewBag.fechaInicio = fechaI.ToString();
                    ViewBag.fechaFinal = fechaF.ToString();
                }
            }
            return View(ventas);
        }

        public ActionResult GenerarReporte(String fechaInicio, String fechaFinal) 
        {
            var listado = db.VentasRealizadas.ToList();
            DateTime fi = new DateTime();
            DateTime ff = new DateTime();
            if (fechaFinal != null && fechaFinal != null)
            {
                if (fechaFinal != "" && fechaFinal != "")
                {
                    fi = Convert.ToDateTime(fechaInicio);
                    ff = Convert.ToDateTime(fechaFinal);
                    listado = db.VentasRealizadas.Where(v => v.FechaVenta >= fi && v.FechaVenta <= ff).ToList();
                }
            }
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
                title = new Paragraph("Reporte de Ventas " + fecha, titleFont);
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
                tabla.AddCell(clMarca);
                tabla.AddCell(clModelo);
                tabla.AddCell(clCosto);
                tabla.AddCell(clFecha);

                foreach (var item in listado) 
                {
                    colVendedor = new PdfPCell(new Phrase(item.Usuario.Nombre, _standardFont));
                    colDireccion = new PdfPCell(new Phrase(item.Usuario.Direccion, _standardFont));
                    clTel = new PdfPCell(new Phrase(item.Usuario.Telegono.ToString(), _standardFont));
                    clMarca = new PdfPCell(new Phrase(item.Vehiculo.Marca.Nombre, _standardFont));
                    clModelo = new PdfPCell(new Phrase(item.Vehiculo.Modelo.ToString(), _standardFont));
                    clCosto = new PdfPCell(new Phrase("Q." + item.Vehiculo.Precio, _standardFont));
                    clFecha = new PdfPCell(new Phrase(Convert.ToString(item.FechaVenta), _standardFont));
                    tabla.AddCell(colVendedor);
                    tabla.AddCell(colDireccion);
                    tabla.AddCell(clTel);
                    tabla.AddCell(clMarca);
                    tabla.AddCell(clModelo);
                    tabla.AddCell(clCosto);
                    tabla.AddCell(clFecha);
                }              
                doc.Add(tabla);

                doc.Close();
                writer.Close();

                Response.Redirect(path);
            }catch(IOException e)
            {
                Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }

            return RedirectToAction("Index","Venta");
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

            nombre = "reporte_ventas_" + fecha + ".pdf";
            return nombre;
        }
    }
}