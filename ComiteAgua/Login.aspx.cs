using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComiteAgua.Domain.Transacciones_Automaticas;
using System.Data.Entity;
using ComiteAgua.Controllers;
using ComiteAgua.Classes.Security;

namespace ComiteAgua
{
    public partial class Login : System.Web.UI.Page 
    {

        #region * Constructor generado por Comité Agua *

        public Login()
        {
            _context = new DataContext();
        } // public Login()

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;            

        } // protected void Page_Load(object sender, EventArgs e)     

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

        public void AceptarButton_Click(object sender, EventArgs e)
        {
            var usuariosDomain = new UsuariosDomain(_context);
            var transaccionesAutomaticasDomain = new TransaccionesAutomaticasDomain(_context);           

            var usurios = usuariosDomain.ObtenerUsuario(this.UsuarioTextBox.Value);

            if (usurios == null)
            {
                Session.Clear();
                Session.Abandon();
                UserSession.DestroyUserSession();

                this.UsuarioMessage.Visible = true;
                this.ContraseñaMessage.Visible = true;
            }
            else if (usurios.Password != this.ContraseñaTextBox.Value)
            {
                Session.Clear();
                Session.Abandon();
                UserSession.DestroyUserSession();

                this.ContraseñaMessage.Visible = true;
                this.UsuarioMessage.Visible = false;
            }
            else
            {
                // ejecuta transacciones automaticas
                var cambiarEstatusConvenio = transaccionesAutomaticasDomain.ObtenerTransaccion((int)TiposTransaccionesAutomaticasDomain.TiposTransaccionesAutomaticasDomainEnum.CambiarEstatusConvenio);

                if (cambiarEstatusConvenio == null)
                {
                    transaccionesAutomaticasDomain.CambiarEstatusConveniosVencido(usurios.UsuarioId);
                }

                if(usurios.UsuarioRol.Any(u => u.RolId != 4 ))
                {
                    UserSession.AddUserToSession(usurios.UsuarioId.ToString());
                   
                    Session["UsuarioId"] = usurios.UsuarioId;
                    Session["RolIds"] = usurios.UsuarioRol.Select(r => r.RolId).ToList();
                    Session["MultiComiteId"] = usurios.Persona.MultiComite.MultiComitesSucursales.Select(mc => mc.MultiComiteId).FirstOrDefault();
                    Session["SucursalId"] = usurios.Persona.MultiComite.MultiComitesSucursales.Select(mc => mc.SucursalId).FirstOrDefault();

                    Response.Redirect("~/home?bandera=" + true);
                    return;
                }               
            }

            Session.Clear();
            Session.Abandon();
            UserSession.DestroyUserSession();

            this.ContraseñaMessage.Visible = true;
            this.UsuarioMessage.Visible = true;

        } // public void AceptarButton_Click(object sender, EventArgs e)

    } // public partial class Login : System.Web.UI.Page

} // namespace ComiteAgua