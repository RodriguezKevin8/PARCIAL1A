using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {

        private readonly Parcial1aContext _parcial1aContext;

        public AutorLibroController(Parcial1aContext parcial1aContext)
        {
            _parcial1aContext = parcial1aContext;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAsync()
        {
            List<AutorLibro> Autorlibro = await _parcial1aContext.AutorLibros.ToListAsync();

            if (Autorlibro.Count > 0)
            {
                return Ok(Autorlibro);
            }
            else
            {
                return NotFound();
          
            }
        }
        [HttpGet]
        [Route("GetbyName")]
        public async Task<IActionResult> GetbyName(string nombreAutor)
        {
            var autorLibros = await _parcial1aContext.Autores
                .Where(a => a.Nombre == nombreAutor)
                .SelectMany(a => a.AutorLibros)
                .Include(al => al.Libro)
                .Select(al => new
                {
                    AutorId = al.Autorid,
                    AutorNombre = al.Autor.Nombre,
                    LibroId = al.Libroid,
                    TituloLibro = al.Libro.Titulo,
                    Orden = al.Orden
                })
                .ToListAsync();

            if (autorLibros.Count > 0)
            {
                return Ok(autorLibros);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] AutorLibro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _parcial1aContext.AutorLibros.AddAsync(libro);
            await _parcial1aContext.SaveChangesAsync();

            return Ok(libro);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, [FromBody] AutorLibro libromodificado)
        {
            try
            {
                AutorLibro? libroactual = await _parcial1aContext.AutorLibros.FindAsync(id);

                if (libroactual == null)
                {
                    return NotFound();
                }
                libroactual.Autorid = libromodificado.Autorid;
                libroactual.Libroid = libromodificado.Libroid;
                libroactual.Orden = libromodificado.Orden;

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
        public async Task<IActionResult> EliminarcAsync(int id)
        {
            try
            {
                AutorLibro? libro = await _parcial1aContext.AutorLibros.FindAsync(id);

                if (libro == null)
                {
                    return NotFound();
                }
                _parcial1aContext.AutorLibros.Remove(libro);
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
