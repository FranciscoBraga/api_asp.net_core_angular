using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);
                var results  = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado ");
            }
        }

        [HttpGet("{EventoId}")]
          public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncByID(EventoId, false);
                var result  = _mapper.Map<EventoDto>(evento);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
            }
        }

        
        [HttpGet("/getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsyncTema(tema, true);
                var results  = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _repo.Add(evento);
              
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",_mapper.Map<EventoDto>(evento));
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado. No momento de criar evento ");
            }

                return BadRequest();
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncByID(EventoId,false);
                if(evento == null) return NotFound();

                _mapper.Map(model,evento);

                _repo.Update(evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",_mapper.Map<EventoDto>(evento));
                     
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
            }

                return BadRequest();
        }

        [HttpDelete("{EventoId}")]
          public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncByID(EventoId,false);
                if(evento == null) return NotFound();

                _repo.Delete(evento);

              
                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                      
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encontrado");
            }

                return BadRequest();
        }

        



    }
}