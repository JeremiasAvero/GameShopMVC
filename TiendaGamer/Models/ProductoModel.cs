using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TiendaElectronica.Models
{
    public class ProductoModel
    {
   
        public int Id { get; set; }
        [StringLength(7, MinimumLength = 2, ErrorMessage = "El Codigo debe tener entre 3 y 35 caracteres")]
        [Required(ErrorMessage = "Introduce un codigo")]
        public string Codigo { get; set; }
        [StringLength(45, MinimumLength = 2, ErrorMessage = "El Nombre de usuario debe tener entre 3 y 45 caracteres")]
        [Required(ErrorMessage = "Introduce un nombre")]
        public string? Nombre { get; set; }
        [StringLength(4000, MinimumLength = 2, ErrorMessage = "La descripción debe tener entre 3 y 100 caracteres")]
        [Required(ErrorMessage = "Introduce una descripción")]
        public string? Descripcion { get; set; }
        public MarcaModel Marca { get; set; }
        public CategoriaModel Categoria { get; set; }
        //[Range(1.00, 10000000000.00, ErrorMessage = "maximo de 10000000000.0")]
        [Required(ErrorMessage = "Introduce un precio")]
        public decimal Precio { get; set; }
        [Range(1, 1000, ErrorMessage = "maximo de 1000")]
        [Required(ErrorMessage = "Introduce una cantidad")]
        public int Stock { get; set; }
        public string? UrlImagen { get; set; }

        
    }
}
