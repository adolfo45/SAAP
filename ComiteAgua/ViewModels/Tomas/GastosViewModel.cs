using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class GastosViewModel
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int GastoId { get; set; }
        public int SucursalId { get; set; }
        public int MultiComiteId { get; set; }
        public string UrlArchivo { get; set; }        

        [Display(Name = "Concepto")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(4000, ErrorMessage = "La {0} debe ser máximo de {1} caractéres.")]
        public string Concepto { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Total { get; set; }

        [Display(Name = "Archivo")]
        public List<ArchivosGastoViewModel> ArchivosGasto { get; set; }        
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class GastosViewModel

} // namespace ComiteAgua.ViewModels.Tomas
