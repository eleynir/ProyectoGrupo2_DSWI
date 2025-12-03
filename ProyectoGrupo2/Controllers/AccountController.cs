using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Models;
using ProyectoGrupo2.Data;
using System.Linq;

namespace ProyectoGrupo2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UsuarioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool existeCorreo = _context.Usuarios.Any(u => u.Correo == model.Correo);
            if (existeCorreo)
            {
                ModelState.AddModelError("Correo", "Este correo ya está registrado.");
                return View(model);
            }

            _context.Usuarios.Add(model);
            _context.SaveChanges();

            TempData["Message"] = $"Usuario {model.Nombre} registrado correctamente.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UsuarioModel model)
        {
            ModelState.Remove("Nombre");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == model.Correo
                                   && u.Clave == model.Clave
                                   && u.Estado == "A");

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(model);
            }

            TempData["Message"] = "Inicio de sesión correcto.";
            TempData["NombreUsuario"] = usuario.Nombre;

            return RedirectToAction("Index", "Home");
        }
    }
}