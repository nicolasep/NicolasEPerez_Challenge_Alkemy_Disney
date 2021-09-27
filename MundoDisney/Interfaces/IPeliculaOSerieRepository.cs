using MundoDisney.Entities;
using MundoDisney.Helpers;
using MundoDisney.ResourceParameters;
using System.Collections.Generic;

namespace MundoDisney.Interfaces
{
    public interface IPeliculaOSerieRepository:IRepository<PeliculaOSerie>
    {
        List<PeliculaOSerie> GetPersonajesConPeliculas();

        PagedList<PeliculaOSerie> GetPersonajesConPeliculasConParametros(BaseParametersGetAllViewModel baseParametersGetAllViewModel);


        PeliculaOSerie GetPeliculaOSeriePorId(int id);
    }
}
