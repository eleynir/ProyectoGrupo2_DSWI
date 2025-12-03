using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo2.Models
{
    [Table("Libros")]
    public class LibroModel
    {
        [Key]
        [Column("IdLibro")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Titulo")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Autor")]
        public string Autor { get; set; }

        [StringLength(20)]
        [Column("ISBN")]
        public string? ISBN { get; set; }

        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [Column("AnioPublicacion")]
        public int? AnioPublicacion { get; set; }

        [StringLength(150)]
        [Column("Editorial")]
        public string? Editorial { get; set; }

        [Column("StockTotal")]
        public int StockTotal { get; set; }

        [Column("StockDisponible")]
        public int StockDisponible { get; set; }

        [StringLength(1)]
        [Column("Estado")]
        public string Estado { get; set; } = "A"; 
    }
}
