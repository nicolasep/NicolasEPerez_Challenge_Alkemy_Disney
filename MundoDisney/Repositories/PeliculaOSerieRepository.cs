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
    }
}
