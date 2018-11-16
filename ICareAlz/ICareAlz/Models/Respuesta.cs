﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Respuesta
    {

        [Key]
        public int RespuestaId { get; set; }

        public string Nombre { get; set; }

        public int Valor { get; set; }

        public int PreguntaId { get; set; }

        public virtual Pregunta Pregunta { get; set; }

    }
}