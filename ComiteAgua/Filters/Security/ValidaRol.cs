using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Filters.Security
{
    public class ValidaRol : AuthorizeAttribute
    {

        #region * Constructor generado por SAAP *

        public ValidaRol(params int[] rolId)
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

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["UsuarioId"] == null | httpContext.Session["RolIds"] == null)
                return false;

            foreach (var rolId in rolAutorizadoIds)
            {
                if (((List<int>)httpContext.Session["RolIds"]).Contains(Convert.ToInt32(rolId)))
                    return true;

            } // foreach (var rolId in rolAutorizadoIds)

            return false;

        } // protected override bool AuthorizeCore(HttpContextBase httpContext)    

        #endregion

    } // public class ValidaRol
} // namespace ComiteAgua.Filters.Security