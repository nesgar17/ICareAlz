﻿
namespace ICareAlz.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class Tip
    {
        [Key]
        public int TipId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Titulo del tip")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public int CategoriaTipId { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [Display(Name = "Imagen o Video")]
        public string Contenido { get; set; }

        [NotMapped]
        public HttpPostedFileBase ContenidoFile { get; set; }

        public int InstitutoId { get; set; }


        public virtual CategoriaTip CategoriaTip { get; set; }

        public virtual Instituto Instituto { get; set; }
    }
}