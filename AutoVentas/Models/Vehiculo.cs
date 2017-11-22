using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class Vehiculo
    {

        [Key]
        public int IDVehiculo { get; set; }

        [Display(Name = "Nombre")]
        public String Nombre { get; set; }
        
        public int IDMarca { get; set; }

        public virtual Marca Marca { get; set; }

        [Display(Name = "Modelo"), Required(ErrorMessage = "El modelo es requerido.")]
        public int Modelo { get; set; }

        [Display(Name = "Precio"), Required(ErrorMessage = "El precio es requerido.")]
        public double Precio { get;  set; }
        
        public String Estado { get; set; }

        public int IDTipoCombustible { get; set; }

        public virtual TipoCombustible TipoCombustible { get; set; }

        public int IDTipoVehiculo { get; set; }

        public virtual TipoVehiculo TipoVehiculo { get; set; }

        [Display(Name = "Descripcion"), Required(ErrorMessage = "Se necesita una descripcion del vehiculo.")]
        public String Descripcion { get; set; }

        public virtual List<Archivo> Archivos { get; set; }
    }
}