using TiendaElectronica.Models;

namespace TiendaElectronica.Data
{
    public class MarcaDatos
    {
        public List<MarcaModel> Listar()
        {
            var lista = new List<MarcaModel>();
            var datos = new DatosConexion();

            try
            {
                datos.Procedimiento("ListarMarcas");
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    MarcaModel marca = new MarcaModel();
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(marca);
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

        public bool Registrar(MarcaModel marca)
        {
            bool rpta = false;
            DatosConexion datos = new DatosConexion();
            try
            {
                datos.Procedimiento("RegistrarMarca");
                datos.Parametro("Descripcion", marca.Descripcion);
                datos.ExecuteNonQuery();
                rpta = true;    
               
            }
            catch (Exception ex)
            {
                rpta = false;
                throw ex;
                
            }
            finally
            {

                datos.CerrarConexion();   
            }
            return rpta;
        }
        public MarcaModel Obtener(int id)
        {
            DatosConexion datos = new DatosConexion();
            MarcaModel marca = new MarcaModel();    
            try
            {
                datos.Procedimiento("ObtenerMarca");
                datos.Parametro("Id", id);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.CerrarConexion();
            }
            return marca;
        }
        public bool Editar(MarcaModel marca)
        {
            bool rpta = false;
            DatosConexion datos = new DatosConexion();
            try
            {
                datos.Procedimiento("EditarMarca");
                datos.Parametro("Id", marca.Id);
                datos.Parametro("Descripcion", marca.Descripcion);
                datos.ExecuteNonQuery();
                rpta = true;

            }
            catch (Exception ex)
            {
                rpta = false;
                throw ex;

            }
            finally
            {

                datos.CerrarConexion();
            }
            return rpta;
        }
        public bool Eliminar(int id)
        {
            bool rpta = false;
            DatosConexion datos = new DatosConexion();
            try
            {
                datos.Procedimiento("EliminarMarca");
                datos.Parametro("Id", id);
                datos.ExecuteNonQuery();
                rpta = true;

            }
            catch (Exception ex)
            {
                rpta = false;
                throw ex;

            }
            finally
            {

                datos.CerrarConexion();
            }
            return rpta;
        }
    }
}
