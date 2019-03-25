using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.WebAPI.Controllers
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllEventoAsync(true);
                return Ok(result);
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
                var result = await _repo.GetAllEventoAsyncByID(EventoId, false);
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
                var result = await _repo.GetAllEventoAsyncTema(tema, true);
                return Ok(result);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
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
                    return Created($"/api/evento/{model.Id}",model);
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado. No momento de criar evento ");
            }

                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncByID(EventoId,false);
                if(evento == null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",model);
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
            }

                return BadRequest();
        }

        [HttpDelete]
          public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncByID(EventoId,false);
                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync())
                {
                    //return OK();
                      
                }
               
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Error servidor não encotrado");
            }

                return BadRequest();
        }

        



    }
}