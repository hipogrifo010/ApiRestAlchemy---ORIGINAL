﻿using System.ComponentModel.DataAnnotations;


namespace ApiRestAlchemy.Models
{
    public class PeliculaDTOtoPost
    {

        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string FechaDeCreacion { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public int Calificacion { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public string PersonajesAsociados { get; set; }

        [Required(ErrorMessage = "el campo es requerido")]
        public int GenreId { get; set; }



    }
}
