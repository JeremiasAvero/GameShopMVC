using Microsoft.AspNetCore.Mvc;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TiendaGamer.Models;

namespace TiendaElectronica.Controllers
{
    [Authorize(Roles = "2")]
    //[Authorize()]
    public class UsuariosController : Controller
    {


        UsuarioDatos datosUsuario = new UsuarioDatos();
        
        public IActionResult Listar(string? filtro,int pg = 1)
        {

            //Lista de Usuarios

            var listaUsers = datosUsuario.ListarUsuarios();
            
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower(); // Convierte el filtro a minúsculas para hacer una comparación insensible a mayúsculas
                listaUsers = listaUsers.Where(p =>
                    p.NombreUsuario.ToLower().Contains(filtro) || // Reemplaza "Nombre" con la propiedad relevante del producto
                    p.Correo.ToLower().Contains(filtro)).ToList(); // Agrega más propiedades según sea necesario
            }
           
            const int pageSize = 14;
            if (pg < 1)
                pg = 1;

            int recsCount = listaUsers.Count();

            var paginador = new Paginador(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = listaUsers.Skip(recSkip).Take(paginador.PageSize).ToList();

            this.ViewBag.Pager = paginador;


            return View(data);
        }
        public IActionResult Registrar()
        {
            //devuelve vista de registrar
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            //metodo recibe el objeto para guardarlo en la db
            if (!ModelState.IsValid)
                return View();

            var respuesta = datosUsuario.RegistrarUsuario(usuario);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Editar(int idUsuario)
        {
            var usuario = datosUsuario.ObtenerUsuario(idUsuario);
            return View(usuario);


        }
        [HttpPost]
        public IActionResult Editar(UsuarioModel Edusuario)
        {
            if (!ModelState.IsValid)
                return View();
            var respuesta = datosUsuario.EditarUsuario(Edusuario);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Eliminar(int idUsuario)
        {
            var respuesta = datosUsuario.EliminarUsuario(idUsuario);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();


        }



    }
}
