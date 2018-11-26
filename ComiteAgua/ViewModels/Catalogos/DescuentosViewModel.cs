using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ComiteAgua.Models;

namespace ComiteAgua.ViewModels.Catalogos
{
    public class DescuentosViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int DescuentoId { get; set; }
        [Display(Name = "Mes Año")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string MesAno { get; set; }
        [Display(Name = "Porcentaje")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string MontoPoncentaje { get; set; }
        public string UrlRetorno { get; set; }
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Descuento";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Descuento";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Cosultar Descuento";
                return "Error";
            } // get
        } //public string Titulo

        public List<DescuentoProntoPago> Descuentos { get; set; }

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class DescuentosViewModel
}//namespace ComiteAgua.ViewModels.Catalogos