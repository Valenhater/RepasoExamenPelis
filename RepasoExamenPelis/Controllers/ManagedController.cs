using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RepasoExamenPelis.Models;
using RepasoExamenPelis.Repositories;
using System.Security.Claims;

namespace RepasoExamenPelis.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsuarios repo;

        public ManagedController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = 
                this.repo.GetUserByEmailPassword(email, password);
            if (usuario != null)
            {
                //SEGURIDAD
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                //CREAMOS EL CLAIM PARA EL NOMBRE (APELLIDO)
                Claim claimName =
                    new Claim(ClaimTypes.Name, usuario.Nombre);
                identity.AddClaim(claimName);

                Claim claimId =
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(claimId);

                Claim claimEmail =
                    new Claim(ClaimTypes.Role, usuario.Email);
                identity.AddClaim(claimEmail);

                Claim claimIDforma2 =
                    new Claim("id", usuario.IdUsuario.ToString());
                identity.AddClaim(claimIDforma2);

                Claim claimEmail2 =
                    new Claim("Email", usuario.Email.ToString());
                identity.AddClaim(claimEmail2);

                //COMO POR AHORA NO VOY A UTILIZAR NI SE UTILIZAR ROLES
                //NO LO INCLUIMOS
                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                //LO VAMOS A LLEVAR A UNA VISTA CON LA INFORMACION
                //QUE NOS DEVUELVE EL FILTER EN TEMPDATA
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                if (TempData["id"] != null)
                {
                    string id = "";
                    id = TempData["id"].ToString();
                    return RedirectToAction(action, controller, new { id = id });
                }
                else
                {
                    return RedirectToAction(action, controller);
                }
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Peliculas");
        }
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
