using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComiteAgua.Models;
using System.ComponentModel.DataAnnotations;
using ComiteAgua.Models.Seguridad;

namespace ComiteAgua.ViewModels.Tomas
{
    public class ConveniosViewModel
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ConvenioId { get; set; }
        public int PropietarioId { get; set; }
        public string Propietario { get; set; }

        [Display(Name = "Concepto")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int ConceptoConvenioId { get; set; }
        public string ConceptoConvenio { get; set; }
        public int TomaId { get; set; }

        [Display(Name = "Estatus Convenio")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int EstatusConvenioId { get; set; }
        public string EstatusConvenios { get; set; }

        [Display(Name = "Periodo Pago")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int PeriodoPagoConvenioId { get; set; }
        public string PeriodoPago { get; set; }

        [Display(Name = "Autorizo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int PersonaId { get; set; }
        public string Persona { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha Fin")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string FechaFin { get; set; }

        [Display(Name = "Sub Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string SubTotal { get; set; }

        [Display(Name = "Descuento")]
        public string Descuento { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Total { get; set; }
        public string FechaTermino { get; set; }

        [Display(Name = "Periodo Inicio")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string PeriodoInicio { get; set; }

        [Display(Name = "Periodo Fin")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string PeriodoFin { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        public int? CategoriaId { get; set; }

        [Display(Name = "Primer Pago")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string PimerPago { get; set; }

        [Display(Name = "Pagos De")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string PagosDe { get; set; }
        public string RutaArchivo { get; set; }
        public bool BotonImprimirVisible { get; set; }

        [Display(Name = "No Tarjeta")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string NoTarjeta { get; set; }

        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public int Folio { get; set; }
        public string EstatusConvenio { get; set; }
        public string NombreCompleto { get; set; }


        public List<ConceptoConvenio> ConceptosConvenio { get; set; }
        //public List<EstatusConvenio> EstatusConvenio { get; set; }
        public List<PeriodoPagoConvenio> PeriodosPagoConvenio { get; set; }
        public List<PersonaSeguridad> PersonasSeguridad { get; set; }       

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class ConveniosViewModel

} // namespace ComiteAgua.ViewModels.Tomas