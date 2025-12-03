
using global::ProyectoGrupo2.Models;
using Microsoft.EntityFrameworkCore;


namespace ProyectoGrupo2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<LibroModel> Libros { get; set; }
    }
}