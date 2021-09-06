using MundoDisney.Entities;
using MundoDisney.ViewModels.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ViewModels.PeliculasOSeries
{
    public class PeliculaOSerieGetResponseFullViewModel
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha_De_Creacion { get; set; }
        public int Calificacion { get; set; }

        public Genero Genero { get; set; }
        public List<PersonajeGetResponseViewModel> Personajes { get; set; }
    }
}
