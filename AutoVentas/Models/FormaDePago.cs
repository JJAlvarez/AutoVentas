using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class FormaDePago
    {
         [Key]
         public int IDFormaDePago { get; set; }

        [Required(ErrorMessage ="Nombre de la forma de pago requerido.")]
        public String Nombre { get; set; }
    }
}