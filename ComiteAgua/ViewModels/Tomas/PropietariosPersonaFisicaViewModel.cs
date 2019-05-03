using ComiteAgua.Global;
using ComiteAgua.Models;
using ComiteAgua.ViewModels.Tomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class PropietariosPersonaFisicaViewModel
    {

        #region * Constructor generado por Comité Agua *

        public PropietariosPersonaFisicaViewModel()
        {
            EstadosCiviles = new List<EstadoCivil>();
            Toma = new TomasViewModel();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PropietarioId { get; set; }

        public int PersonaId { get; set; }       

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        //[Required(ErrorMessage = "La {0} es requerida.")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Estado Civil")]
        //[Required(ErrorMessage = "Ea {0} es requerido.")]
        public int? EstadoCivilId { get; set; }

        [Display(Name = "Teléfono")]
        [RegularExpression("^[\\d]{10}$", ErrorMessage = "El Teléfono no es válido.")]
        public string Telefono { get; set; }

        [Display(Name = "Correo")]
        [RegularExpression("^[\\w]+([\\.\\-][\\w]+)*[@]([\\w]+[\\.\\-])+([\\w]+[\\.])?[\\w]{2,4}$", ErrorMessage = "El {0} no es válido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "RFC")]
        [RegularExpression("^[a-zA-Z]{4}((\\d{2}((0[13578]|1[02])(0[1-9]|[12]\\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\\d|30)|02(0[1-9]|1\\d|2[0-8])))|([02468][048]|[13579][26])0229)([a-zA-Z\\d]{3})?$", ErrorMessage = "El {0} no tiene el formato correcto")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string Rfc { get; set; }
        public bool CambioPropietario { get; set; }
        [Display(Name = "Costo")]
        public string CostoCambioPropietario { get; set; }
        public string DownloadToken { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }       

        public List<EstadoCivil> EstadosCiviles { get; set; }
        public TomasViewModel Toma { get; set; }
        

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PropietariosViewModel

} // namespace ComiteAgua.ViewModels.Tomas