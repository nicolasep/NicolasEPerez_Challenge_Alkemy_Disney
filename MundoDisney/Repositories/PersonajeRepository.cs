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
    }
}
