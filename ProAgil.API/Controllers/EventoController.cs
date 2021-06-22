using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.Entities;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var results = await _repo.GetEventoAsyncById(id, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
        }    

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.EventoId}", model);
                }
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
            return BadRequest();
        }                        
    
        [HttpPut]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(id, false);

                if(evento == null)
                {
                    return NotFound();
                }

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.EventoId}", model);
                }
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
            return BadRequest();
        }   

        [HttpDelete] 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(id, false);

                if(evento == null)
                {
                    return NotFound();
                }

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }                
                
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }

            return BadRequest();
        }
    }

}