using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoVentas.Models;

namespace AutoVentas.Controllers
{
    public class ArchivoController : Controller
    {
        DB_Carros db = new DB_Carros();
        // GET: Archivo
        public ActionResult ObtenerArchivo(int id)
        {
            var imagen = db.Archivo.Find(id);
            return File(imagen.Contenido, imagen.ContentType);
        }
    }
}