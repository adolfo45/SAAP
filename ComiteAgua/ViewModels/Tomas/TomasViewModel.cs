using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ComiteAgua.Controllers.Tomas;

namespace ComiteAgua.ViewModels.Tomas 
{
    public class TomasViewModel
    {

        #region * Constructor generado por Comité Agua *

        public TomasViewModel()
        {
            Categorias = new List<Categoria>();
            Direccion = new DireccionesViewModel();
            Convenio = new ConveniosViewModel();
            Pagos = new PagosViewModel();
            PeriodoPago = new PeriodosPagoViewModel();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int TomaId { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int CategoriaId { get; set; }

        public int PropietarioId { get; set; }

        public int? DireccionId { get; set; }

        [Display(Name = "Liquidación")]
        //[Required(ErrorMessage = "La {0} es requerida.")]
        public int LiquidacionId { get; set; }

        [Display(Name = "Folio")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int? Folio { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(255, ErrorMessage = "La {0} debe ser máximo de {1} caractéres.")]
        public string Observaciones { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        [Display(Name = "Inactiva")]
        public bool Pasiva { get; set; }

        //DataTable
        public string Propietario { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Categoria { get; set; }
        public string UltimoPeriodoPago { get; set; }
        public int ConceptoPagoId { get; set; }
        public bool ConvenioActivo { get; set; }
        public bool ConvenioVencido { get; set; }
        public bool Notificacion { get; set; }
        public bool ReciboImpreso { get; set; }
        public string Tesorero { get; set; }

        public List<Categoria> Categorias { get; set; }
        public DireccionesViewModel Direccion { get; set; }
        public List<LiquidacionToma> LiquidacionesToma { get; set; }
        public ConveniosViewModel Convenio { get; set; }
        public PagosViewModel Pagos { get; set; }
        public PeriodosPagoViewModel PeriodoPago { get; set; }       

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *



        #endregion

    } //  public class TomasViewModel

} // namespace ComiteAgua.ViewModels.Tomas