using MundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Interfaces
{
    public interface IPersonajeRepository:IRepository<Personaje>
    {
        List<Personaje> GetPersonajesConPeliculas();

        PeliculaOSerie BuscarPeliculaOSerie(int id);

        Personaje GetPersonajePorId(int id);
    }
}
