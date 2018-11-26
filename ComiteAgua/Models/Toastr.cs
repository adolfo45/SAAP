using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    [Serializable]
    public class Toastr
    {

        #region * Constructor generado por Adserti *

        public Toastr()
        {
            ToastMessages = new List<ToastMessage>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
            ProgressBar = true;

        } // public Toastr()

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        #endregion

        #region * Propiedades declaradas por Adserti *

        public bool ShowNewestOnTop { get; set; }

        public bool ShowCloseButton { get; set; }

        public bool ProgressBar { get; set; }

        public List<ToastMessage> ToastMessages { get; set; }

        #endregion

        #region * Acciones generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *

        public ToastMessage ShowToastMessage(string title, string message, ToastMessage.ToastType toastType)
        {
            var toast = new ToastMessage()
            {
                Title = title,
                Message = message,
                MessageType = toastType

            };

            ToastMessages.Add(toast);

            return toast;

        } // public ToastMessage ShowToastMessage(string title, string message, ToastMessage.ToastType toastType)

        #endregion

    } // public class Toastr

} // namespace ComiteAgua.Models