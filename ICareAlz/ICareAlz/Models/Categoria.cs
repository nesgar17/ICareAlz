using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Categoria
    {

        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        public virtual ICollection<Solicitud> Solicitudes { get; set; }

        public virtual ICollection<Instituto> Institutos { get; set; }


    }
}