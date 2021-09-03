using MundoDisney.Context;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Repositories
{
    public class GeneroRepository: BaseRepository<Genero,DisneyContext>, IGeneroRepository
    {
        public GeneroRepository(DisneyContext context) : base(context)
        {

        }
    }
}
