using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class EstadoCuentaViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public string NombrePropietario { get; set; }
        public string Direccion { get; set; }
        public string Categoria { get; set; }
        public string Folio { get; set; }
        public List<Tarifa> Tarifas { get; set; }
        public decimal Sumatoria { get; set; }
        public int Contador { get; set; }       

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class EstadoCuentaViewModel
}//namespace ComiteAgua.ViewModels.Tomas