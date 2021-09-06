using MundoDisney.ViewModels.PeliculasOSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ViewModels.Personajes
{
    public class PersonajeGetResponseFullViewModel
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public List<PeliculaOSerieResponseViewModel> Peliculas { get; set; }
    }
}
