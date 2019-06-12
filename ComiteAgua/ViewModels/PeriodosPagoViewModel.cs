using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels
{
    public class PeriodosPagoViewModel
    {

        #region * Constructor generado por Comité Agua *
       

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *       

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PeriodoPagoId { get; set; }
        public int TomaId { get; set; }
        public int? PagoId { get; set; }
        public int? PropietarioId { get; set; }

        [Display(Name = "Periodo Inicio")]
        [Required(ErrorMessage = "El {0} es requerido.")]        
        public string MesAnoInicio { get; set; }

        [Display(Name = "Periodo Fin")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string MesAnoFin { get; set; }

        public string TotalPago { get; set; }
        public string ConceptoPago { get; set; }
        public string FechaPago { get; set; }

        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public bool Activo { get; set; }
        public int? NoRecibo { get; set; }
        public string TipoPago { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PeriodosPagoViewModel

} // namespace ComiteAgua.ViewModels