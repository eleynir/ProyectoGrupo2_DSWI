using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo2.Models
{
    [Table("Roles")]
    public class RolModel
    {
        [Key]
        public int IdRol { get; set; }

        public string NombreRol { get; set; }
    }
}