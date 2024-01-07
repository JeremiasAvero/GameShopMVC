using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Extensions;

namespace TiendaGamer.Controllers
{
    public class ArmaTuPc : Controller
    {
        public void Productos()
        {
            ProductoDatos datosProductos = new ProductoDatos();
            //obtenemos procesador y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("procesador") != null && HttpContext.Session.GetString("procesador") != "0")
            {
                int idProcesador = int.Parse(HttpContext.Session.GetString("procesador"));
                ViewBag.ProcesadorElegido = datosProductos.Obtener(idProcesador);
            }

            //obtenemos cooler y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("cooler") != null && HttpContext.Session.GetString("cooler") != "0")
            {
                int idCooler = int.Parse(HttpContext.Session.GetString("cooler"));
                ViewBag.CoolerElegido = datosProductos.Obtener(idCooler);
            }
            //obtenemos placa madre y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("placamadre") != null && HttpContext.Session.GetString("placamadre") != "0")
            {
                int idPlacaMadre = int.Parse(HttpContext.Session.GetString("placamadre"));
                ViewBag.PlacaMadreElegida = datosProductos.Obtener(idPlacaMadre);
            }
            //obtenemos placa madre y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("memoriaram") != null && HttpContext.Session.GetString("memoriaram") != "0")
            {
                int idMemoriaRam = int.Parse(HttpContext.Session.GetString("memoriaram"));
                ViewBag.MemoriaRamElegida = datosProductos.Obtener(idMemoriaRam);
            }
            //obtenemos gabinete y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("gabinete") != null && HttpContext.Session.GetString("gabinete") != "0")
            {
                int idGabinete = int.Parse(HttpContext.Session.GetString("gabinete"));
                ViewBag.GabineteElegido = datosProductos.Obtener(idGabinete);
            }
            //obtenemos fuente y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("fuente") != null && HttpContext.Session.GetString("fuente") != "0")
            {
                int idFuente = int.Parse(HttpContext.Session.GetString("fuente"));
                ViewBag.FuenteElegida = datosProductos.Obtener(idFuente);
            }
            //obtenemos disco principal y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("discoprincipal") != null && HttpContext.Session.GetString("discoprincipal") != "0")
            {
                int idDiscoPrincipal = int.Parse(HttpContext.Session.GetString("discoprincipal"));
                ViewBag.DiscoPrincipalElegido = datosProductos.Obtener(idDiscoPrincipal);
            }
            //obtenemos fdisco secundario y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("discosecundario") != null && HttpContext.Session.GetString("discosecundario") != "0")
            {
                int idDiscoSecundario = int.Parse(HttpContext.Session.GetString("discosecundario"));
                ViewBag.DiscoSecundarioElegido = datosProductos.Obtener(idDiscoSecundario);
            }
            //obtenemos placa de video y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("placadevideo") != null && HttpContext.Session.GetString("placadevideo") != "0")
            {
                int idPlacaDeVideo = int.Parse(HttpContext.Session.GetString("placadevideo"));
                ViewBag.PlacaDeVideoElegida = datosProductos.Obtener(idPlacaDeVideo);
            }
            //obtenemos armado testeo e instalación y lo guardamos para mostrarlo
            if (HttpContext.Session.GetString("atyisi") != null && HttpContext.Session.GetString("atyisi") != "No")
            {
                string idATYI = HttpContext.Session.GetString("atyisi");
                ViewBag.ATYIElegido = idATYI;
            }
        }
 
        public IActionResult Procesador(int? idProducto,int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();
            
            //si se eligio un producto
            if (idProducto != null)
            {
                string Procesador;
                if (HttpContext.Session.GetString("procesador") == null)
                {
                    Procesador = idProducto.ToString();
                    HttpContext.Session.SetString("procesador", Procesador);
                }
                else
                {
                    Procesador = HttpContext.Session.GetString("procesador");
                    Procesador = idProducto.ToString();
                    HttpContext.Session.SetString("procesador", Procesador);
                }
            }
            
            //si se desa quitar un producto
            if(idProductoQuitar != null)
            {
                string Procesador;
                Procesador = HttpContext.Session.GetString("procesador");
                Procesador = "0";
                HttpContext.Session.SetString("procesador", Procesador);

            }

            //Listamos los productos que sean procesadores
            var listaProcesadores = datosProductos.Listar();
                listaProcesadores = listaProcesadores.Where(p =>
                    p.Categoria.Descripcion.ToLower().Contains("microprocesadores")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaProcesadores);
        }
        public IActionResult Cooler(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string Cooler;
                if (HttpContext.Session.GetString("cooler") == null)
                {
                    Cooler = idProducto.ToString();
                    HttpContext.Session.SetString("cooler", Cooler);
                }
                else
                {
                    Cooler = HttpContext.Session.GetString("cooler");
                    Cooler = idProducto.ToString();
                    HttpContext.Session.SetString("cooler", Cooler);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string Cooler;
                Cooler = HttpContext.Session.GetString("cooler");
                Cooler = "0";
                HttpContext.Session.SetString("cooler", Cooler);

            }

            //Listamos los productos que sean procesadores
            var listaCoolers = datosProductos.Listar();
            listaCoolers = listaCoolers.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("cooler")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaCoolers);
        }

        public IActionResult PlacaMadre(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string PlacaMadre;
                if (HttpContext.Session.GetString("placamadre") == null)
                {
                    PlacaMadre = idProducto.ToString();
                    HttpContext.Session.SetString("placamadre", PlacaMadre);
                }
                else
                {
                    PlacaMadre = HttpContext.Session.GetString("placamadre");
                    PlacaMadre = idProducto.ToString();
                    HttpContext.Session.SetString("placamadre", PlacaMadre);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string PlacaMadre;
                PlacaMadre = HttpContext.Session.GetString("placamadre");
                PlacaMadre = "0";
                HttpContext.Session.SetString("placamadre", PlacaMadre);

            }

            var listaPlacaMadre = datosProductos.Listar();
            listaPlacaMadre = listaPlacaMadre.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("motherboard")).ToList(); // Agrega más propiedades según sea necesario

            int idProcesador = int.Parse(HttpContext.Session.GetString("procesador"));
            var Procesador = datosProductos.Obtener(idProcesador);

            if (Procesador.Marca.Descripcion.ToLower().Contains("amd"))
            {


                listaPlacaMadre = listaPlacaMadre.Where(p =>
                    p.Nombre.ToLower().Contains("am4") || p.Nombre.ToLower().Contains("am5")).ToList(); // Agrega más propiedades según sea necesario
            }
            else
            {

                listaPlacaMadre = listaPlacaMadre.Where(p =>
                    !p.Nombre.ToLower().Contains("am4") && !p.Nombre.ToLower().Contains("am5")).ToList(); // Agrega más propiedades según sea necesario
            }
          

            Productos();
            return View(listaPlacaMadre);
        }
        public IActionResult MemoriaRam(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string MemoriaRam;
                if (HttpContext.Session.GetString("memoriaram") == null)
                {
                    MemoriaRam = idProducto.ToString();
                    HttpContext.Session.SetString("memoriaram", MemoriaRam);
                }
                else
                {
                    MemoriaRam = HttpContext.Session.GetString("memoriaram");
                    MemoriaRam = idProducto.ToString();
                    HttpContext.Session.SetString("memoriaram", MemoriaRam);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string MemoriaRam;
                MemoriaRam = HttpContext.Session.GetString("memoriaram");
                MemoriaRam = "0";
                HttpContext.Session.SetString("memoriaram", MemoriaRam);

            }

            //Listamos los productos que sean procesadores
            var listaMemoriaRam = datosProductos.Listar();
            listaMemoriaRam = listaMemoriaRam.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("memoria")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaMemoriaRam);
        }
        public IActionResult Gabinete(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string Gabinete;
                if (HttpContext.Session.GetString("gabinete") == null)
                {
                    Gabinete = idProducto.ToString();
                    HttpContext.Session.SetString("gabinete", Gabinete);
                }
                else
                {
                    Gabinete = HttpContext.Session.GetString("gabinete");
                    Gabinete = idProducto.ToString();
                    HttpContext.Session.SetString("gabinete", Gabinete);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string Gabinete;
                Gabinete = HttpContext.Session.GetString("gabinete");
                Gabinete = "0";
                HttpContext.Session.SetString("gabinete", Gabinete);

            }

            //Listamos los productos que sean procesadores
            var listaGabinete = datosProductos.Listar();
            listaGabinete = listaGabinete.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("gabinete")).ToList(); // Agrega más propiedades según sea necesario
            Productos();
            return View(listaGabinete);
        }
        public IActionResult Fuente(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string Fuente;
                if (HttpContext.Session.GetString("fuente") == null)
                {
                    Fuente = idProducto.ToString();
                    HttpContext.Session.SetString("fuente", Fuente);
                }
                else
                {
                    Fuente = HttpContext.Session.GetString("fuente");
                    Fuente = idProducto.ToString();
                    HttpContext.Session.SetString("fuente", Fuente);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string Fuente;
                Fuente = HttpContext.Session.GetString("fuente");
                Fuente = "0";
                HttpContext.Session.SetString("fuente", Fuente);

            }

            //Listamos los productos que sean procesadores
            var listaFuente = datosProductos.Listar();
            listaFuente = listaFuente.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("fuente")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaFuente);
        }
        public IActionResult DiscoPrincipal(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string DiscoPrincipal;
                if (HttpContext.Session.GetString("discoprincipal") == null)
                {
                    DiscoPrincipal = idProducto.ToString();
                    HttpContext.Session.SetString("discoprincipal", DiscoPrincipal);
                }
                else
                {
                    DiscoPrincipal = HttpContext.Session.GetString("discoprincipal");
                    DiscoPrincipal = idProducto.ToString();
                    HttpContext.Session.SetString("discoprincipal", DiscoPrincipal);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string DiscoPrincipal;
                DiscoPrincipal = HttpContext.Session.GetString("discoprincipal");
                DiscoPrincipal = "0";
                HttpContext.Session.SetString("discoprincipal", DiscoPrincipal);

            }

            //Listamos los productos que sean procesadores
            var listaDiscoPrincipal = datosProductos.Listar();
            listaDiscoPrincipal = listaDiscoPrincipal.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("disco")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaDiscoPrincipal);
        }
        public IActionResult DiscoSecundario(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string DiscoSecundario;
                if (HttpContext.Session.GetString("discosecundario") == null)
                {
                    DiscoSecundario = idProducto.ToString();
                    HttpContext.Session.SetString("discosecundario", DiscoSecundario);
                }
                else
                {
                    DiscoSecundario = HttpContext.Session.GetString("discosecundario");
                    DiscoSecundario = idProducto.ToString();
                    HttpContext.Session.SetString("discosecundario", DiscoSecundario);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string DiscoSecundario;
                DiscoSecundario = HttpContext.Session.GetString("discosecundario");
                DiscoSecundario = "0";
                HttpContext.Session.SetString("discosecundario", DiscoSecundario);

            }

            //Listamos los productos que sean procesadores
            var listaDiscoSecundario = datosProductos.Listar();
            listaDiscoSecundario = listaDiscoSecundario.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("disco")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaDiscoSecundario);
        }
        public IActionResult PlacaDeVideo(int? idProducto, int? idProductoQuitar)
        {
            ProductoDatos datosProductos = new ProductoDatos();

            //si se eligio un producto
            if (idProducto != null)
            {
                string PlacaDeVideo;
                if (HttpContext.Session.GetString("placadevideo") == null)
                {
                    PlacaDeVideo = idProducto.ToString();
                    HttpContext.Session.SetString("placadevideo", PlacaDeVideo);
                }
                else
                {
                    PlacaDeVideo = HttpContext.Session.GetString("placadevideo");
                    PlacaDeVideo = idProducto.ToString();
                    HttpContext.Session.SetString("placadevideo", PlacaDeVideo);
                }
            }

            //si se desa quitar un producto
            if (idProductoQuitar != null)
            {
                string PlacaDeVideo;
                PlacaDeVideo = HttpContext.Session.GetString("placadevideo");
                PlacaDeVideo = "0";
                HttpContext.Session.SetString("placadevideo", PlacaDeVideo);

            }

            //Listamos los productos que sean procesadores
            var listaPlacaDeVideo = datosProductos.Listar();
            listaPlacaDeVideo = listaPlacaDeVideo.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("placa")).ToList(); // Agrega más propiedades según sea necesario

            Productos();
            return View(listaPlacaDeVideo);
        }
        public IActionResult ATYI(string? ATYI)
        {
            if (ATYI != null && ATYI != "No")
            {
                string atyi;
                if (HttpContext.Session.GetString("atyisi") == null)
                {
                    atyi = ATYI.ToString();
                    HttpContext.Session.SetString("atyisi", atyi);
                }
                else
                {
                    atyi = HttpContext.Session.GetString("atyisi");
                    atyi = ATYI;
                    HttpContext.Session.SetString("atyisi", atyi);
                }
            }
            return View();
        }

        public IActionResult Comprar(string? idcompra)
        {
            if(idcompra == "si")
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

                Productos();

                if(ViewBag.ProcesadorElegido != null)
                {
                    ProductoModel procesador = ViewBag.ProcesadorElegido;
                    carrito.Add(procesador.Id);
                   
                }

                if (ViewBag.CoolerElegido != null)
                {
                    ProductoModel cooler = ViewBag.CoolerElegido;
                    carrito.Add(cooler.Id);

                }
                if (ViewBag.PlacaMadreElegida != null)
                {
                    ProductoModel placam = ViewBag.PLacaMadreElegida;
                    carrito.Add(placam.Id);

                }
                if (ViewBag.MemoriaRamElegida != null)
                {
                    ProductoModel memoram = ViewBag.MemoriaRamElegida;
                    carrito.Add(memoram.Id);

                }
                if (ViewBag.GabineteElegido != null)
                {
                    ProductoModel gabinete = ViewBag.GabineteElegido;
                    carrito.Add(gabinete.Id);

                }
                if (ViewBag.FuenteElegida != null)
                {
                    ProductoModel fuente = ViewBag.FuenteElegida;
                    carrito.Add(fuente.Id);

                }
                if (ViewBag.DiscoPrincipalElegido != null)
                {
                    ProductoModel discp = ViewBag.DiscoPrincipalElegido;
                    carrito.Add(discp.Id);

                }
                if (ViewBag.DiscoSecundarioElegido != null)
                {
                    ProductoModel discs = ViewBag.DiscoSecundarioElegido;
                    carrito.Add(discs.Id);

                }
                if (ViewBag.PlacaDeVideoElegido != null)
                {
                    ProductoModel placadevideo = ViewBag.PlacaDeVideoElegido;
                    carrito.Add(placadevideo.Id);


                }

                if (ViewBag.ATYIElegido != null)
                {
                    string atyisi = "V";
                    HttpContext.Session.SetString("atyisiV", atyisi);

                }
              
                HttpContext.Session.SetObject("carrito", carrito);
            }
            Productos();
            return View();
        }
    }

}
