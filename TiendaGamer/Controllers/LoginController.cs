using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaElectronica.Data;
using TiendaElectronica.Models;
using System.Security.Cryptography.X509Certificates;

namespace TiendaElectronica.Controllers
{
    public class LoginController : Controller
    {
        UsuarioDatos datosUsuario = new UsuarioDatos();
        public IActionResult Login()
        {
            // Verificacion de que estea autenticado el usuario
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioModel u)
        {

            try
            {
                UsuarioModel usuario = datosUsuario.LoguearUsuario(u.NombreUsuario, u.Password);
                if (usuario.NombreUsuario != null)
                {
                    string rol = usuario.IdTipoUsuario.ToString();
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.NombreUsuario),
                        new Claim("Correo", usuario.Correo),
                        new Claim(ClaimTypes.Role, rol)

                    };
                    ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new();
                    //El usuario puede actualizar session
                    properties.AllowRefresh = true;
                    //El usuario si es persistente
                    properties.IsPersistent = usuario.MantenerActivo;


                    if (!u.MantenerActivo)
                    {
                        properties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                    }
                    else
                    {
                        properties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120);
                    }

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Datos incorrectos";
                }
                return View();
            }
            catch (Exception)
            {

               
                return View();
            }

            
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

            if (!datosUsuario.ValidarCorreoNombre(usuario.Correo, usuario.NombreUsuario))
            {
                var validacion = "El Correo o Nobre de usuario introducido ya está en uso, elegí otro.";
                ViewBag.validacion = validacion;
                return View();
            }
            
            var respuesta = datosUsuario.RegistrarUsuario(usuario);

            if (respuesta)
                return RedirectToAction("Index","Home");
            else
                return View();
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
