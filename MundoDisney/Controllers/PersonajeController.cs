using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using MundoDisney.ViewModels.PeliculasOSeries;
using MundoDisney.ViewModels.Personajes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Controllers
{
    [ApiController]
    [Route("characters")]
    [Authorize]
    public class PersonajeController:ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        private readonly IPeliculaOSerieRepository _peliculaOSerieRepository;

        public PersonajeController(IPersonajeRepository personajeRepository, IPeliculaOSerieRepository peliculaOSerieRepository)
        {
            _personajeRepository = personajeRepository;
            _peliculaOSerieRepository = peliculaOSerieRepository;
        }

        
        [HttpGet]
       
        public IActionResult Get()
        {
            var aux = _personajeRepository.GetAllEntities();
            var personajeViewModel = new List<PersonajeGetResponseViewModel>();
            foreach (var personaje in aux)
            {
                personajeViewModel.Add(new PersonajeGetResponseViewModel
                {
                    Imagen = personaje.Imagen,
                    Nombre = personaje.Nombre,
                });
            }
            return Ok(personajeViewModel);
        }
        
        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult Get([FromQuery]int id)
        {
            var listaPersonajes = _personajeRepository.GetPersonajesConPeliculas();
            var personajeViewModel = new List<PersonajeGetResponseFullViewModel>();
            if (id > 0)
            {
                listaPersonajes = listaPersonajes.Where(x => x.Id == id).ToList();
                foreach (var personaje in listaPersonajes)
                {
                    personajeViewModel.Add(new PersonajeGetResponseFullViewModel
                    {
                        Id = personaje.Id,
                        Imagen = personaje.Imagen,
                        Nombre = personaje.Nombre,
                        Peso = personaje.Peso,
                        Edad = personaje.Edad,
                        Historia = personaje.Historia,
                        Peliculas = personaje.PeliculasOSeries.Any() ? personaje.PeliculasOSeries.Select(x => new PeliculaOSerieResponseViewModel
                        {
                            Id = x.Id,
                            Nombre = x.Titulo
                        }).ToList() : null
                    });
                }
            }
            if (!personajeViewModel.Any())
            {
                return BadRequest(error: "No existen personajes con en esa busqueda");
            }

            return Ok(personajeViewModel);
        }

        
        [HttpGet]
        [Route(template: "/character")]
        public IActionResult Get([FromQuery] string name,int age, int movies)
        {
            var listaPersonajes = _personajeRepository.GetPersonajesConPeliculas().Where(x => x.Nombre == name);

            if(listaPersonajes == null)
            {
                return BadRequest(error: "No existe un personaje con ese nombre");
            }

            var personajeViewModel = new List<PersonajeGetResponseFullViewModel>();
                
        
            foreach (var personaje in listaPersonajes)
            {
                personajeViewModel.Add(new PersonajeGetResponseFullViewModel
                {
                    Id = personaje.Id,
                    Imagen = personaje.Imagen,
                    Nombre = personaje.Nombre,
                    Peliculas = personaje.PeliculasOSeries.Any() ? personaje.PeliculasOSeries.Select(x => new PeliculaOSerieResponseViewModel
                    {
                        Id = x.Id,
                        Nombre = x.Titulo
                    }).ToList() : null
                });
            }
            if (age > 0)
            {
                listaPersonajes = listaPersonajes.Where(x => x.Edad == age).ToList();
            }
            if (movies > 0)
            {
                listaPersonajes = listaPersonajes.Where(x => x.PeliculasOSeries.FirstOrDefault(x => x.Id == movies) != null).ToList();
                if (listaPersonajes != null)
                {
                    foreach (var personaje in listaPersonajes)
                    {
                        personajeViewModel.Add(new PersonajeGetResponseFullViewModel
                        {
                            Id = personaje.Id,
                            Imagen = personaje.Imagen,
                            Nombre = personaje.Nombre,
                            Peliculas = personaje.PeliculasOSeries.Any() ? personaje.PeliculasOSeries.Select(x => new PeliculaOSerieResponseViewModel
                            {
                                Id = x.Id,
                                Nombre = x.Titulo
                            }).ToList() : null
                        });
                    }
                }
            }
                    
             return Ok(personajeViewModel);
        }

        [HttpPost]
        
        public IActionResult Post([FromBody]PersonajesPostResponseViewModel viewModel)
        {
            
            Personaje aux = new Personaje
            {
                Imagen = viewModel.Imagen,
                Nombre = viewModel.Nombre,
                Edad = viewModel.Edad,
                Peso = viewModel.Peso
            };
            var existe =  _personajeRepository.GetAllEntities().Where(x => x.Nombre == aux.Nombre);
            if(existe != null)
            {
                return BadRequest(error:$"El personaje {aux.Nombre} ya existe");
            }

            if (viewModel.Peliculas.Any())
            {
                foreach (var pelicula in viewModel.Peliculas)
                {
                    var dbPelicula = _peliculaOSerieRepository.Get(pelicula.Id);

                    if (dbPelicula == null)
                    {
                        continue;
                    }
                    aux.PeliculasOSeries.Add(dbPelicula);
                }
            }
            _personajeRepository.Add(aux);
            return Ok("El Personaje fue creado con exito");
        }
        [HttpPut]
        
        public IActionResult Put(Personaje personaje)
        {
            var auxPersonaje = _personajeRepository.Get(personaje.Id);
            if (auxPersonaje == null)
            {
                return BadRequest(error: "El Personaje enviado no existe");
            }
            auxPersonaje.Imagen = personaje.Imagen;
            auxPersonaje.Nombre = personaje.Nombre;
            auxPersonaje.Edad = personaje.Edad;

            _personajeRepository.Update(personaje);

            return Ok(personaje);
        }

        [HttpDelete]
        
        public IActionResult Delete([FromQuery]int id)
        {

            if (_personajeRepository.Get(id) == null)
            {
                return BadRequest(error: "El Personaje enviado no existe");
            }

            return Ok(_personajeRepository.Delete(id));
        }
    }
}
