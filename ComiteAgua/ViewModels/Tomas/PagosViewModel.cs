using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class PagosViewModel
    {

        #region * Constructor generado por Comité Agua *

        public PagosViewModel()
        {
            PeriodoPago = new PeriodosPagoViewModel();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PagoId { get; set; }
        public int PropietarioId { get; set; }
        public int ConceptoPagoId { get; set; }
        public int? PeriodoPagoId { get; set; }
        public string ConceptoPago { get; set; }
        public int TomaId { get; set; }
        public int? ConvenioId { get; set; }
        public string TipoPago { get; set; }

        [Display(Name = "Sub Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string SubTotal { get; set; }

        [Display(Name = "Descuento")]        
        public string Descuento { get; set; }
        [Display(Name = "Descuento Pronto Pago")]
        public string DescuentoProntoPago { get; set; }
        public int EstatusConvenioId { get; set; }
        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Total { get; set; }        
        public bool ReciboImpreso { get; set; }
        public string UrlRetorno { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Pago";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Pago";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Cosultar Pago";
                return "Error";
            } // get
        } // public string Titulo

        public string ResumenPagoConvenio { get; set; }
        public decimal? TotalPagosConvenio { get; set; }
        public decimal? TotalConvenio { get; set; }
        public decimal? RestanConvenio { get; set; }

        [Display(Name = "Abono")]
        public string Abono { get; set; }

        [Display(Name = "Abono Anterior")]
        public string AbonoAnterior { get; set; }

        [Display(Name = "Periodo Inicio")]
        public string MesAnoInicio { get; set; }

        [Display(Name = "Periodo Fin")]
        public string MesAnoFin { get; set; }

        public int? CategoriaId { get; set; }
        public bool Activo { get; set; }

        [Display(Name = "Folio Tarjeta")]
        public string FolioTarjeta { get; set; }

        [Display(Name = "Precio Toma")]
        public string PrecioToma { get; set; }

        public PeriodosPagoViewModel PeriodoPago { get; set; }

        // Tarjeta Convenio
        public string FechaAbono { get; set; }
        public string AbonoConvenio { get; set; }
        public string Saldo { get; set; }
        public string AdeudoTotal { get; set; }

        // Recibo
        [MaxLength(47)]
        public string Observaciones { get; set; }

        [MaxLength(47)]
        public string Adicional { get; set; }

        [MaxLength(47)]
        public string RenglonAdicional1 { get; set; }

        [MaxLength(47)]
        public string RenglonAdicional2 { get; set; }

        [MaxLength(47)]
        public string RenglonAdicional3 { get; set; }

        [MaxLength(47)]
        public string CantidadLetra { get; set; }
        public int? NoRecibo { get; set; }

        //Notificaciones
        public bool Notificada { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PagosViewModel

} // namespace ComiteAgua.ViewModels.Tomas