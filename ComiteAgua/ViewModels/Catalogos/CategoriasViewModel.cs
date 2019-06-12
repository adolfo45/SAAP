using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Catalogos
{
    public class CategoriasViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int CategoriaId { get; set; }
        [Display(Name = "Año")]
        public int? MesAnoFiltro { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Tarifa> Tarifas { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Categoria { get; set; }

        [Display(Name = "Abreviatura")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Abreviatura { get; set; }
        public int UsuarioAltaId { get; set; }
        public int UsuarioCambioId { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int? MesAno { get; set; }

        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string CostoTarifa { get; set; }
        public int TarifaId { get; set; }
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Categoría";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Categoría";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Cosultar Categoría";
                return "Error";
            } // get
        } // public string Titulo

        public string UrlRetorno { get; set; }

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class CategoriasViewModel
}//namespace ComiteAgua.ViewModels.Catalogos