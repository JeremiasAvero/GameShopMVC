using TiendaElectronica.Models;

namespace TiendaElectronica.Data
{
    public class CategoriaDatos
    {
        public List<CategoriaModel> Listar()
        {
            var lista = new List<CategoriaModel>();
            var datos = new DatosConexion();

            try
            {
                datos.Procedimiento("ListarCategorias");
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    CategoriaModel marca = new CategoriaModel();
                    marca.Id = (int)datos.Lector["IdCategoria"];
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
        public bool Registrar(CategoriaModel categoria)
        {
            bool rpta = false;
            DatosConexion datos = new DatosConexion();
            try
            {
                datos.Procedimiento("RegistrarCategoria");
                datos.Parametro("Descripcion", categoria.Descripcion);
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
        public CategoriaModel Obtener(int id)
        {
            DatosConexion datos = new DatosConexion();
            CategoriaModel categoria = new CategoriaModel();
            try
            {
                datos.Procedimiento("ObtenerCategoria");
                datos.Parametro("Id", id);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    categoria.Id = (int)datos.Lector["IdCategoria"];
                    categoria.Descripcion = (string)datos.Lector["Descripcion"];
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
            return categoria;
        }
        public bool Editar(CategoriaModel categoria)
        {
            bool rpta = false;
            DatosConexion datos = new DatosConexion();
            try
            {
                datos.Procedimiento("EditarCategoria");
                datos.Parametro("Id", categoria.Id);
                datos.Parametro("Descripcion", categoria.Descripcion);
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
                datos.Procedimiento("EliminarCategoria");
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
    