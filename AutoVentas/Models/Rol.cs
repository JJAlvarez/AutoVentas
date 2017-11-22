using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoVentas.Models
{
    public class Rol
    {
        [Key]
        public int IDRol { get; set; }

        [Required(ErrorMessage ="Es necesario el nombre del rol.")]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

    }
}