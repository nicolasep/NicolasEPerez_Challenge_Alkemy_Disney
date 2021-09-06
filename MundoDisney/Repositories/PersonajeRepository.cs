using Microsoft.EntityFrameworkCore;
using MundoDisney.Context;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Repositories
{
    public class PersonajeRepository : BaseRepository<Personaje, DisneyContext>, IPersonajeRepository
    {
        public PersonajeRepository(DisneyContext context) : base(context)
        {
            
        }
        public List<Personaje> GetPersonajesConPeliculas()
        {
            return _context.Set<Personaje>().Include(x => x.PeliculasOSeries).ToList(); 
        }
        public PeliculaOSerie BuscarPeliculaOSerie(int id)
        {
            return _context.PeliculadOSeries.Find(id);
        }
        public Personaje GetPersonajePorId(int id)
        {
            return DbSet.Include(x => x.PeliculasOSeries).FirstOrDefault(x => x.Id == id);
        }
    }
}
