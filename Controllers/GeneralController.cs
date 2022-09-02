using Microsoft.AspNetCore.Mvc;
using ApiRestAlchemy.Database;
using ApiRestAlchemy.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace ApiRestAlchemy.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {



        private DatabaseContext _context;

        public GeneralController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet("/characters")]
        public async Task<ActionResult<IEnumerable<PersonajeDTO>>> GetCharactersToDTO()
        {
            return await _context.Personajes
                .Select(x => PersonajeToDTO(x))
                .ToListAsync();
        }


        [HttpGet("Characters")]

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


        [HttpGet("Movies")]
        
        public async Task<ActionResult<List<PeliculaOserieDTO>>> SearchMovies([FromQuery] string? name, string? order,int? genre)
        {

            var personajeQueryable = _context.PeliculasOseries.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                personajeQueryable = personajeQueryable.Where(x=>x.Titulo.Contains(name));
            }

            if (!string.IsNullOrEmpty(order))
            {
                if (order == "ASC")
                {

                    personajeQueryable = personajeQueryable.OrderBy("Titulo ascending");

                }
                if (order == "DESC")
                {

                    personajeQueryable = personajeQueryable.OrderBy("Titulo descending");

                }
            }

            if (genre!=null)
            {
                    personajeQueryable = personajeQueryable.Where(x => x.GenreId.Equals(genre));

            }


            return await personajeQueryable
                .Select(x => PeliculaOserieToDTO(x))
                .ToListAsync();
        }
        

        [HttpGet("/movies")]
        public async Task<ActionResult<IEnumerable<PeliculaOserieDTO>>> GetPeliculasToDTO()
        {
            return await _context.PeliculasOseries
                .Select(x => PeliculaOserieToDTO(x))
                .ToListAsync();
        }
        
        private static PersonajeDTO ChNameDTO(Personaje todoItem) =>
        new PersonajeDTO
        {

            CharacterId = todoItem.CharacterId,
            Nombre = todoItem.Nombre,
            Imagen = todoItem.Imagen,

        };

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
