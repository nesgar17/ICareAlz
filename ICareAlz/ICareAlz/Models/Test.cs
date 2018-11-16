using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Test
    {

        [Key]
        public int TestId { get; set; }

        public string Nombre { get; set; }

        public int Resultado { get; set; }

        public DateTime FechaCreacion { get; set; }


    }
}