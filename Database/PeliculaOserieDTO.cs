using System.ComponentModel.DataAnnotations;


namespace ApiRestAlchemy.Models
{
    public class PeliculaOserieDTO
    {
        [Required(ErrorMessage = "el campo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string FechaDeCreacion { get; set; }

        public string Genero { get; set; }

    }
}
