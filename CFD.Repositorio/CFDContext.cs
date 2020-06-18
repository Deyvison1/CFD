using System;
using CFD.Dominio;
using Microsoft.EntityFrameworkCore;

namespace CFD.Repositorio
{
    public class CFDContext : DbContext
    {
        public CFDContext(DbContextOptions<CFDContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Divida> Dividas { get; set; }
        public DbSet<Renda> Rendas { get; set; }
    }
}
