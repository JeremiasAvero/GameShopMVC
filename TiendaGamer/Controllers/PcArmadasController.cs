using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Data;
using TiendaGamer.Models;

namespace TiendaGamer.Controllers
{
    [Authorize(Roles = "2")]
    public class PcArmadasController : Controller
    {
        ProductoDatos datosProductos = new ProductoDatos();

        public void Productos()
        {
            //PROCESADORES
            var procesadores = datosProductos.Listar();
            procesadores = procesadores.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("microprocesadores")).ToList();
            ViewBag.procesadores = procesadores;

            //COOLERS
            var coolers = datosProductos.Listar();
            coolers = coolers.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("cooler")).ToList();
            ViewBag.coolers = coolers;

            //PLACA MADRE
            var placasmadre = datosProductos.Listar();
            placasmadre  = placasmadre.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("motherboard")).ToList();
            ViewBag.placasmadre = placasmadre;

            //MEMORIA RAM
            var memoriasram = datosProductos.Listar();
            memoriasram = memoriasram.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("memoria")).ToList();
            ViewBag.memoriasram = memoriasram;

            //GABINETE
            var gabinetes = datosProductos.Listar();
            gabinetes = gabinetes.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("gabinete")).ToList();
            ViewBag.gabinetes = gabinetes;

            //FUENTE
            var fuentes = datosProductos.Listar();
            fuentes = fuentes.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("fuente")).ToList();
            ViewBag.fuentes = fuentes;

            //DISCOS
            var discos = datosProductos.Listar();
            discos = discos.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("disco")).ToList();
            ViewBag.discos = discos;

            //PLACAS DE VIDEO
            var placasvideo = datosProductos.Listar();
            placasvideo = placasvideo.Where(p =>
                p.Categoria.Descripcion.ToLower().Contains("placa")).ToList(); 
            ViewBag.placasdevideo = placasvideo;

        }


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

        PcArmadaDatos datosPcArmadas = new PcArmadaDatos();

        public IActionResult Index()
        {
            var listaPc = datosPcArmadas.Listar();
            return View(listaPc);
        }
        public IActionResult Listar()
        {
            //Lista de Productos
            var listaPc = datosPcArmadas.Listar();
            return View(listaPc);
        }


        public IActionResult Registrar()
        {
            Productos();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(PcArmadaModel pc)
        {

            if (!ModelState.IsValid)
            {
                Productos();
                return View("Registrar");

            }

            var respuesta = datosPcArmadas.Registrar(pc);


            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                Productos();
                return View("Registrar");
            }

        }
        public IActionResult Editar(int id)
        {
            var pc = datosPcArmadas.Obtener(id);

            Productos();
            return View(pc);


        }
        [HttpPost]
        public async Task<IActionResult> Editar(PcArmadaModel pc)
        {

            if (!ModelState.IsValid)
            {
                Productos();
                return View("Editar");

            }

            var respuesta = datosPcArmadas.Editar(pc);


            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                Productos();
                return View("Editar");
            }

        }

        public IActionResult Eliminar(int id)
        {
            var respuesta = datosPcArmadas.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
