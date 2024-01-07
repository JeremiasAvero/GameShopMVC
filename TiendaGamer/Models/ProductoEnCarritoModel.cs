using TiendaElectronica.Models;

namespace TiendaGamer.Models
{
    public class ProductoEnCarritoModel
    {
        public ProductoModel Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
    }
}
