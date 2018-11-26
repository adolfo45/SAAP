using ComiteAgua.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComiteAgua
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region * Constructor generado por Adserti *

        public MvcApplication()
        {
            

        } // public MvcApplication()

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        

        #endregion

        #region * Propiedades declaradas por Adserti *

        #endregion

        #region * Acciones generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *

        protected void Application_Start()
        {           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            

        } // protected void Application_Start()       

        #endregion
    }
}
