using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoDisney.Entities;
using MundoDisney.Interfaces;

namespace MundoDisney.Controllers
{
    [ApiController]
    [Route("genre")]
    [Authorize]
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
        
        [HttpGet]
        [Route(template: "{id}")]
        public IActionResult GetId(int id)
        {
            return Ok(_generoRepository.Get(id));
        }
        
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post(Genero genero)
        {
            _generoRepository.Add(genero);

            return Ok(_generoRepository.GetAllEntities());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(Genero genero)
        {
            if (_generoRepository.Get(genero.Id) == null)
            {
                return BadRequest(error: "El genero enviado no existe");
            }

            return Ok(_generoRepository.Update(genero));
        }
        
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {

            if (_generoRepository.Get(id) == null)
            {
                return BadRequest(error: "El genero enviado no existe");
            }

            return Ok(_generoRepository.Delete(id));
        }
    }
}
