using Microsoft.EntityFrameworkCore;
using MundoDisney.Context;
using MundoDisney.Entities;
using MundoDisney.Helpers;
using MundoDisney.Interfaces;
using MundoDisney.ResourceParameters;
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
        

        public PagedList<Personaje> GetPersonajesConPeliculasConParametros(PersonajeParameters personajeParameters)
        {
            var list = DbSet.Include(x => x.PeliculasOSeries) as IQueryable<Personaje>;
            
            return PagedList<Personaje>.Create(list,personajeParameters.PageNumber,personajeParameters.PageSize);
        }
        public List<Personaje> GetPersonajesConPeliculas()
        {
            var list = DbSet.Include(x => x.PeliculasOSeries);

            return list.ToList();
        }
        public Personaje GetPersonajePorId(int id)
        {
            return DbSet.Include(x => x.PeliculasOSeries).FirstOrDefault(x => x.Id == id);
        }

        
    }
}
