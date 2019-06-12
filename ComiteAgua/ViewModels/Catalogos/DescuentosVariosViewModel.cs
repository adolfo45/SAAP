using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Catalogos
{
    public class DescuentosVariosViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        public int DescuentoVariosId { get; set; }
        public int TipoDescuentoId { get; set; }
        public string Porcentaje { get; set; }
        public string TipoDescuento { get; set; }
        public string FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public string UrlRetorno { get; set; }
        public string Accion { get; set; }
        public List<Descuento> DescuentosVarios { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class DescuentosVariosViewModel
}//namespace ComiteAgua.ViewModels.Catalogos