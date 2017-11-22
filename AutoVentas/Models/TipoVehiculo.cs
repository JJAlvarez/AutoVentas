using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class TipoVehiculo
    {

        [Key]
        public int IDTipoVehiculo { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre del tipo de vehiculo.")]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

    }
}