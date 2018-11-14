using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Solicitud
    {
        [Key]
        public int SolicitudId { get; set; }

        public DateTime FechaEnvio { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre de la Institución")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Semblanza")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public int EstadoId { get; set; }

        public int MunicipioId { get; set; }

        public int LocalidadId { get; set; }


        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(210, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Representante")]
        public string Responsable { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        public string Status { get; set; }


        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaFundacion { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }


        public string FullAddress
        {
            get
            {
                return string.Format("{0},{1},{2},{3}"
                , Estado.Nombre
                , Municipio.Nombre
                , Localidad.Nombre
                , Direccion);
            }
        }

        public virtual Estado Estado { get; set; }

        public virtual Municipio Municipio { get; set; }

        public virtual Localidad Localidad { get; set; }

        public virtual Categoria  Categoria { get; set; }

    }
}