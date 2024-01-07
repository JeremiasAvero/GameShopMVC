using System.ComponentModel.DataAnnotations;

namespace TiendaElectronica.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "de 3 a 30 caracteres")]
        [Required(ErrorMessage = "Introduce un nombre de categoria")]

        public string? Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}