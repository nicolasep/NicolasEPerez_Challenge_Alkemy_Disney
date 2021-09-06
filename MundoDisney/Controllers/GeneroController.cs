using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Controllers
{
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;
       
        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_generoRepository.GetAllEntities());
        }
        /*
        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_generoRepository.Get(id));
        }
        */
        [HttpPost]
        public IActionResult Post(Genero genero)
        {
            _generoRepository.Add(genero);

            return Ok(_generoRepository.GetAllEntities());
        }

        [HttpPut]
        public IActionResult Put(Genero genero)
        {
            if (_generoRepository.Get(genero.Id) == null)
            {
                return BadRequest(error: "El genero enviado no existe");
            }

            return Ok(_generoRepository.Update(genero));
        }
        /*
        [HttpDelete]
        [Route(template: "{id}")]
        public IActionResult Delete(int id)
        {

            if (_generoRepository.Get(id) == null)
            {
                return BadRequest(error: "El genero enviado no existe");
            }

            return Ok(_generoRepository.Delete(id));
        }*/
    }
}
