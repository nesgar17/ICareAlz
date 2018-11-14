namespace ICareAlz.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    public class Localidad
    {
        public int LocalidadId { get; set; }

        public int MunicipioId { get; set; }

        public string Clave { get; set; }

        public string Nombre { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Altitud { get; set; }

        public string Carta { get; set; }

        public string Ambito { get; set; }

        public int Poblacion { get; set; }

        public int Femenino { get; set; }

        public int Masculino { get; set; }

        public int Viviendas { get; set; }

        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        public bool Activo { get; set; }


        public virtual Municipio Municipio { get; set; }


        public virtual ICollection<Administrador> Administradores { get; set; }

        public virtual ICollection<Solicitud> Solicitudes { get; set; }

        public virtual ICollection<Instituto> Institutos { get; set; }

    }
}