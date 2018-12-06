using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICareAlz.Models
{
    public class Usuario
    {

        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre(s)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        public int EstadoId { get; set; }

        public int MunicipioId { get; set; }

        public int LocalidadId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime Fechanan { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }

        [NotMapped]
        public HttpPostedFileBase FotoFile { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Correo Electronico")]
        [Index("Usuario_Correo_Index", IsUnique = true)]
        [StringLength(50, ErrorMessage = "The field {0} can maximun {1} and minimum {2} characters",
             MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [Compare("Password", ErrorMessage ="La contraseña no coincide")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmarPassword { get; set; }

        [Display(Name = "Dirección")]
        public string FullAddress
        {
            get
            {
                return $"{Estado.Nombre}{Municipio.Nombre}{Localidad.Nombre}{Direccion}";
            }
        }

        public virtual Estado Estado { get; set; }

        public virtual Municipio Municipio { get; set; }

        public virtual Localidad Localidad { get; set; }


    }
}