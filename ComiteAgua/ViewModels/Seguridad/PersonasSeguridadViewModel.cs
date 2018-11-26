using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Seguridad
{
    public class PersonaSeguridadViewModel
    {

        #region * Constructor generado por Adserti *

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        #endregion

        #region * Propiedades declaradas por Adserti 

        public int PersonaId { get; set; }       
        public int UsuarioId { get; set; }        
        public int MultiEmpresaId { get; set; }
        public string Telefono { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100)]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Correo Electrónico")]
        [StringLength(100)]
        public string CorreoElectroico { get; set; }
       

        #endregion

        #region * Eventos generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *

        #endregion

    } // public class PersonasSeguridadViewModel
} // namespace ComiteAgua.ViewModels.Seguridad