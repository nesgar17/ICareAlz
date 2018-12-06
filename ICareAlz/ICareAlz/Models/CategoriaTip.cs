namespace ICareAlz.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CategoriaTip
    {
        [Key]
        public int CategoriaTipId { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Categoria")]
        public string Nombre { get; set; }


        public virtual ICollection<Tip> Tips { get; set; }
    }
}