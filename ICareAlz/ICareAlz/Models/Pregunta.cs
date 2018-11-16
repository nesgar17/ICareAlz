using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Pregunta
    {
        [Key]
        public int PreguntaId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}