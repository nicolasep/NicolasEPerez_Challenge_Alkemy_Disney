using MundoDisney.Entities;
using MundoDisney.ViewModels.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ViewModels.PeliculasOSeries
{
    public class PeliculaOSerieGetResponseFullViewModel : PeliculaOSerieGetResponseViewModel
    {

        public int Id { get; set; }
        public int Calificacion { get; set; }

        public Genero Genero { get; set; }
        public List<PersonajeGetResponseViewModel> Personajes { get; set; }
    }
}
