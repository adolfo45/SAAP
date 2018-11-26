using ComiteAgua.Models.Direcciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class DireccionesViewModel
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int DireccionId { get; set; }

        public int TomaId { get; set; }

        public int PropietarioId { get; set; }

        [Display(Name = "Calle")]
        //[Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(100, ErrorMessage = "La {0} debe ser máximo de {1} caractéres.")]
        public string Calle { get; set; }

        [Display(Name = "Num. Int.")]        
        [StringLength(70, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string NumInt { get; set; }

        [Display(Name = "Num. Ext.")]
        [StringLength(70, ErrorMessage = "El {0} debe ser máximo de {1} caractéres.")]
        public string NumExt { get; set; }

        [Display(Name = "Colonia")]
        //[Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(100, ErrorMessage = "La {0} debe ser máximo de {1} caractéres.")]
        public string Colonia { get; set; }

        [Display(Name = "Calle")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int? CalleId { get; set; }

        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int? ColoniaId { get; set; }

        [Display(Name = "Tipo Calle")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int? TipoCalleId { get; set; }
        public IList<Calles> Calles { get; set; }
        public IList<Colonias> Colonias { get; set; }
        public IList<TiposCalle> TiposCalle { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class DireccionesViewModel

} // namespace ComiteAgua.ViewModels.Tomas