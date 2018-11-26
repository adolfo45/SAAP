using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    [Serializable]
    public class ToastMessage
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum ToastType
        {
            Error = 0,
            Info = 1,
            Success = 2,
            Warning = 3

        } // public enum ToastType

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public string Title { get; set; }

        public string Message { get; set; }

        public ToastType MessageType { get; set; }

        public bool IsSticky { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class ToastMessage

} // namespace ComiteAgua.Models