using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class ArchivosGastoViewModel
    {

        #region * Constructor generado por Comité Agua *

        public ArchivosGastoViewModel()
        {            
            Gastos = new GastosViewModel();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ArchivoGastoId { get; set; }
        public int GastoId { get; set; }
        public string Nombre { get; set; }

        GastosViewModel Gastos { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class ArchivosGastoViewModel

} // namespace ComiteAgua.ViewModels.Tomas