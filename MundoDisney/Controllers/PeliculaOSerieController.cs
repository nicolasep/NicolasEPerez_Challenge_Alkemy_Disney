using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using MundoDisney.ViewModels.PeliculasOSeries;
using MundoDisney.ViewModels.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Controllers
{
    [ApiController]
    [Route("movies")]
    [Authorize]
    public class PeliculaOSerieController : ControllerBase
    {
        private readonly IPeliculaOSerieRepository _peliculaOSerieRepository;
        private readonly IPersonajeRepository _personajeRepository;

        public PeliculaOSerieController(IPeliculaOSerieRepository peliculaOSerieRepository, IPersonajeRepository personajeRepository)
        {
            _peliculaOSerieRepository = peliculaOSerieRepository;
            _personajeRepository = personajeRepository;
        }


        [HttpGet]
       
        public IActionResult Get()
        {
            var aux = _peliculaOSerieRepository.GetAllEntities();
            var peliculaSerieViewModel = new List<PeliculaOSerieGetResponseViewModel>();
            foreach (var pelicula in aux)
            {
                peliculaSerieViewModel.Add(new PeliculaOSerieGetResponseViewModel
                {
                    Imagen = pelicula.Imagen,
                    Titulo = pelicula.Titulo,
                    Fecha_De_Creacion = pelicula.Fecha_de_Creacion
                });
            }
            return Ok(peliculaSerieViewModel);
        }



        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult Get([FromQuery] int id)
        {
            var listaPeliculasOSeries = _peliculaOSerieRepository.GetPersonajesConPeliculas();
            var peliculaViewModel = new List<PeliculaOSerieGetResponseFullViewModel>();
            if (id > 0)
            {
                listaPeliculasOSeries = listaPeliculasOSeries.Where(x => x.Id == id).ToList();
                foreach (var pelicula in listaPeliculasOSeries)
                {
                    peliculaViewModel.Add(new PeliculaOSerieGetResponseFullViewModel
                    {
                        Id = pelicula.Id,
                        Imagen = pelicula.Imagen,
                        Fecha_De_Creacion = pelicula.Fecha_de_Creacion,
                        Calificacion = pelicula.Calificacion,
                        Personajes = pelicula.Personajes.Any() ? pelicula.Personajes.Select( x => new PersonajeGetResponseViewModel
                        {
                            Imagen = x.Imagen,
                            Nombre = x.Nombre
                        }).ToList() : null
                    });
                }
            }
            if (!peliculaViewModel.Any())
            {
                return BadRequest(error: "No existen peliculas con en esa busqueda");
            }

            return Ok(peliculaViewModel);
        }
        [HttpGet]
        [Route(template: "/movie")]
        public IActionResult Get([FromQuery] string name, int genre, string order)
        {
            var listaPeliculasOSeries = _peliculaOSerieRepository.GetPersonajesConPeliculas();
            listaPeliculasOSeries.Where(x => x.Titulo == name);
            if(listaPeliculasOSeries == null)
            {
                return BadRequest(error: $"No existe una pelicula con el nombre de: {name}");
            }
            var peliculaViewModel = new List<PeliculaOSerieGetResponseFullViewModel>();
            foreach (var pelicula in listaPeliculasOSeries)
            {
                peliculaViewModel.Add(new PeliculaOSerieGetResponseFullViewModel
                {
                    Id = pelicula.Id,
                    Imagen = pelicula.Imagen,
                    Fecha_De_Creacion = pelicula.Fecha_de_Creacion,
                    Calificacion = pelicula.Calificacion,
                    Genero = pelicula.Genero,
                    Personajes = pelicula.Personajes.Any() ? pelicula.Personajes.Select(x => new PersonajeGetResponseViewModel
                    {
                        Imagen = x.Imagen,
                        Nombre = x.Nombre
                    }).ToList() : null
                });
            }
            if (genre > 0)
            {
                listaPeliculasOSeries.Where(x => x.Genero.Id == genre);
            }
            switch(order.ToUpper())
            {
                case "DESC":
                    peliculaViewModel.OrderByDescending(x => x.Fecha_De_Creacion);
                    break;
                default:
                    peliculaViewModel.OrderBy(x => x.Fecha_De_Creacion);
                    break;

            }
            return Ok(listaPeliculasOSeries);
        }
        
        
        [HttpPost]
        
        public IActionResult Post([FromBody] PeliculaOSeriePostResponseViewModel viewModel)
        {
            
            PeliculaOSerie aux = new PeliculaOSerie
            {
                Imagen = viewModel.Imagen,
                Titulo = viewModel.Titulo
            };
            var existePelicula = _peliculaOSerieRepository.GetAllEntities().Where(x => x.Titulo == aux.Titulo);
            if(existePelicula != null)
            {
                return BadRequest(error: $"La pelicula {aux.Titulo} ya existe");
            }
            if (viewModel.Personajes.Any())
            {
                foreach (var personaje in viewModel.Personajes)
                {
                    var dbPersonajes = _personajeRepository.Get(personaje.Id);

                    if (dbPersonajes == null)
                    {
                        continue;
                    }
                    aux.Personajes.Add(dbPersonajes);
                }
            }
            _peliculaOSerieRepository.Add(aux);
            return Ok();
        }
        
        [HttpPut]
       
        public IActionResult Put(PeliculaOSerie peliculaOSerie)
        {
            var auxPersonaje = _peliculaOSerieRepository.Get(peliculaOSerie.Id);
            if (auxPersonaje == null)
            {
                return BadRequest(error: "El Personaje enviado no existe");
            }
            auxPersonaje.Imagen = peliculaOSerie.Imagen;
            auxPersonaje.Titulo = peliculaOSerie.Titulo;
            auxPersonaje.Calificacion = peliculaOSerie.Calificacion;

            _peliculaOSerieRepository.Update(auxPersonaje);

            return Ok(auxPersonaje);
        }

        [HttpDelete]
       
        public IActionResult Delete([FromQuery] int id)
        {

            if (_peliculaOSerieRepository.Get(id) == null)
            {
                return BadRequest(error: "La pelicula enviada no existe");
            }

            return Ok(_peliculaOSerieRepository.Delete(id));
        }
    }
}
