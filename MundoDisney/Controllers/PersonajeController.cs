using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Controllers
{
    public class PersonajeController:ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;

        public PersonajeController(IPersonajeRepository personajeRepository)
        {
            _personajeRepository = personajeRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personajeRepository.GetAllEntities());
        }

        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_personajeRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Post(Personaje personaje)
        {
            _personajeRepository.Add(personaje);

            return Ok(_personajeRepository.GetAllEntities());
        }

        [HttpPut]
        public IActionResult Put(Personaje personaje)
        {
            if (_personajeRepository.Get(personaje.Id) == null)
            {
                return BadRequest(error: "El Personaje enviado no existe");
            }

            return Ok(_personajeRepository.Update(personaje));
        }
        [HttpDelete]
        [Route(template: "{id}")]
        public IActionResult Delete(int id)
        {

            if (_personajeRepository.Get(id) == null)
            {
                return BadRequest(error: "El Personaje enviado no existe");
            }

            return Ok(_personajeRepository.Delete(id));
        }
    }
}
