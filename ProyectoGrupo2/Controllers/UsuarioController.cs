using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Data;
using ProyectoGrupo2.Models;
using System.Linq;

namespace ProyectoGrupo2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsuarioModel model)
        {
            ModelState.Remove("Nombre");
            ModelState.Remove("Correo");
            ModelState.Remove("Clave"); 
            ModelState.Remove("FechaRegistro");

            if (ModelState.IsValid)
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(u => u.Id == model.Id);

                if (usuarioDb != null)
                {
                    usuarioDb.IdRol = model.IdRol;
                    usuarioDb.Estado = model.Estado;

                    _context.Update(usuarioDb);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            ViewBag.Roles = _context.Roles.ToList();
            return View(model);
        }

        public IActionResult CambiarEstado(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            usuario.Estado = usuario.Estado == "A" ? "I" : "A";

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}