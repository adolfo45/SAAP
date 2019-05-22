using ComiteAgua.Models.Recibos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class RecibosViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        [Display(Name = "No Recibo")]
        public int? NoReciboFiltro { get; set; }
        [Display(Name = "Folio")]
        public int? FolioFiltro { get; set; }
        [Display(Name = "Fecha")]
        public DateTime? FechaFiltro { get; set; }
        public int? ReciboId { get; set; }
        public List<Recibo> Recibos { get; set; }
        public string UrlOrigen { get; set; }

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class RecibosViewModel
}//namespace ComiteAgua.ViewModels.Tomas