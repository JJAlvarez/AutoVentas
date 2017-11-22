using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class ComprasRealizadas
    {

        [Key]
        public int IDCompra { get; set; }

        public int IDVehiculo { get; set; }

        [Display(Name = "Fecha de compra"), Required(ErrorMessage = "Fecha Obligatoria")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }
    }
}