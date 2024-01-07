using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaElectronica.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [StringLength(35, MinimumLength = 3, ErrorMessage = "El Nombre de usuario debe tener entre 3 y 35 caracteres")]
        [Required(ErrorMessage ="Introduce un nombre valido")]
        public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "Introduce un correo electrónico")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Por favor, introduce una dirección de correo electrónico válida.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Por favor, introduce una dirección de correo electrónico válida.")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Introduce una contraseña valida")]
        [StringLength(23, MinimumLength = 3, ErrorMessage = "La contraseña debe tener entre 3 y 23 caracteres")]
        public string? Password { get; set; }
        [Required]
        public int IdTipoUsuario { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }

    }
}
