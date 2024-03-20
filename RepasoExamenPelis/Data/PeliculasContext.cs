using Microsoft.EntityFrameworkCore;
using RepasoExamenPelis.Models;

namespace RepasoExamenPelis.Data
{
    public class PeliculasContext: DbContext
    {
        public PeliculasContext(DbContextOptions<PeliculasContext> options) : base(options) { }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
