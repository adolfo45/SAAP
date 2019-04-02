using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AdsertiVS2017ClassLibrary;

namespace ComiteAgua.Domain.Seguridad
{
    public class SeguridadDomain
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

        public string LlenarTexto(string texto)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("MDSKJ"+texto);
            return Convert.ToBase64String(plainTextBytes);
        }
        public string LimpiarTexto(string texto)
        {
            var base64EncodedBytes = Convert.FromBase64String(texto);
            string cadena = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            string text = cadena.Remove(0, 5);
            string text1 = AdsertiFunciones.DesencriptarTexto(text);
            return text1;
        }

        #endregion
    }//public class SeguridadDomain
}//namespace ComiteAgua.Domain.Seguridad