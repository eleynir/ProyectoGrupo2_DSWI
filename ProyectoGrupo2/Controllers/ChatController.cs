using Microsoft.AspNetCore.Mvc;

namespace ProyectoGrupo2.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Salas()
        {
            var rol = HttpContext.Session.GetInt32("IdRol");

            if (rol == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Index(string sala)
        {
            var rol = HttpContext.Session.GetInt32("IdRol");

            if (rol == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Sala = sala;
            return View();
        }
    }
}