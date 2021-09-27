using MundoDisney.Entities;
using MundoDisney.Helpers;
using MundoDisney.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Interfaces
{
    public interface IPersonajeRepository:IRepository<Personaje>
    {
        PagedList<Personaje> GetPersonajesConPeliculasConParametros(BaseParametersGetAllViewModel personajeParameters);
        List<Personaje> GetPersonajesConPeliculas();


        Personaje GetPersonajePorId(int id);
    }
}
