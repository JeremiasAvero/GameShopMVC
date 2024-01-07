using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using TiendaGamer.Models;

namespace TiendaElectronica.Controllers
{
    [Authorize(Roles = "2")]
    public class MarcasController : Controller
    {
        public MarcaDatos datosMarca = new MarcaDatos();
        public IActionResult Listar(string? filtro, int pg = 1)
        {
            var lista = datosMarca.Listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                lista = lista.Where(p =>
                    p.Descripcion.ToLower().Contains(filtro) ).ToList();
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
        public IActionResult Registrar( MarcaModel marca)
        {
            if (!ModelState.IsValid || marca == null)
                return View("Registrar");

            var respuesta = datosMarca.Registrar(marca);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View("Registrar");
        }
        public IActionResult Editar(int id)
        {
            var producto = datosMarca.Obtener(id);
            return View(producto);
        }
        [HttpPost]
        public IActionResult Editar(MarcaModel marca)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(marca.Descripcion))
                return View("Editar");

            var respuesta = datosMarca.Editar(marca);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View("Editar");
        }
        public IActionResult Eliminar(int id)
        {
            var respuesta = datosMarca.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
