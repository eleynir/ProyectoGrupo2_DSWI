using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo2.Models
{
    [Table("PrestamoDetalle")]
    public class PrestamoDetalleModel
    {
        [Key]
        [Column("IdPrestamoDetalle")]
        public int IdPrestamoDetalle { get; set; }

        [Column("IdPrestamo")]
        public int IdPrestamo { get; set; }

        [ForeignKey("IdPrestamo")]
        public PrestamoModel Prestamo { get; set; }

        [Column("IdLibro")]
        public int IdLibro { get; set; }

        [ForeignKey("IdLibro")]
        public LibroModel Libro { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }
    }
}
