using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class TipoCombustible
    {
        [Key]
        public int IDTipoCombustible { get; set; }

        [Display(Name = "Nombre"), Required(ErrorMessage = "El nombre es requerido.")]
        public String Nombre { get; set; }
    }
}