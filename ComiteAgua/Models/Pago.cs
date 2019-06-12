using ComiteAgua.Models.Notificaciones;
using ComiteAgua.Models.Recibos;
using ComiteAgua.Models.Rentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Pago
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PagoId { get; set; }
        public int ConceptoPagoId { get; set; }
        public ConceptoPago ConceptoPago { get; set; }
        public int? TomaId { get; set; }
        public Toma Toma { get; set; }
        public int? ConvenioId { get; set; }
        public Convenio Convenio { get;set;}
        public int? ConstanciaId { get; set; }
        public Constancia Constancia { get; set; }
        public int? RentaId { get; set; }
        public Renta Renta { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? DescuentProntoPago { get; set; }
        public decimal Total { get; set; }
        public decimal? Abono { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public int? NotificacionId { get; set; }
        public Notificacion Notificaciones { get; set; }
        public string NoRecibo { get; set; }
        public int? DescuentoId { get; set; }
        public DescuentoProntoPago DescuentoProntoPago { get; set; }
        public decimal? CostoToma { get; set; }
        public decimal? DescuentoMadreSoltera { get; set; }
        public decimal? DescuentoTerceraEdad { get; set; }

        public List<PeriodoPago> PeriodoPago { get; set; }
        public List<Recibo> Recibo { get; set; }
        public List<DescuentoPago> DescuentoPago { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    }//public class Pago
}//namespace ComiteAgua.Models