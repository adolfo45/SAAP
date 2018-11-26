using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class TabsViewModel
    {

        #region * Constructor generado por Comité Agua *

        public TabsViewModel()
        {
            PropietariosPersonaFisica = new PropietariosPersonaFisicaViewModel();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public string TabDireccionActivo { get; set; }
        public string TabTomaActivo { get; set; }
        public string TabPropietarioActivo { get; set; }
        public string TabPagosActivo { get; set; }
        public string TabPagosHabilitado { get; set; }
        public string TabDireccionHabilitado { get; set; }
        public string TabTomaHabilitado { get; set; }
        public string TabPeriodoPagos { get; set; }
        public PropietariosPersonaFisicaViewModel PropietariosPersonaFisica { get; set; }
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Toma";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Toma";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Cosultar Toma";
                return "Error";
            } // get
        } // public string Titulo

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class TabsViewModel

} // namespace ComiteAgua.ViewModels.Tomas