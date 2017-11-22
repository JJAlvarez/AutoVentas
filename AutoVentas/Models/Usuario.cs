using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoVentas.Models
{
    public class Usuario
    {

        [Key]
        public int IDUsuario { get; set; }

        [Display(Name ="Nombre"), Required(ErrorMessage ="El nombre es requerido.")]
        public String Nombre { get; set; }

        [Display(Name = "Apellido"), Required(ErrorMessage = "El apellido es requerido.")]
        public String Apellido { get; set; }

        [Display(Name = "Telfono"), Required(ErrorMessage = "El telefono es requerido.")]
        public long Telegono { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "El correo es requerido."), DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Display(Name = "Direccion"), Required(ErrorMessage = "El direccion es requerido.")]
        public String Direccion { get; set; }

        [Display(Name = "Nombre de Usuario"), Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public String Username { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "La contrasenia es requerido."), DataType(DataType.Password)]
        public String Password { get; set; }
        
        public int IDRol { get; set; }

        public virtual Rol Rol { get; set; }
    }
}