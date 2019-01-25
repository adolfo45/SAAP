using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class ConveniosAdministracionViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public List<ConveniosViewModel> Convenios { get; set; }       
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                switch (Accion)
                {
                    case ComiteAgua.Accion.Agregar:
                        return "Agregar Convenio";
                    case ComiteAgua.Accion.Editar:
                        return "Editar Convenio";
                    case ComiteAgua.Accion.Consultar:
                        return "Consultar Convenio";
                }
                return "Error";
            } // get
        }

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    }//public class ConveniosAdministracionViewModel
}//namespace ComiteAgua.ViewModels.Tomas