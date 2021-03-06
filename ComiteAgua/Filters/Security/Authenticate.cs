﻿using ComiteAgua.Classes.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComiteAgua.Filters.Security
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {

        // Si no estamos logeado, regresamos al login
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            if (filterContext.HttpContext.Session["UsuarioId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account");                
            }

            base.OnActionExecuting(filterContext);

        }

    } //public class AutheticateAttribute : ActionFilterAttribute
} //namespace ComiteAgua.Filters.Security