using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class PropietarioPersonaMoralViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        public int PersonaId { get; set; }

        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string Nombre { get; set; }

        [Display(Name = "RFC")]
        public string Rfc { get; set; }

        [Display(Name = "Correo")]
        [RegularExpression("^[\\w]+([\\.\\-][\\w]+)*[@]([\\w]+[\\.\\-])+([\\w]+[\\.])?[\\w]{2,4}$", ErrorMessage = "El {0} no es válido.")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string CorreoElectronico {get; set;}
        public bool CambioPropietario { get; set; }
        public string DownloadTokenMoral { get; set; }
        public int PropietarioId { get; set; }
        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string CostoCambioPropietario { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class PropietarioPersonaMoralViewModel
}//namespace ComiteAgua.ViewModels.Tomas