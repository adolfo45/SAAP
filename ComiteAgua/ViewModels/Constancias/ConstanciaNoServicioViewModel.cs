using ComiteAgua.Models.Direcciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Constancias
{
    public class ConstanciaNoServicioViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        public int ConstanciaId { get; set; }
        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Propietario { get; set; }
        [Display(Name = "Tipo calle")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int TipoCalleId { get; set; }
        public string TipoCalle { get; set; }
        [Display(Name = "Calle")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int CalleId { get; set; }
        public string Calle { get; set; }
        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int ColoniaId { get; set; }
        public string Colonia { get; set; }
        [Display(Name = "No Int")]
        public string NoInt { get; set; }
        [Display(Name = "No Ext")]
        public string NoExt { get; set; }
        [Display(Name = "Fecha")]
        public string Fecha { get; set; }
        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Costo { get; set; }
        public string DownloadToken { get; set; }
        public string Accion { get; set; }
        public string UrlRetorno { get; set; }        
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Constancia de no servicio";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Constancia de no servicio";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Constancia de no servicio";
                return "Error";
            } // get
        } // public string Titulo
        public List<TiposCalle> TiposCalle { get; set; }
        public List<Calles> Calles { get; set; }
        public List<Colonias> Colonias { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class ConstanciaNoServicioViewModel
}//namespace ComiteAgua.ViewModels.Constancias