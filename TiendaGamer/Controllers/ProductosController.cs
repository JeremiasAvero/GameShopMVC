using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Models;

namespace TiendaElectronica.Controllers
{

    [Authorize(Roles = "2")]
    public class ProductosController : Controller
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
        public IActionResult Listar( string? filtro, int pg = 1)
        {
            //Lista de Productos
            var listaProductos = datosProducto.Listar();
            
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower(); // Convierte el filtro a minúsculas para hacer una comparación insensible a mayúsculas
                listaProductos = listaProductos.Where(p =>
                    p.Nombre.ToLower().Contains(filtro) || // Reemplaza "Nombre" con la propiedad relevante del producto
                    p.Marca.ToString().ToLower().Contains(filtro) || p.Categoria.ToString().ToLower().Contains(filtro)).ToList(); // Agrega más propiedades según sea necesario
            }

            const int pageSize = 14;
            if (pg < 1)
                pg = 1;

            int recsCount = listaProductos.Count();

            var paginador = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = listaProductos.Skip(recSkip).Take(paginador.PageSize).ToList();

            this.ViewBag.Pager = paginador;
            

            return View(data);
        }
        
       
        public IActionResult Registrar()
        {

            ViewBag.marcas = listaMarcas();
            ViewBag.categorias = listaCategorias();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(ProductoModel producto, IFormFile imagen)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.marcas = listaMarcas();
                ViewBag.categorias = listaCategorias();
                return View("Registrar");

            }


            Stream image = imagen.OpenReadStream();
            string urlImagen = await SubirArchivo(image, producto.Codigo);
            producto.UrlImagen = urlImagen;
            var respuesta = datosProducto.Registrar(producto);


            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                ViewBag.marcas = listaMarcas();
                ViewBag.categorias = listaCategorias();
                return View("Registrar");
            }

        }
        public IActionResult Editar(int id)
        {
            var producto = datosProducto.Obtener(id);

            ViewBag.marcas = listaMarcas();
            ViewBag.categorias = listaCategorias();
            return View(producto);
        }
        [HttpPost]
        public IActionResult Editar(ProductoModel producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.marcas = listaMarcas();
                ViewBag.categorias = listaCategorias();
                return View("Editar");

            }

            var respuesta = datosProducto.Editar(producto);

            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                ViewBag.marcas = listaMarcas();
                ViewBag.categorias = listaCategorias();
                return View("Editar");

            }
        }
        public IActionResult Eliminar(int id)
        {
            var respuesta = datosProducto.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            string email = "jeremiasavero98@gmail.com";
            string clave = "Firebasegamerpro09";
            string ruta = "tiendagamer-2c0b1.appspot.com";
            string api_key = "AIzaSyDrfSGDUsOaoZCavLLkDPeqQSngrFb-1Eo";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("ImagenesProductos")
                .Child(nombre)
                .PutAsync(archivo, cancellation.Token);

            var downloadUrl = await task;
            return downloadUrl;
        }
    }
}
