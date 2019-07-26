using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class CorteViewModel
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public List<PagosViewModel> Pagos { get; set; }
        public List<GastosViewModel> Gastos { get; set; }
        public List<CorteExcelViewModel> CorteExcel { get; set; }
        public string TotalIngresos { get; set; }
        public string TotalGastos { get; set; }
        public string Total { get; set; }
        public DateTime FechaConsulta { get; set; }       

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } //  public class CorteViewModel

} // namespace ComiteAgua.ViewModels.Tomas