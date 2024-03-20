using Microsoft.EntityFrameworkCore;
using RepasoExamenPelis.Data;
using RepasoExamenPelis.Models;

namespace RepasoExamenPelis.Repositories
{
    public class RepositoryPeliculas
    {
        private PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context)
        {
            this.context = context;
        }
        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await this.context.Peliculas.ToListAsync();
        }
        public async Task<Pelicula> FindpeliculaAsync(int idPelicula)
        {
            return await this.context.Peliculas.FirstOrDefaultAsync(x => x.IdPelicula == idPelicula);
        }
        public async Task<List<Pelicula>> PeliculasGenero(int idgenero)
        {
            var consulta = from datos in context.Peliculas
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }
        public List<Genero> GetAllGeneros()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return consulta.ToList();
        }

    }
}
