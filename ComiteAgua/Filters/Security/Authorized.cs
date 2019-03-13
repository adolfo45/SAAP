using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Filters.Security
{
    public class Authorized : ActionFilterAttribute
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string macAddresses = "";

            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            
        }
        #endregion
    }//public class Authorized : ActionFilterAttribute
}//namespace ComiteAgua.Filters.Security