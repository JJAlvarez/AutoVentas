using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoVentas.Models
{
    public class Archivo
    {
        [Key]
        public int IDArchivo { get; set; }
        public String Nombre { get; set; }
        public String ContentType { get; set; }
        public byte[] Contenido { get; set; }
        public int IDVehiculo { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}