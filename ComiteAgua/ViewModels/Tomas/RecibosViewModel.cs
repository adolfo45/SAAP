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

        public int TomaId { get; set; }
        [Display(Name = "Concepto")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Concepto { get; set; }
        [Display(Name = "Renglon 1")]
        public string RenglonAdicional1 { get; set; }
        [Display(Name = "Renglon 2")]
        public string RenglonAdicional2 { get; set; }
        [Display(Name = "No Recibo")]
        public int? NoReciboFiltro { get; set; }
        [Display(Name = "Folio")]
        public int? FolioFiltro { get; set; }
        [Display(Name = "Fecha")]
        public DateTime? FechaFiltro { get; set; }
        [Display(Name = "Propietario")]
        public string PropietarioFiltro { get; set; }
        [Display(Name = "Calle")]
        public string CalleFiltro { get; set; }
        public int? ReciboId { get; set; }
        public List<Recibo> Recibos { get; set; }
        public string UrlOrigen { get; set; }
        public string Accion { get; set; }
        public string UrlRetorno { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Recibo";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Recibo";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Recibos Extra";
                return "Error";
            } // get
        } // public string Titulo
        [Display(Name = "Sub Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string SubTotal { get; set; }
        [Display(Name = "Descuento")]       
        public string Descuento { get; set; }
        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Total { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class RecibosViewModel
}//namespace ComiteAgua.ViewModels.Tomas