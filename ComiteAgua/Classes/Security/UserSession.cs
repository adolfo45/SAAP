using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ComiteAgua.Classes.Security
{
    public class UserSession
    {

        #region * Constructor generado por SAAP *

        #endregion

        #region * Enumeraciones declaradas por SAAP *

        #endregion

        #region * Variables declaradas por SAAP *

        #endregion

        #region * Propiedades declaradas por SAAP * 

        #endregion

        #region * Acciones generados por SAAP *

        #endregion

        #region * Métodos creados por SAAP *

        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        } // public static bool ExistUserInSession()

        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        } // public static void DestroyUserSession()

        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        } // public static int GetUser()

        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);            

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        } // public static void AddUserToSession(string id)


        #endregion

    } // public class UserSession
} // namespace ComiteAgua.Classes.Security