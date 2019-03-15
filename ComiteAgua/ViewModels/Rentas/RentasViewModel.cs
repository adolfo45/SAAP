using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Rentas
{
    public class RentasViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int RentaId { get; set; }
        public int TipoRentaId { get; set; }        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Display(Name = "Calle")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Calle { get; set; }
        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Colonia { get; set; }
        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Municipio { get; set; }
        [Display(Name = "No. Int")]
        public string NoInt { get; set; }
        [Display(Name = "No. Ext")]
        public string NoExt { get; set; }
        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Costo { get; set; }
        public string FechaAlta { get; set; }
        public string UsuarioAltaId { get; set; }
        public string FechaCambio { get; set; }
        public string UsuarioCambioId { get; set; }
        public string Accion { get; set; }
        public string UrlRetorno { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Renta Retroexcavadora";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Renta Retroexcavadora";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Consultar Renta Retroexcavadora";
                return "Error";
            } // get
        } // public string Titulo

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class ConstanciaRentaMaquinaViewModel
}//namespace ComiteAgua.ViewModels.Constancias