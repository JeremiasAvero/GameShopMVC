using TiendaElectronica.Models;

namespace TiendaGamer.Models
{
    public class PcArmadaModel
    {
        public int IdPc { get; set; }
        public string Nombre { get; set; }
        public int IdProcesador { get; set; }
        public int IdCooler { get; set; }
        public int IdMemoriaRam { get; set; }
        public int IdPlacaMadre { get; set; }
        public int IdGabinete { get; set; }
        public int IdFuente { get; set; }
        public int IdDiscoPrincipal { get; set; }
        public int IdDiscoSecundario { get; set; }
        public int IdPlacaDeVideo { get; set; }
        public decimal Precio { get; set; }
    }
}
