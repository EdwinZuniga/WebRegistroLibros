using System.ComponentModel.DataAnnotations;

namespace WebRegistroLibros.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser almenos {2} y maximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; } 

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50, ErrorMessage = "El {0} debe ser almenos {2} y maximo {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = "El Autor es obligatorio")]

        public string Autor { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        public int Precio { get; set; }
    }
}
