using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly Parcial1aContext _parcial1aContext;

        public LibrosController(Parcial1aContext parcial1aContext)
        {
            _parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetAll2")]
        public async Task<IActionResult> GetAsync()
        {
            List<Libro> libro = await _parcial1aContext.Libros.ToListAsync();

            if (libro.Count > 0)
            {
                return Ok(libro);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _parcial1aContext.Libros.AddAsync(libro);
            await _parcial1aContext.SaveChangesAsync();

            return Ok(libro);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, [FromBody] Libro libromodificado)
        {
            try
            {
                Libro? libroactual = await _parcial1aContext.Libros.FindAsync(id);

                if (libroactual == null)
                {
                    return NotFound();
                }
                libroactual.Titulo= libromodificado.Titulo;
                await _parcial1aContext.SaveChangesAsync();
                return Ok(libroactual);
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
                Libro? libro = await _parcial1aContext.Libros.FindAsync(id);

                if (libro == null)
                {
                    return NotFound();
                }
                _parcial1aContext.Libros.Remove(libro);
                await _parcial1aContext.SaveChangesAsync();

                return Ok(libro);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

    }
}
