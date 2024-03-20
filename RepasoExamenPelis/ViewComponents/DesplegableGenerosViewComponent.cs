using Microsoft.AspNetCore.Mvc;
using RepasoExamenPelis.Models;
using RepasoExamenPelis.Repositories;

namespace RepasoExamenPelis.ViewComponents
{
    public class DesplegableGenerosViewComponent : ViewComponent
    {
        private RepositoryPeliculas repo;

        public DesplegableGenerosViewComponent(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = this.repo.GetAllGeneros();
            return View(generos);
        }
    }
}
