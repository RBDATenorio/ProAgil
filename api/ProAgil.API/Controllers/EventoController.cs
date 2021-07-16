using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.DTOs;
using ProAgil.Domain.Entities;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);

                var results = _mapper.Map<IEnumerable<EventoDTO>>(eventos);

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
                var evento = await _repo.GetEventoAsyncById(id, true);

                var results = _mapper.Map<EventoDTO>(evento);

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
                var eventos = await _repo.GetAllEventoAsyncByTema(tema, true);

                var results = _mapper.Map<IEnumerable<EventoDTO>>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _repo.Add(evento);
                
                if (await _repo.SaveChangesAsync())
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

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                _repo.Update(model);

                if (await _repo.SaveChangesAsync())
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(id, false);

                if (evento == null)
                {
                    return NotFound();
                }

                _repo.Delete(evento);

                if (await _repo.SaveChangesAsync())
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