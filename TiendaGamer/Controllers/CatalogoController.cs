using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Data;
using TiendaGamer.Extensions;
using TiendaGamer.Models;

namespace TiendaGamer.Controllers
{
    public class CatalogoController : Controller
    {
        public MarcaDatos marcaDatos = new MarcaDatos();
        public CategoriaDatos categoriaDatos = new CategoriaDatos();

        public List<MarcaModel> listaMarcas()
        {
            List<MarcaModel> lista = marcaDatos.Listar();
            return lista;
        }
        public List<CategoriaModel> listaCategorias()
        {
            List<CategoriaModel> lista = categoriaDatos.Listar();
            return lista;
        }

        ProductoDatos datosProducto = new ProductoDatos();

        public IActionResult Listar(int? idProducto, string? filtro, int pg = 1)
        {
            
            var listaProductos = datosProducto.Listar();
            ViewBag.marcas = listaMarcas();
            ViewBag.categorias = listaCategorias();
            if (!string.IsNullOrEmpty(filtro))
            {
                ViewBag.Filtro = filtro;
                filtro = filtro.ToLower(); // Convierte el filtro a minúsculas para hacer una comparación insensible a mayúsculas
                listaProductos = listaProductos.Where(p =>
                    p.Nombre.ToLower().Contains(filtro) || // Reemplaza "Nombre" con la propiedad relevante del producto
                    p.Marca.ToString().ToLower().Contains(filtro) || p.Categoria.ToString().ToLower().Contains(filtro)).ToList(); // Agrega más propiedades según sea necesario
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("FPrecio")) || !string.IsNullOrEmpty(HttpContext.Session.GetString("FMarca")) || !string.IsNullOrEmpty(HttpContext.Session.GetString("FCategoria")))
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("FPrecio")))
                {
                    var FPrecio = HttpContext.Session.GetString("FPrecio");
                    if (FPrecio == "Menor Precio")
                    {
                        listaProductos = listaProductos.OrderBy(p => p.Precio).ToList();
                    }
                    if (FPrecio == "Mayor Precio")
                    {
                        listaProductos = listaProductos.OrderByDescending(p => p.Precio).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("FMarca")))
                {
                    string FMarca = HttpContext.Session.GetString("FMarca");

                    listaProductos = listaProductos.Where(p =>
                         p.Marca.ToString().ToLower().Contains(FMarca.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("FCategoria")))
                {
                    string FCategoria = HttpContext.Session.GetString("FCategoria");

                    listaProductos = listaProductos.Where(p =>
                         p.Categoria.ToString().ToLower().Contains(FCategoria.ToLower())).ToList();

                }
       
            }
            else
            {
                HttpContext.Session.Remove("FPrecio");
                HttpContext.Session.Remove("FMarca");
                HttpContext.Session.Remove("FCategoria");
            }
               
            //paginacion
            const int pageSize = 14;
            if (pg < 1)
                pg = 1;

            int recsCount = listaProductos.Count();

            var paginador = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = listaProductos.Skip(recSkip).Take(paginador.PageSize).ToList();

            this.ViewBag.Pager = paginador;
            //paginacion

            //si viene un id de producto
            if (idProducto != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("carrito") == null)
                {
                    carrito = new List<int>();

                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("carrito");
                }

                    carrito.Add(idProducto.Value);
                    HttpContext.Session.SetObject("carrito", carrito);
            }
            //if (idProductoElim != null)
            //{
            //    if (HttpContext.Session.GetObject<List<int>>("carrito") != null)
            //    {
            //        var carrito = HttpContext.Session.GetObject<List<int>>("carrito");
            //        carrito.Remove(idProductoElim.Value);
            //        HttpContext.Session.SetObject("carrito", carrito);

            //        return View(data);
            //    }

            //}

            return View(data);
        }
        public IActionResult Filtros(string? QFMarca, string? QFCategoria, string? QFPrecio, string? FPrecio, string? FMarca, string? FCategoria)
        {

            if (!string.IsNullOrEmpty(FMarca) || !string.IsNullOrEmpty(FCategoria) || !string.IsNullOrEmpty(FPrecio))
            {
                if (!string.IsNullOrEmpty(FMarca))
                {
                    HttpContext.Session.SetString("FMarca", FMarca);
                }

                if (!string.IsNullOrEmpty(FCategoria))
                {
                    HttpContext.Session.SetString("FCategoria", FCategoria);

                }
                if (!string.IsNullOrEmpty(FPrecio))
                {
                    HttpContext.Session.SetString("FPrecio", FPrecio);

                }
            }
            if (!string.IsNullOrEmpty(QFMarca) || !string.IsNullOrEmpty(QFCategoria) || !string.IsNullOrEmpty(QFPrecio))
            {
                if (!string.IsNullOrEmpty(QFMarca))
                {
                    HttpContext.Session.Remove("FMarca"); 
                }
                if (!string.IsNullOrEmpty(QFCategoria))
                {
                    HttpContext.Session.Remove("FCategoria");

                }
                if (!string.IsNullOrEmpty(QFPrecio))
                {
                    HttpContext.Session.Remove("FPrecio");

                }

            }
           

            return RedirectToAction("Listar");
        }
        public IActionResult PcArmadas()
        {
            PcArmadaDatos datosPcs = new PcArmadaDatos();
            var listaPcs = datosPcs.Listar();
            return View(listaPcs);
        }
        public IActionResult PcArmada(int id, string? idcompra)
        {
            PcArmadaDatos datospc = new PcArmadaDatos();

            PcArmadaModel pcs = datospc.Obtener(id);
            if (idcompra == "si")
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("carrito") == null)
                {
                    carrito = new List<int>();

                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("carrito");
                }

                if (pcs.IdProcesador != null)
                {
                    carrito.Add(pcs.IdProcesador);
                }

                if (pcs.IdCooler != null)
                {
                    carrito.Add(pcs.IdCooler);
                }

                if (pcs.IdPlacaMadre != null)
                {
                    carrito.Add(pcs.IdPlacaMadre);
                }

                if (pcs.IdMemoriaRam != null)
                {
                    carrito.Add(pcs.IdMemoriaRam);
                }

                if (pcs.IdGabinete != null)
                {
                    carrito.Add(pcs.IdGabinete);
                }

                if (pcs.IdFuente != null)
                {
                    carrito.Add(pcs.IdFuente);
                }

                if (pcs.IdDiscoPrincipal != null)
                {
                    carrito.Add(pcs.IdDiscoPrincipal);
                }

                if (pcs.IdDiscoSecundario != null)
                {
                    carrito.Add(pcs.IdDiscoSecundario);
                }

                if (pcs.IdPlacaDeVideo != null)
                {
                    carrito.Add(pcs.IdPlacaDeVideo);
                }

                if (ViewBag.ATYIElegido != null)
                {
                    string atyisi = "V";
                    HttpContext.Session.SetString("atyisiV", atyisi);

                }

                HttpContext.Session.SetObject("carrito", carrito);
            }

            return View(pcs);
        }
        public IActionResult VerProducto(int? idProducto)
        {

            int id = int.Parse(idProducto.ToString());
            ProductoDatos datosProductos = new ProductoDatos();
            ProductoModel productoElegido = datosProductos.Obtener(id);

            List<ProductoModel> listaProductos = datosProductos.Listar();

            listaProductos = listaProductos.Where(p => p.Categoria.Descripcion.Contains(productoElegido.Categoria.Descripcion) || p.Marca.Descripcion.Contains(productoElegido.Marca.Descripcion)).ToList();
            ViewBag.ProductosRelacionados = listaProductos;
            if (idProducto != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("carrito") == null)
                {
                    carrito = new List<int>();

                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("carrito");
                }

                if (carrito.Contains(idProducto.Value) == false)
                {
                    carrito.Add(idProducto.Value);
                    HttpContext.Session.SetObject("carrito", carrito);
                }
            }

            return View(productoElegido);
        }

    }




}
