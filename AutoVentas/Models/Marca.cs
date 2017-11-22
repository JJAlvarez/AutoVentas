using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class Marca
    {

        [Key]
        public int IDMarca { get; set; }

        [Display(Name ="Nombre de la Marca"), Required(ErrorMessage ="El nombre de la marca es requerido.")]
        public String Nombre { get; set; }
    }
}