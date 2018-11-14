

namespace ICareAlz.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Municipio
    {

        public int MunicipioId { get; set; }

        public int EstadoId { get; set; }

        public int Clave { get; set; }

        public string Nombre { get; set; }

        public string Abreviatura { get; set; }

        public bool Activo { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual ICollection<Localidad> Localidades { get; set; }

        public virtual ICollection<Administrador> Administradores { get; set; }

        public virtual ICollection<Solicitud> Solicitudes { get; set; }

        public virtual ICollection<Instituto> Institutos { get; set; }


    }
}