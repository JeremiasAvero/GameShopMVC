using Microsoft.AspNetCore.Mvc;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Extensions;
using TiendaGamer.Models;

namespace TiendaGamer.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index(int? idProductoQ, int? idProductoA, int? idProductoElim, int pg = 1)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            List<int> carrito = HttpContext.Session.GetObject<List<int>>("carrito");
            if (carrito == null)
                return View();
            else
            {

                //sacar producto
                if (idProductoQ != null)
                {
                    carrito.Remove(idProductoQ.Value);
                    HttpContext.Session.SetObject("carrito", carrito);

                }

                //productos en carrito
                //List<ProductoModel> productosEnCarrito = new List<ProductoModel>();              
                //productos
                List<ProductoModel> productos = datosProductos.Listar();

                Dictionary<int, int> productosEnCarritoConCantidad = new Dictionary<int, int>();

                foreach (int idProducto2 in carrito)
                {
                    if (productosEnCarritoConCantidad.ContainsKey(idProducto2))
                    {
                        // Si el producto ya está en el carrito, incrementa la cantidad
                        productosEnCarritoConCantidad[idProducto2]++;
                    }
                    else
                    {
                        // Si el producto no está en el carrito, agrégalo con cantidad 1
                        productosEnCarritoConCantidad.Add(idProducto2, 1);
                    }
                }

                List<ProductoEnCarritoModel> productosEnCarrito = new List<ProductoEnCarritoModel>();

                // Llena la lista final con detalles del producto y cantidad
                foreach (var entry in productosEnCarritoConCantidad)
                {
                    ProductoModel producto = productos.FirstOrDefault(p => p.Id == entry.Key);
                    if (producto != null)
                    {

                        productosEnCarrito.Add(new ProductoEnCarritoModel
                        {
                            Producto = producto,
                            Cantidad = entry.Value
                        });
                    }
                    if (idProductoElim != null)
                    {
                        // Remove all occurrences of the product ID from the cart
                        carrito.RemoveAll(id => id == idProductoElim.Value);
                        HttpContext.Session.SetObject("carrito", carrito);
                    }
                }
                //agregar producto
                if (idProductoA != null)
                {

                    if (HttpContext.Session.GetObject<List<int>>("carrito") == null)
                    {
                        carrito = new List<int>();

                    }
                    else
                    {
                        carrito = HttpContext.Session.GetObject<List<int>>("carrito");
                    }

                    carrito.Add(idProductoA.Value);
                    HttpContext.Session.SetObject("carrito", carrito);
                }
                //foreach (int idProducto2 in carrito)
                //{
                //    ProductoModel productoEnCarrito = productos.FirstOrDefault(p => p.Id == idProducto2);
                //    if (productoEnCarrito != null)
                //    {
                //        productosEnCarrito.Add(productoEnCarrito);
                //    }
                //}

                //ViewBag.pruebaprod = productoEnCarrito;
                const int pageSize = 14;
                if (pg < 1)
                    pg = 1;

                int recsCount = productosEnCarrito.Count();

                var paginador = new Paginador(recsCount, pg, pageSize);

                int recSkip = (pg - 1) * pageSize;

                var data = productosEnCarrito.Skip(recSkip).Take(paginador.PageSize).ToList();

                this.ViewBag.Pager = paginador;

                ViewBag.ATYISIV = HttpContext.Session.GetString("atyisiV");

                return View(productosEnCarrito);
            }
        }
    }
}
