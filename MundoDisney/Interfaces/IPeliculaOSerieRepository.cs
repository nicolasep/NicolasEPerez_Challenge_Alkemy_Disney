using MundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Interfaces
{
    public interface IPeliculaOSerieRepository:IRepository<PeliculaOSerie>
    {
        List<PeliculaOSerie> GetPersonajesConPeliculas();
        
        PeliculaOSerie GetPeliculaOSeriePorId(int id);
    }
}
