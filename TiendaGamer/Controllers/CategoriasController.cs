using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Models;

namespace TiendaElectronica.Controllers
{
    [Authorize(Roles = "2")]
    public class CategoriasController : Controller
    {
        public CategoriaDatos datosCategoria = new CategoriaDatos();
        public IActionResult Listar(string? filtro, int pg = 1)
        {
            var lista = datosCategoria.Listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                lista = lista.Where(p =>
                    p.Descripcion.ToLower().Contains(filtro)).ToList();
            }

            const int pageSize = 14;

            if (pg < 1)
                pg = 1;

            int recsCount = lista.Count();

            var paginador = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = lista.Skip(recSkip).Take(paginador.PageSize).ToList();

            this.ViewBag.Pager = paginador;

            return View(data);
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(CategoriaModel categoria)
        {
            if (!ModelState.IsValid || categoria == null)
                return View("Registrar");

            var respuesta = datosCategoria.Registrar(categoria);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View("Registrar");
        }
        public IActionResult Editar(int id)
        {
            var categoria = datosCategoria.Obtener(id);
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Editar(CategoriaModel categoria)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(categoria.Descripcion))
                return View("Editar");

            var respuesta = datosCategoria.Editar(categoria);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View("Editar");
        }
        public IActionResult Eliminar(int id)
        {
            var respuesta = datosCategoria.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
   
   
}
