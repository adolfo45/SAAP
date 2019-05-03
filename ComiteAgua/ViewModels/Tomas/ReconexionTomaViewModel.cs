using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class ReconexionTomaViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        [Required]
        public int TomaId { get; set; }
        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Costo { get; set; }
        [MaxLength(47)]
        public string Observaciones { get; set; }
        public string Accion { get; set; }
        public string UrlRetorno { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Reconexión de toma";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Reconexión de toma";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Consultar Reconexión de toma";
                return "Error";
            } // get
        } // public string Titulo
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class ReconexionTomaViewModel
}//namespace ComiteAgua.ViewModels.Tomas