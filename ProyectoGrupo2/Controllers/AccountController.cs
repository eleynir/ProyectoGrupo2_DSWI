using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Models;

namespace ProyectoGrupo2.Controllers
{
    public class AccountController : Controller
    {
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

            // Aquí luego iría la lógica para guardar en BD
            TempData["Message"] = $"Usuario {model.Nombre} registrado correctamente.";
            return RedirectToAction("Index", "Home");
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["Message"] = $"Bienvenido, {model.Nombre ?? "usuario"}";
            return RedirectToAction("Index", "Home");
        }
    }
}