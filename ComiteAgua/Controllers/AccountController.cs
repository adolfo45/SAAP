using ComiteAgua.Classes.Security;
using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Domain.Transacciones_Automaticas;
using ComiteAgua.Filters.Security;
using ComiteAgua.Models;
using ComiteAgua.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Controllers
{
    [Authorized]
    public class AccountController : MessageControllerBase
    {
       
        #region * Constructor generado por Comité Agua *
        public AccountController()
        {
            _context = new DataContext();
        }
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *
        // GET: Account
        public ActionResult Index()
        {            
            var authenticationVM = new AuthenticationViewModel();
            return View("Authentication", authenticationVM);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(AuthenticationViewModel model)
        {
            var usuariosDomain = new UsuariosDomain(_context);
            var transaccionesAutomaticasDomain = new TransaccionesAutomaticasDomain(_context);

            var usurios = usuariosDomain.ObtenerUsuario(model.UserName);

            if (usurios == null)
            {
                Session.Clear();
                Session.Abandon();
                UserSession.DestroyUserSession();

                ShowToastMessage("SAAP", "Usuario o contraseña incorrectos", ToastMessage.ToastType.Warning);
                return View("Authentication", model);
            }

            if (usurios.Password != model.Password)
            {
                Session.Clear();
                Session.Abandon();
                UserSession.DestroyUserSession();

                ShowToastMessage("SAAP", "Usuario o contraseña incorrectos", ToastMessage.ToastType.Warning);
                return View("Authentication", model);
            }

            if (usurios.UsuarioRol.All(u => u.RolId == 4 || u.RolId == 6))
            {
                ShowToastMessage("SAAP", "Usuario o contraseña incorrectos", ToastMessage.ToastType.Warning);
                return View("Authentication", model);
            }

            // ejecuta transacciones automaticas
            var cambiarEstatusConvenio = transaccionesAutomaticasDomain.ObtenerTransaccion((int)TiposTransaccionesAutomaticasDomain.TiposTransaccionesAutomaticasDomainEnum.CambiarEstatusConvenio);

            if (cambiarEstatusConvenio == null)
            {
                transaccionesAutomaticasDomain.CambiarEstatusConveniosVencido(usurios.UsuarioId);
            }

            //UserSession.AddUserToSession(usurios.UsuarioId.ToString());
            Session["NombreUsuario"] = usurios.Persona.Nombre + " " + usurios.Persona.ApellidoPaterno + " " + usurios.Persona.ApellidoMaterno;
            Session["UsuarioId"] = usurios.UsuarioId;
            Session["RolIds"] = usurios.UsuarioRol.Select(r => r.RolId).ToList();
            Session["MultiComiteId"] = usurios.Persona.MultiComite.MultiComitesSucursales.Select(mc => mc.MultiComiteId).FirstOrDefault();
            Session["SucursalId"] = usurios.Persona.MultiComite.MultiComitesSucursales.Select(mc => mc.SucursalId).FirstOrDefault();

            return RedirectToAction("Index","Home", new { bandera = true});
        }
        public void LogOut()
        {
            //TODO (Terminar implementacion de login)
            //if ((Session["UsuarioId"] != null))
            //{
            //    var usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            //    //var usuarioPerfilesDomain = new UsuarioPerfilesDomain(_context);

            //    //usuarioPerfilesDomain.FinalizarSesion(usuarioId);                

            //} // if ((Session["UsuarioId"] != null))

            Session.Clear();
            Session.Abandon();
            UserSession.DestroyUserSession();

            Response.Redirect("Index");
        } // public void LogOut()

        #endregion

        #region * Métodos creados por Comité Agua *
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        } // protected override void Dispose(bool disposing)
        #endregion
    }
}