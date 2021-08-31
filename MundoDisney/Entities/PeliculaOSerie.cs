using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Entities
{
    public class PeliculaOSerie
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha_de_Creacion { get; set; }

        [Range(1,5)]
        public int Calificacion { get; set; }

        public Genero Genero { get; set; }
        public ICollection<Personaje> Personajes { get; set; }

    }
}
