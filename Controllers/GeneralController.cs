using Microsoft.AspNetCore.Mvc;
using ApiRestAlchemy.Database;
using ApiRestAlchemy.Models;
using DB;
using System.Net;
using System.Security.Cryptography;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.Extensions.Logging;


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

            Nombre = todoItem.Nombre,
            Imagen = todoItem.Imagen,

        };

        private static PersonajeDTO PersonajeToDTO(Personaje todoItem) =>
        new PersonajeDTO
        {
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
    }

}
