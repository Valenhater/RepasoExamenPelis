﻿using RepasoExamenPelis.Data;
using RepasoExamenPelis.Models;

namespace RepasoExamenPelis.Repositories
{
    public class RepositoryUsuarios
    {
        private PeliculasContext context;

        public RepositoryUsuarios(PeliculasContext context)
        {
            this.context = context;
        }
        public Usuario GetUserByEmailPassword(string email, string password)
        {
            return this.context.Usuarios.Where(x => x.Email == email && x.Password == password).AsEnumerable().FirstOrDefault();
        }
    }
}
