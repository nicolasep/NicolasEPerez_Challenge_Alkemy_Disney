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
    public class PeliculaOSerieRepository : BaseRepository<PeliculaOSerie, DisneyContext>, IPeliculaOSerieRepository
    {
        public PeliculaOSerieRepository(DisneyContext context) : base(context)
        {

        }
        public List<PeliculaOSerie> GetPersonajesConPeliculas()
        {
            
            return DbSet.Include(x => x.Personajes).Include(x =>x.Genero).ToList();
        }
        
        
        public PeliculaOSerie GetPeliculaOSeriePorId(int id)
        {
            return DbSet.Include(x => x.Personajes).FirstOrDefault(x => x.Id == id);
        }
    }
}
