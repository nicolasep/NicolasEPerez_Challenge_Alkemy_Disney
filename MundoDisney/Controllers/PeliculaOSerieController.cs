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
    public class PeliculaOSerieController : ControllerBase
    {
        private readonly IPeliculaOSerieRepository _peliculaOSerieRepository;

        public PeliculaOSerieController(IPeliculaOSerieRepository peliculaOSerieRepository)
        {
            _peliculaOSerieRepository = peliculaOSerieRepository;
        }


        [HttpGet]
        [Route(template: "/movies")]
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
        [Route(template: "/movie/id")]
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
            var peliculaViewModel = new List<PeliculaOSerieGetResponseFullViewModel>();
            
            if (!string.IsNullOrEmpty(name))
            {
                listaPeliculasOSeries = listaPeliculasOSeries.Where(x => x.Titulo == name).ToList();
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
                if(genre>0)
                {
                    peliculaViewModel.FirstOrDefault(x => x.Genero.Id == genre);
                }
                if(order == "DESC")
                {
                    peliculaViewModel.OrderByDescending(x => x.Fecha_De_Creacion);
                }
                else
                {
                    peliculaViewModel.OrderBy(x => x.Fecha_De_Creacion);
                }
            }
            
            if (!peliculaViewModel.Any())
            {
                return BadRequest(error: "No existen peliculas con esa busqueda");
            }

            return Ok(peliculaViewModel);
        }
        
        [HttpPost]
        [Route(template: "/movies")]
        public IActionResult Post([FromBody] PeliculaOSeriePostResponseViewModel viewModel)
        {

            PeliculaOSerie aux = new PeliculaOSerie
            {
                Imagen = viewModel.Imagen,
                Titulo = viewModel.Titulo
            };

            if (viewModel.Personajes.Any())
            {
                foreach (var personaje in viewModel.Personajes)
                {
                    var dbPersonajes = _peliculaOSerieRepository.BuscarPeliculaOSerie(personaje.Id);

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
        [Route(template: "/movies/{movie}")]
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
        [Route(template: "/movie/")]
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
