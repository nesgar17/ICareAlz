using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class TestView
    {

        public int PreguntaId { get; set; }

        public string Nombre { get; set; }

        public int RespuestaId { get; set; }

        public string NombreRespuesta { get; set; }

        public int Valor { get; set; }

        public List<Respuesta> Respuestas { get; set; }
    }

   }