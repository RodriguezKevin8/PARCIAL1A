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
            var autorLibro1= await (from a in _parcial1aContext.Autores
                                     join autorLibro in _parcial1aContext.AutorLibros
                                     on a.Autorid equals autorLibro.Autorid
                                     where a.Nombre == nombreAutor
                                   from l in _parcial1aContext.Libros
                                   join AutorLibro in _parcial1aContext.AutorLibros
                                   on l.Libroid equals AutorLibro.Libroid
                                    where l.Libroid == a.Autorid

                                    select new
                                     {
                                         AutorId = a.Autorid,
                                         AutorNombre = a.Nombre,
                                         LibroId = l.Libroid,
                                         TituloLibro = l.Titulo,
                                         Orden = autorLibro.Orden
                                     }).ToListAsync();
            if (autorLibro1.Count > 0)
            {
                return Ok(autorLibro1);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<AutorLibro>> PostAutorLibro(AutorLibro autorLibro)
        {
            _parcial1aContext.AutorLibros.Add(autorLibro);
            await _parcial1aContext.SaveChangesAsync();

            return Ok(autorLibro);
        }

        [HttpPut]
        [Route("actualizar/{autorId}/{libroId}")]
        public async Task<IActionResult> ActualizarAutorLibroAsync(int autorId, int libroId, [FromBody] AutorLibro nuevoAutorLibro)
        {
            var autorLibro = await _parcial1aContext.AutorLibros
                .FirstOrDefaultAsync(al => al.Autorid == autorId && al.Libroid == libroId);

            if (autorLibro != null)
            {
                autorLibro.Orden= nuevoAutorLibro.Orden;
                await _parcial1aContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("eliminar/{autorId}/{libroId}")]
        public async Task<IActionResult> Eliminarr(int autorId, int libroId)
        {
            try
            {
                AutorLibro? libroactual = await _parcial1aContext.AutorLibros
               .FirstOrDefaultAsync(al => al.Autorid == autorId && al.Libroid == libroId);

                if (libroactual == null)
                {
                    return NotFound();
                }
                _parcial1aContext.AutorLibros.Remove(libroactual);
                await _parcial1aContext.SaveChangesAsync();

                return Ok(libroactual);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
