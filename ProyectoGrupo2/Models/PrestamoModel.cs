using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo2.Models
{
    [Table("Prestamos")]
    public class PrestamoModel
    {
        [Key]
        [Column("IdPrestamo")]
        public int IdPrestamo { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public UsuarioModel Usuario { get; set; }

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [Column("FechaRetiro")]
        public DateTime FechaRetiro { get; set; }

        [Column("FechaDevolucionEstimada")]
        public DateTime FechaDevolucionEstimada { get; set; }

        [Column("FechaDevolucionReal")]
        public DateTime? FechaDevolucionReal { get; set; }

        [Column("Estado")]
        public string Estado { get; set; }

        [Column("MontoMulta")]
        public decimal? MontoMulta { get; set; }

        public ICollection<PrestamoDetalleModel> Detalles { get; set; }
    }
}