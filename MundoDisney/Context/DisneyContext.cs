using Microsoft.EntityFrameworkCore;
using MundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Context
{
    public class DisneyContext :DbContext
    {
        public DisneyContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Genero> Generos { get; set; } = null!;
        public DbSet<PeliculaOSerie> PeliculadOSeries { get; set; } = null!;
        public DbSet<Personaje> Personajes { get; set; } = null!;
    }
}
