using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Convenio
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ConvenioId { get; set; }
        public int ConceptoConvenioId { get; set; }
        public ConceptoConvenio ConceptoConvenio { get; set; }
        public int? PersonaId { get; set; }
        public PersonaSeguridad Persona { get; set; }
        public int TomaId { get; set; }
        public Toma Toma { get; set; }
        public int EstatusConvenioId { get; set; }
        public EstatusConvenio EstatusConvenio { get; set; }
        public int PeriodoPagoConvenioId { get; set; }
        public PeriodoPagoConvenio PeriodoPagoConvenio { get; set; }
        public DateTime? PeriodoInicio { get; set; }
        public DateTime? PeriodoFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string Observaciones { get; set; }
        public decimal? PimerPago { get; set; }
        public decimal? Pagos { get; set; }
        public string RutaArchivo { get; set; }
        public string NoTarjeta { get; set; }
        public bool? Eliminado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        public List<Pago> Pago { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Convenio

} // namespace ComiteAgua.Models