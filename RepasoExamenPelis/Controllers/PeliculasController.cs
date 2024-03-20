using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
