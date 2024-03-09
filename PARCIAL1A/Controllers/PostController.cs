    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly Parcial1aContext _parcial1aContexto;
        public PostController(Parcial1aContext parcial1aContexto)
        {
            _parcial1aContexto = parcial1aContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Post> listadoPost = _parcial1aContexto.Posts.ToList();
            if (listadoPost.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPost);
        }

        [HttpGet]
        [Route("LisNombre/{nombre}")]
        public IActionResult Findlibro(string nombre)
        {
            var listaPost = (from e in _parcial1aContexto.Libros
                             join ae in _parcial1aContexto.AutorLibros
                                on e.Libroid equals ae.Libroid
                             join a in _parcial1aContexto.Autores
                                on ae.Autorid equals a.Autorid
                             join tot in _parcial1aContexto.Posts
                                on a.Autorid equals tot.Autorid
                             where a.Nombre.Contains(nombre)
                             select new
                             {
                                 tot.Autorid,
                                 tot.Titulo,
                                 tot.Contenido,
                                 tot.Fechapublicacion,
                                 a.Nombre,
                             }).OrderBy(result => result.Fechapublicacion).ToList();


            if (listaPost.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listaPost);
        }

        [HttpGet]
        [Route("LisLibro/{libro}")]
        public IActionResult buscarLibro(string libro)
        {
            var listaPost = (from e in _parcial1aContexto.Libros
                             join ae in _parcial1aContexto.AutorLibros
                                on e.Libroid equals ae.Libroid
                             join a in _parcial1aContexto.Autores
                                on ae.Autorid equals a.Autorid
                             join tot in _parcial1aContexto.Posts
                                on a.Autorid equals tot.Autorid
                             where tot.Titulo.Contains(libro)
                             select new
                             {
                                 ae.Id,
                                 tot.Titulo,
                                 tot.Contenido,
                                 tot.Fechapublicacion,
                                 a.Nombre,
                             }).OrderBy(result => result.Fechapublicacion).ToList();


            if (listaPost.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listaPost);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post(Post post)
        {
            try
            {
                _parcial1aContexto.Posts.Add(post);
                await _parcial1aContexto.SaveChangesAsync();

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("acrtuaizar")]

        public async Task<IActionResult> Put(int id, Post postactualizar)
        {
            var post = _parcial1aContexto.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Titulo = postactualizar.Titulo;
            post.Contenido = postactualizar.Contenido;
            post.Fechapublicacion = postactualizar.Fechapublicacion;
            post.Autorid = postactualizar.Autorid;

            _parcial1aContexto.Entry(post).State = EntityState.Modified;
            _parcial1aContexto.SaveChanges();

            return Ok(post);
        }
        private bool PostExists(int id)
        {
            return _parcial1aContexto.Posts.Any(e => e.Postid == id);
        }

        [HttpDelete]
        [Route("eliminar{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _parcial1aContexto.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _parcial1aContexto.Posts.Remove(post);
            await _parcial1aContexto.SaveChangesAsync();

            return NoContent();
        }

    }
}



        

       
        


    
