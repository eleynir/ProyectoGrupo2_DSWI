using Microsoft.AspNetCore.Mvc;

namespace ProyectoGrupo2.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Salas()
        {
            return View();
        }

        public IActionResult Index(string sala)
        {
            ViewBag.Sala = sala;
            return View();
        }
    }
}