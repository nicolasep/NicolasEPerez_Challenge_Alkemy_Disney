using MundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ViewModels.PeliculasOSeries
{
    public class PeliculaOSeriePostResponseViewModel
    {
        [Required(ErrorMessage = "El campo imagen es requerido")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        public string Imagen { get; set; }


        [Required(ErrorMessage = "El campo titulo es requerido")]
        [MinLength(3, ErrorMessage = "Debe tener al menos 3 caracteres")]
        public string Titulo { get; set; }
        public List<Personaje> Personajes { get; set; }
       
        [Range(1, 5)]
        public int Calificacion { get; set; }

    }
}
