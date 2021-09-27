using Microsoft.EntityFrameworkCore;
using MundoDisney.Context;
using MundoDisney.Entities;
using MundoDisney.Helpers;
using MundoDisney.Interfaces;
using MundoDisney.ResourceParameters;
using System.Collections.Generic;
using System.Linq;

namespace MundoDisney.Repositories
{
    public class PeliculaOSerieRepository : BaseRepository<PeliculaOSerie, DisneyContext>, IPeliculaOSerieRepository
    {
        
        public PeliculaOSerieRepository(DisneyContext context) : base(context)
        {

        }
        public PagedList<PeliculaOSerie> GetPersonajesConPeliculasConParametros(BaseParametersGetAllViewModel baseParametersGetAllViewModel)
        {
            var list = DbSet.Include(x => x.Personajes).Include(x => x.Genero) as IQueryable<PeliculaOSerie>;
            return PagedList<PeliculaOSerie>.Create(list, baseParametersGetAllViewModel.PageNumber, baseParametersGetAllViewModel.PageSize);
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
