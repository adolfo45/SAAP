using ComiteAgua.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Models
{
    public class MessagesActionFilter : ActionFilterAttribute
    {

        #region * Constructor generado por Adserti *

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

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as MessageControllerBase;

            if (controller != null)
                controller.Toastr = (controller.TempData["Toastr"] as Toastr) ?? new Toastr();

            base.OnActionExecuting(filterContext);

        } // public override void OnActionExecuting(ActionExecutingContext filterContext)

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as MessageControllerBase;

            if (filterContext.Result.GetType() == typeof(ViewResult))
            {
                if (controller.Toastr != null && controller.Toastr.ToastMessages.Any())
                    controller.ViewData["Toastr"] = controller.Toastr;

            } // if (filterContext.Result.GetType() == typeof(ViewResult))

            else if (filterContext.Result.GetType() == typeof(RedirectToRouteResult))
            {
                if (controller.Toastr != null && controller.Toastr.ToastMessages.Any())
                    controller.TempData["Toastr"] = controller.Toastr;

            } // else if (filterContext.Result.GetType() == typeof(RedirectToRouteResult))

            base.OnActionExecuted(filterContext);

        } // public override void OnActionExecuted(ActionExecutedContext filterContext)

        #endregion

    } // public class MessagesActionFilter

} // namespace ComiteAgua.Models