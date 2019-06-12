using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Recibos
{
    public class Recibo
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ReciboId { get; set; }
        public int? PagoId { get; set; }        
        public Pago Pago { get; set; }
        public int? ConvenioId { get; set; }
        public Convenio Convenio { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoQRurl { get; set; }
        public int NoRecibo { get; set; }
        public string Observaciones { get; set; }
        public string Adicional { get; set; }
        public string CantidadLetra { get; set; }
        public string RenglonAdicional1 { get; set; }
        public string RenglonAdicional2 { get; set; }
        public string RenglonAdicional3 { get; set; }       
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public Usuario UsuarioAlta { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public Usuario UsuarioCambio { get; set; }
        public string Concepto { get; set; }
        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Recibo

} // namespace ComiteAgua.Models.Recibos