using System.Data.SqlClient;
using System.Data;
using TiendaElectronica.Models;

namespace TiendaElectronica.Data
{
    public class ProductoDatos
    {
        public List<ProductoModel> Listar()
        {
			DatosConexion datos = new DatosConexion();
            List<ProductoModel> lista = new List<ProductoModel>();  
			try
			{
                datos.Procedimiento("ListarProductos");
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    ProductoModel producto = new ProductoModel();   
                    producto.Id = (int)datos.Lector["IdProducto"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    producto.Precio = (decimal)datos.Lector["Precio"];
                    producto.Marca = new MarcaModel();
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["Marca"];
                    producto.Categoria = new CategoriaModel();
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    producto.UrlImagen = (string)datos.Lector["UrlImagen"];
                    producto.Stock = (int)datos.Lector["Stock"];
                    lista.Add(producto);
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
        public ProductoModel Obtener(int id)
        {
            DatosConexion datos = new DatosConexion();
            ProductoModel producto = new ProductoModel();
            try
            {
                datos.Procedimiento("ObtenerProducto");
                datos.Parametro("Id",id);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    producto.Id = (int)datos.Lector["IdProducto"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    producto.Precio = (decimal)datos.Lector["Precio"];
                    producto.Marca = new MarcaModel();
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["Marca"];
                    producto.Categoria = new CategoriaModel();
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    producto.UrlImagen = (string)datos.Lector["UrlImagen"];
                    producto.Stock = (int)datos.Lector["Stock"];

                }
                return producto;
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
        public bool Registrar(ProductoModel producto)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();
            
            try
            {
                datos.Procedimiento("RegistrarProducto");
                datos.Parametro("Codigo", producto.Codigo);
                datos.Parametro("Nombre", producto.Nombre);
                datos.Parametro("Descripcion", producto.Descripcion);
                datos.Parametro("Precio", producto.Precio);
                datos.Parametro("IdMarca", producto.Marca.Id);
                datos.Parametro("IdCategoria", producto.Categoria.Id);
                datos.Parametro("UrlImagen", producto.UrlImagen);
                datos.Parametro("Stock", producto.Stock);
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
        public bool Editar(ProductoModel producto)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();

            try
            {
                datos.Procedimiento("EditarProducto");
                datos.Parametro("Id",producto.Id);
                datos.Parametro("Codigo", producto.Codigo);
                datos.Parametro("Nombre", producto.Nombre);
                datos.Parametro("Descripcion", producto.Descripcion);
                datos.Parametro("Precio", producto.Precio);
                datos.Parametro("IdMarca", producto.Marca.Id);
                datos.Parametro("IdCategoria", producto.Categoria.Id);
                datos.Parametro("UrlImagen", producto.UrlImagen);
                datos.Parametro("Stock", producto.Stock);
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
                datos.Procedimiento("EliminarProducto");
                datos.Parametro("Id", id);
               
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
