using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo2.Data;
using ProyectoGrupo2.Models;
using System.Linq;

namespace ProyectoGrupo2.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Libros
        public IActionResult Index()
        {
            var libros = _context.Libros.ToList();
            return View(libros);
        }

        // GET: /Libros/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LibroModel model)
        {
            ModelState.Remove("Estado"); // tiene valor por defecto "A"

            if (model.StockDisponible > model.StockTotal)
            {
                ModelState.AddModelError("StockDisponible",
                    "El stock disponible no puede ser mayor al stock total.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Libros.Add(model);
            _context.SaveChanges();

            TempData["Message"] = "Libro registrado correctamente.";
            return RedirectToAction("Index");
        }

        // GET: /Libros/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var libro = _context.Libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: /Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LibroModel model)
        {
            ModelState.Remove("Estado"); // mantenemos el valor que ya tiene

            if (model.StockDisponible > model.StockTotal)
            {
                ModelState.AddModelError("StockDisponible",
                    "El stock disponible no puede ser mayor al stock total.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Libros.Update(model);
            _context.SaveChanges();

            TempData["Message"] = "Libro actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // GET: /Libros/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var libro = _context.Libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: /Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _context.Libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();

            TempData["Message"] = "Libro eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}
