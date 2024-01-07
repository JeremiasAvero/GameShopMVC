using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TiendaElectronica.Data;
using TiendaGamer.Extensions;
using TiendaGamer.Models;

namespace TiendaElectronica.Controllers
{
    public class HomeController : Controller
    {
        ProductoDatos datosProducto = new ProductoDatos();
        MarcaDatos datosMarca = new MarcaDatos();
        CategoriaDatos datosCategoria = new CategoriaDatos();
        public IActionResult Index(int? idProducto, int? idProductoElim)
        {
            //Lista de Productos

          
            ViewBag.marcas = datosMarca.Listar();
            ViewBag.categorias = datosCategoria.Listar();
           
            //perifericos
            var perifericos = datosProducto.Listar();
            perifericos = perifericos.Where(p => p.Categoria.Descripcion.ToLower().Contains("mouse")
            || p.Categoria.Descripcion.ToLower().Contains("teclado")
            || p.Categoria.Descripcion.ToLower().Contains("parlante")).ToList();

            Random random = new Random();
            var perifericosV = perifericos.OrderBy(x => random.Next()).ToList();
            ViewBag.perifericos = perifericosV;

            //componenetes de pc
            var componentes = datosProducto.Listar();
            componentes = componentes.Where(p => p.Categoria.Descripcion.ToLower().Contains("microprocesador")
            || p.Categoria.Descripcion.ToLower().Contains("placa")
            || p.Categoria.Descripcion.ToLower().Contains("motherb")
            || p.Categoria.Descripcion.ToLower().Contains("memoria")
            ).ToList();

            Random random2 = new Random();
            var componentesOrdenados = componentes.OrderBy(x => random2.Next()).ToList();
            ViewBag.componentes = componentesOrdenados;
            
            var listaProductos = datosProducto.Listar();
            Random random3 = new Random();
            var productosord = listaProductos.OrderBy(x => random3.Next()).ToList();
            listaProductos = productosord;


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
            if (idProductoElim != null)
            {
                if (HttpContext.Session.GetObject<List<int>>("carrito") != null)
                {
                    var carrito = HttpContext.Session.GetObject<List<int>>("carrito");
                    carrito.Remove(idProductoElim.Value);
                    HttpContext.Session.SetObject("carrito", carrito);


                    return View(listaProductos);
                }


            }

           
            return View(listaProductos);

        }
    }
}
