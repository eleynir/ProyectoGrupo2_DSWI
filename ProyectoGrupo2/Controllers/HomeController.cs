using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Models;

namespace ProyectoGrupo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IdRol") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.NombreUsuario = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.IdRol = HttpContext.Session.GetInt32("IdRol");
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
