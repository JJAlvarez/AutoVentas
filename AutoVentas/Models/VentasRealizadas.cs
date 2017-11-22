using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class VentasRealizadas
    {

        [Key]
        public int IDVenta { get; set; }

        public int IDVehiculo { get; set; }

        public int IDUsuario { get; set; }

        public int IDFormaDePago { get; set; }

        [Display(Name = "Fecha de venta"), Required(ErrorMessage = "Fecha Obligatoria")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaVenta { get; set; }

        public String nit { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }

        public virtual FormaDePago FormaDePago { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}