using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComiteAgua.Filters.Security
{
    public class PermissionAttribute : ActionFilterAttribute
    {

        #region * Constructor generado por SAAP *

        public PermissionAttribute(params int[] rolId)
        {
            rolAutorizadoIds = rolId;
        }

        #endregion

        #region * Enumeraciones declaradas por SAAP *

        #endregion

        #region * Variables declaradas por SAAP *

        private readonly int[] rolAutorizadoIds;

        #endregion

        #region * Propiedades declaradas por SAAP * 

        #endregion

        #region * Acciones generados por SAAP *

        #endregion

        #region * Métodos creados por SAAP *             

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (((List<int>)filterContext.HttpContext.Session["RolIds"]).Except(rolAutorizadoIds).Any())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "home"
                }));
            }
        }        

        #endregion

    } // public class PermissionAttribute : ActionFilterAttribute
} // namespace ComiteAgua.Filters.Security