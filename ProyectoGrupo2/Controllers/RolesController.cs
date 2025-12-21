using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Data;
using ProyectoGrupo2.Models;

namespace ProyectoGrupo2.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RolModel rol)
        {
            if (!ModelState.IsValid)
                return View(rol);

            _context.Roles.Add(rol);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null)
                return NotFound();

            return View(rol);
        }

        [HttpPost]
        public IActionResult Edit(RolModel rol)
        {
            if (!ModelState.IsValid)
                return View(rol);

            _context.Roles.Update(rol);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
