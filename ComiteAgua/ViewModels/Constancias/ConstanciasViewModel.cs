using ComiteAgua.ViewModels.Tomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Constancias
{
    public class ConstanciasViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua *  
        public int TomaId { get; set; }
        [Display(Name = "Folio")]
        public string FolioFiltro { get; set; }
        [Display(Name = "Propietario")]
        public string PropietarioFiltro { get; set; }
        [Display(Name = "Calle")]
        public string CalleFiltro { get; set; }
        public string Accion { get; set; }
        public string UrlRetorno { get; set; }
        public List<TomasViewModel> Tomas {get;set;}
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Constancia no adeudo";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Constancia no adeudo";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Constancia no adeudo";
                return "Error";
            } // get
        } // public string Titulo
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class ConstanciasViewModel
}//namespace ComiteAgua.ViewModels.Constancias