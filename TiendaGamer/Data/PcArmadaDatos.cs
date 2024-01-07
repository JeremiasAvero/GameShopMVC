using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Models;

namespace TiendaGamer.Data
{
    public class PcArmadaDatos
    {
        public List<PcArmadaModel> Listar()
        {
            ProductoDatos datosProductos = new ProductoDatos();

            DatosConexion datos = new DatosConexion();
            List<PcArmadaModel> lista = new List<PcArmadaModel>();
            try
            {
                datos.Procedimiento("ListarPcArmadas");
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    PcArmadaModel pc = new PcArmadaModel();
                    pc.IdPc = (int)datos.Lector["IdPc"];
                    pc.Nombre = (string)datos.Lector["Nombre"];
                    pc.IdProcesador = (int)datos.Lector["IdProcesador"];
                    pc.IdCooler = (int)datos.Lector["IdCooler"];
                    pc.IdPlacaMadre = (int)datos.Lector["IdPlacaMadre"];
                    pc.IdMemoriaRam = (int)datos.Lector["IdMemoriaRam"];
                    pc.IdGabinete = (int)datos.Lector["IdGabinete"];
                    pc.IdFuente = (int)datos.Lector["IdFuente"];
                    pc.IdDiscoPrincipal = (int)datos.Lector["IdDisco"];
                    pc.IdDiscoSecundario = (int)datos.Lector["IdDisco2"];
                    pc.IdPlacaDeVideo = (int)datos.Lector["IdPlacaVideo"];
                    pc.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(pc);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public PcArmadaModel Obtener(int id)
        {
            DatosConexion datos = new DatosConexion();
            PcArmadaModel pc = new PcArmadaModel();
            try
            {
                datos.Procedimiento("ObtenerPcArmada");
                datos.Parametro("IdPc", id);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    pc.IdPc = (int)datos.Lector["IdPc"];
                    pc.Nombre = (string)datos.Lector["Nombre"];
                    pc.IdProcesador = (int)datos.Lector["IdProcesador"];
                    pc.IdCooler = (int)datos.Lector["IdCooler"];
                    pc.IdPlacaMadre = (int)datos.Lector["IdPlacaMadre"];
                    pc.IdMemoriaRam = (int)datos.Lector["IdMemoriaRam"];
                    pc.IdGabinete = (int)datos.Lector["IdGabinete"];
                    pc.IdFuente = (int)datos.Lector["IdFuente"];
                    pc.IdDiscoPrincipal = (int)datos.Lector["IdDisco"];
                    pc.IdDiscoSecundario = (int)datos.Lector["IdDisco2"];
                    pc.IdPlacaDeVideo = (int)datos.Lector["IdPlacaVideo"];
                    pc.Precio = (decimal)datos.Lector["Precio"];

                }
                return pc;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public bool Registrar(PcArmadaModel pc)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();

            try
            {
                datos.Procedimiento("RegistrarPcArmada");
                datos.Parametro("Nombre", pc.Nombre);
                datos.Parametro("IdProcesador", pc.IdProcesador);
                datos.Parametro("IdCooler", pc.IdCooler);
                datos.Parametro("IdPlacaMadre", pc.IdPlacaMadre);
                datos.Parametro("IdMemoriaRam", pc.IdMemoriaRam);
                datos.Parametro("IdGabinete", pc.IdGabinete);
                datos.Parametro("IdFuente", pc.IdFuente);
                datos.Parametro("IdDiscoPrincipal", pc.IdDiscoPrincipal);
                datos.Parametro("IdDiscoSecundario", pc.IdDiscoSecundario);
                datos.Parametro("IdPlacaDeVideo", pc.IdPlacaDeVideo);
                datos.Parametro("Precio", pc.Precio);

                datos.ExecuteNonQuery();
                rpta = true;

            }
            catch (Exception)
            {
                rpta = false;
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
        }
        public bool Editar(PcArmadaModel pc)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();

            try
            {
                datos.Procedimiento("EditarPcArmada");

                datos.Parametro("IdPc", pc.IdPc);
                datos.Parametro("Nombre", pc.Nombre);
                datos.Parametro("IdProcesador", pc.IdProcesador);
                datos.Parametro("IdCooler", pc.IdCooler);
                datos.Parametro("IdPlacaMadre", pc.IdPlacaMadre);
                datos.Parametro("IdMemoriaRam", pc.IdMemoriaRam);
                datos.Parametro("IdGabinete", pc.IdGabinete);
                datos.Parametro("IdFuente", pc.IdFuente);
                datos.Parametro("IdDiscoPrincipal", pc.IdDiscoPrincipal);
                datos.Parametro("IdDiscoSecundario", pc.IdDiscoSecundario);
                datos.Parametro("IdPlacaDeVideo", pc.IdPlacaDeVideo);
                datos.Parametro("Precio", pc.Precio);
                datos.ExecuteNonQuery();
                rpta = true;

            }
            catch (Exception)
            {
                rpta = false;
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
        }
        public bool Eliminar(int id)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();

            try
            {
                datos.Procedimiento("EliminarPcArmada");
                datos.Parametro("IdPc", id);

                datos.ExecuteNonQuery();
                rpta = true;

            }
            catch (Exception)
            {
                rpta = false;
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
        }
    }
}
