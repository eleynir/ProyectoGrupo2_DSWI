using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo2.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        [Column("IdUsuario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        [Column("NombreCompleto")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        [StringLength(100)]
        [Column("Email")]
        public string Correo { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 255 caracteres")]
        [Column("PasswordHash")]
        public string Clave { get; set; }

        [Column("IdRol")]
        public int IdRol { get; set; } = 1;  

        [Column("Estado")]
        [StringLength(1)]
        public string Estado { get; set; } = "A"; 

        [NotMapped]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}