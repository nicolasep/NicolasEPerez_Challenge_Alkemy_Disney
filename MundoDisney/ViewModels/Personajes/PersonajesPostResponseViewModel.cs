using MundoDisney.ViewModels.PeliculasOSeries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ViewModels.Personajes
{
    public class PersonajesPostResponseViewModel
    {
        [Required(ErrorMessage ="El campo imagen es requerido")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres")]
        public string Imagen { get; set; }


        [Required(ErrorMessage = "El campo nombre es requerido")]
        [MinLength(3,ErrorMessage ="Debe tener al menos 3 caracteres")]
        
        public string Nombre { get; set; }
        public List<BasePeliculaOSerieResponseViewModel> Peliculas { get; set; }
    }
}
