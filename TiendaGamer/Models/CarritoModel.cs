namespace TiendaElectronica.Models
{
    public class CarritoModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        public int IdProducto { get; set; }

        public decimal MontoTotal { get; set; }
    }
}
