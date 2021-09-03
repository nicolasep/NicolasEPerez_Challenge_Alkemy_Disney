using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
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
        public IActionResult Get()
        {
            return Ok(_peliculaOSerieRepository.GetAllEntities());
        }

        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_peliculaOSerieRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post(PeliculaOSerie peliculaOSerie)
        {
            _peliculaOSerieRepository.Add(peliculaOSerie);

            return Ok(_peliculaOSerieRepository.GetAllEntities());
        }

        [HttpPut]
        public IActionResult Put(PeliculaOSerie peliculaOSerie)
        {
            if (_peliculaOSerieRepository.Get(peliculaOSerie.Id) == null)
            {
                return BadRequest(error: "La Pelicula o serie enviada no existe");
            }

            return Ok(_peliculaOSerieRepository.Update(peliculaOSerie));
        }
        [HttpDelete]
        [Route(template: "{id}")]
        public IActionResult Delete(int id)
        {

            if (_peliculaOSerieRepository.Get(id) == null)
            {
                return BadRequest(error: "La Pelicula o serie enviada no existe");
            }

            return Ok(_peliculaOSerieRepository.Delete(id));
        }
    }
}
