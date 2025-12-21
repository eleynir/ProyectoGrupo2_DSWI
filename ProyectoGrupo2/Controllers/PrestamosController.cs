using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo2.Data;
using ProyectoGrupo2.Models;

namespace ProyectoGrupo2.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var prestamos = _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Libro)
                .ToList();

            return View(prestamos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int IdUsuario, int IdLibro, DateTime FechaRetiro, DateTime FechaDevolucionEstimada)
        {
            var rol = HttpContext.Session.GetInt32("IdRol");
            if (rol != 2 && rol != 3)
                return RedirectToAction("Index", "Home");

            var prestamo = new PrestamoModel
            {
                IdUsuario = IdUsuario,
                FechaRegistro = DateTime.Now,
                FechaRetiro = FechaRetiro,
                FechaDevolucionEstimada = FechaDevolucionEstimada,
                Estado = "Pendiente"
            };

            _context.Prestamos.Add(prestamo);
            _context.SaveChanges(); 

            var detalle = new PrestamoDetalleModel
            {
                IdPrestamo = prestamo.IdPrestamo,
                IdLibro = IdLibro,
                Cantidad = 1
            };

            _context.PrestamoDetalles.Add(detalle);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var rol = HttpContext.Session.GetInt32("IdRol");
            if (rol != 2 && rol != 3)
                return RedirectToAction("Index", "Home");

            ViewBag.Usuarios = _context.Usuarios
                .Where(u => u.Estado == "A" && u.IdRol == 1) 
                .ToList();

            ViewBag.Libros = _context.Libros.ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Devolver(int id)
        {
            var prestamo = _context.Prestamos
                .FirstOrDefault(p => p.IdPrestamo == id);

            if (prestamo == null)
                return NotFound();

            if (prestamo.Estado != "Pendiente")
                return RedirectToAction("Index");

            prestamo.Estado = "Devuelto";
            prestamo.FechaDevolucionReal = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}
