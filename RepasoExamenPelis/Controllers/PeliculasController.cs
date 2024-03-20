using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepasoExamenPelis.Extensions;
using RepasoExamenPelis.Filters;
using RepasoExamenPelis.Models;
using RepasoExamenPelis.Repositories;

namespace RepasoExamenPelis.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryPeliculas repo;
        public PeliculasController(RepositoryPeliculas repo) 
        {
            this.repo = repo;
        }
        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> Index()
        {
            List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
            return View(peliculas);
        }
        public async Task<IActionResult> PeliculasGenero(int idgenero)
        {
            List<Pelicula> peliculasGenero = await this.repo.PeliculasGenero(idgenero);
            return View(peliculasGenero);
        }
        public async Task<IActionResult> DetallesPelicula(int id)
        {
            Pelicula peli = await this.repo.FindpeliculaAsync(id);
            return View(peli);
        }
        public IActionResult GuardarPeliculaCarrito(int? idPelicula)
        {
            if (idPelicula != null)
            //GUARDAMOS EL PRODUCTO EN EL CARRITO
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                carrito.Add(idPelicula.Value);
                HttpContext.Session.SetObject("CARRITO", carrito);      
            }
            return RedirectToAction("Index");
        }
        public IActionResult Carrito(int? idPeliculaEliminar)
        {
            //LE PASAMOS EL CARRITO
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            //TIENES QUE CREAR PARA AÑADIR DATOS AL CARRITO
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idPeliculaEliminar != null)
                {
                    carrito.Remove(idPeliculaEliminar.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Pelicula> peliculas = this.repo.GetPeliculasCarrito(carrito);
                return View(peliculas);
            }
        }
        [AuthorizeUsuarios]
        public IActionResult Pedidos()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Pelicula> peliculas = this.repo.GetPeliculasCarrito(carrito);
            HttpContext.Session.Remove("CARRITO");
            return View(peliculas);
        }
    }
}
