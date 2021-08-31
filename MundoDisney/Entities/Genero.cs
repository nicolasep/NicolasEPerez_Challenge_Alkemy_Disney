using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Entities
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public ICollection<PeliculaOSerie> PeliculasOSeries { get; set; }
    }
}
