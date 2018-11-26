using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Controllers
{
    public abstract class MessageControllerBase : Controller
    {

        #region * Constructor generado por Comité Agua *

        protected MessageControllerBase()
        {
           
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public Toastr Toastr { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public ToastMessage ShowToastMessage(string title, string message, ToastMessage.ToastType toastType)
        {

            return Toastr.ShowToastMessage(title, message, toastType);

        } // public ToastMessage ShowToastMessage(string title, string message, ToastMessage.ToastType toastType)

        #endregion

    } // public class MessageControllerBase : Controller

} // namespace ComiteAgua.Controllers