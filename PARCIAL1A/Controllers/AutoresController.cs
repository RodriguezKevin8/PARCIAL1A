using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly Parcial1aContext _parcial1aContext;

        public AutoresController(Parcial1aContext parcial1aContext)
        {
            _parcial1aContext = parcial1aContext;
        }

   

        [HttpGet]
        [Route("GetAll2")]
        public async Task<IActionResult> GetAsync()
        {
            List<Autore> autores = await _parcial1aContext.Autores.ToListAsync();

            if (autores.Count > 0)
            {
                return Ok(autores);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] Autore autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _parcial1aContext.Autores.AddAsync(autor);
            await _parcial1aContext.SaveChangesAsync();

            return Ok(autor);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> ActualizarAutor(int id, [FromBody] Autore autormodificado)
        {
            try
            {
                Autore? autoractual = await _parcial1aContext.Autores.FindAsync(id);

                if (autoractual == null)
                {
                    return NotFound();
                }
                autoractual.Nombre = autormodificado.Nombre;
                await _parcial1aContext.SaveChangesAsync();
                return Ok(autoractual); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarclienteAsync(int id)
        {
            try
            {
                Autore? autor = await _parcial1aContext.Autores.FindAsync(id);

                if (autor == null)
                {
                    return NotFound();
                }
                _parcial1aContext.Autores.Remove(autor);
                await _parcial1aContext.SaveChangesAsync();

                return Ok(autor); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
    }
}
