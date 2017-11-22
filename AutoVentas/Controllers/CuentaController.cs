using AutoVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoVentas.Controllers
{
    public class CuentaController : Controller
    {
        public DB_Carros db = new DB_Carros();

        // GET: Cuenta
        public ActionResult Iniciar()
        {
            if (Session["IDRol"] != null)
            {
                if (Session["IDRol"].Equals(1))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (Session["IDRol"].Equals(2))
                {
                    return RedirectToAction("Inicio", "Cliente");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Usuario usuario)
        {
                var user = db.Usuario.FirstOrDefault( u => u.Username == usuario.Username && u.Password == usuario.Password);
                if(user != null)
                {
                    Session["Username"] = user.Username;
                    Session["Nombre"] = user.Nombre;
                    Session["IDUsuario"] = user.IDUsuario;
                    Session["IDRol"] = user.IDRol;
                    if (user.IDRol == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Inicio", "Cliente");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Verifique sus credenciales: Nombre de Usuario o contraseña incorrectos.");
                }
            return View();
        }

        public ActionResult Registrarse()
        {
            if (Session["IDUsuario"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                Rol rol = db.Rol.FirstOrDefault(r => r.IDRol == 2);
                usuario.Rol = rol;
                db.Usuario.Add(usuario);
                db.SaveChanges();
                ViewBag.mensaje = "El usuario " + usuario.Username + " fue registrado correctamente";
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("IDUsuario");
            Session.Remove("Nombre");
            Session.Remove("Username");
            Session.Remove("IDRol");
            return RedirectToAction("Iniciar");
        }
    }
}