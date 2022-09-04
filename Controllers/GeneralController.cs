using Microsoft.AspNetCore.Mvc;
using ApiRestAlchemy.Database;
using ApiRestAlchemy.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiRestAlchemy.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {

        public List<Personaje> personajeList { get; set; }
        public dynamic ViewBag { get; }
        private DatabaseContext _context;

        public GeneralController(DatabaseContext context)
        {
            _context = context;
        }
  


        [HttpGet("/Listado/characters")]
        public async Task<ActionResult<IEnumerable<PersonajeDTO>>> ListadoPersonajes()
        {
            return await _context.Personajes
                .Select(x => PersonajeToDTO(x))
                .ToListAsync();
        }
        

        [HttpGet("/Busqueda/Characters")]

        public async Task<ActionResult<List<PersonajeDTO>>> SearchCharacters([FromQuery] string? name, int? age, int? movies)
        {

            var personajeQueryable = _context.Personajes.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                personajeQueryable = personajeQueryable.Where(x => x.Nombre.Contains(name));
            }
            if (age != null)
            {
                personajeQueryable = personajeQueryable.Where(x => x.Edad.Equals(age));
            }

            if (movies != null)
            {
                personajeQueryable = personajeQueryable.Where(x => x.MovieId.Equals(movies));

            }


            return await personajeQueryable
                .Select(x => PersonajeToDTO(x))
                .ToListAsync();
        }


        [HttpGet("/DetalleCharacter/{CharacterName}")]
        public ActionResult IndexCharacter(string CharacterName)
        {


            int peliculaId = _context.Personajes.Where(x => x.Nombre.Equals(CharacterName))
                                               .Select(x => x.MovieId).FirstOrDefault();


            var personajePelicula = _context.Personajes.Join(_context.PeliculasOseries, personaje => personaje.MovieId,
                                     pelicula => pelicula.MovieId, (personaje, pelicula) => new { pelicula, personaje })
                                    .Where(x => x.personaje.MovieId == peliculaId);


            return Ok(personajePelicula
                                       .Where(x => x.personaje.Nombre.Equals(CharacterName))
                                       .Select(x => new { x.personaje.Nombre,
                                                          x.personaje.Edad,
                                                          x.personaje.Imagen,
                                                          x.personaje.CharacterId,
                                                          x.personaje.Historia,
                                                          x.personaje.Peso,
                                                          x.personaje.MovieId,
                                                          x.personaje.PeliculaOserie.Titulo,
                                                          }));

        }



        [HttpGet("/Listado/movies")]
        public async Task<ActionResult<IEnumerable<PeliculaOserieDTO>>> ListadoDePeliculas()
        {
            return await _context.PeliculasOseries
                .Select(x => PeliculaOserieToDTO(x))
                .ToListAsync();
        }


        [HttpGet("/Busqueda/Movies")]
        public async Task<ActionResult<List<PeliculaOserieDTO>>> SearchMovies([FromQuery] string? name, string? order, int? genre)
        {

            var peliculaQueryable = _context.PeliculasOseries.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.Titulo.Contains(name));
            }

            if (!string.IsNullOrEmpty(order))
            {
                if (order == "ASC")
                {

                    peliculaQueryable = peliculaQueryable.OrderBy("FechaDeCreacion ascending");

                }
                if (order == "DESC")
                {

                    peliculaQueryable = peliculaQueryable.OrderBy("FechaDeCreacion descending");

                }
            }

            if (genre != null)
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.GenreId.Equals(genre));

            }


             return Ok(peliculaQueryable
                                      
                                       .Select(x => new {
                                           x.Titulo,
                                           x.GenreId,

                                           }));
        }


        [HttpGet("/DetalleMovie/{MovieName}")]
        public ActionResult Index(string MovieName)
        {

            int peliculaId = _context.PeliculasOseries.Where(x => x.Titulo.Equals(MovieName))
                                               .Select(x => x.MovieId).FirstOrDefault();


            var personajePelicula = _context.Personajes.Join(_context.PeliculasOseries, personaje => personaje.MovieId,
                                     pelicula => pelicula.MovieId, (personaje, pelicula) => new { pelicula, personaje })
                                    .Where(x => x.personaje.MovieId == peliculaId);


            return Ok(personajePelicula
                                       .Where(x => x.pelicula.MovieId.Equals(peliculaId))
                                       .Select(x => new {
                                           x.personaje.PeliculaOserie.Titulo,
                                           x.personaje.PeliculaOserie.MovieId,
                                           x.personaje.PeliculaOserie.Imagen,
                                           x.personaje.PeliculaOserie.FechaDeCreacion,
                                           x.personaje.PeliculaOserie.Calificacion,
                                           x.personaje.Nombre
                                         
                                         }));

        }


       

        private static PersonajeDTO PersonajeToDTO(Personaje todoItem) =>
        new PersonajeDTO
        {
          CharacterId=todoItem.CharacterId,
          Nombre = todoItem.Nombre,
          Imagen = todoItem.Imagen,

        };
        private static PeliculaOserieDTO PeliculaOserieToDTO(PeliculaOserie peliculaOserie) =>
        new PeliculaOserieDTO
        {
           Titulo= peliculaOserie.Titulo,
           Imagen= peliculaOserie.Imagen,
           FechaDeCreacion= peliculaOserie.FechaDeCreacion
        };
  

        private static GeneroDTO PeliculasPorGenero(Genero genero) =>
        new GeneroDTO
        {
           
            GenreId = genero.GenreId
        };
    }

}
