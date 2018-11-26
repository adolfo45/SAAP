﻿using ComiteAgua.Domain;
using ComiteAgua.Domain.Recibos;
using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComiteAgua.Print
{
    public partial class ReciboSistema : System.Web.UI.Page
    {                       

        #region * Constructor generado por Comité Agua *        

        public ReciboSistema()
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

        #region * Eventos generados por Comité Agua *

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            this.ReciboIdHiddenField.Value = this.Page.Request["reciboId"];
            this.UrlOrigenHiddenField.Value = this.Page.Request["UrlOrigen"];

            this.InicializarPantalla();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            if (this.UrlOrigenHiddenField.Value == "1")
            {
                Response.Redirect("~/tomas/Recibos?reciboId=" + this.ReciboIdHiddenField.Value);
            }
            else
            {
                Response.Redirect("~/home");
            }           
        }

        #endregion

        #region * Métodos creados por Comité Agua *

        public void InicializarPantalla()
        {
           
            var recibosDomain = new RecibosDomain(_context);            
           
            var recibo = recibosDomain.ObtenerReciboImpresion(Convert.ToInt32(this.ReciboIdHiddenField.Value));
            this.CanceladoText.Visible = !recibo.Pago.Activo;
            this.UsuarioTextBox.InnerText = recibo.Pago.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                            recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                            recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno;            

            this.DireccionTextBox.InnerText = recibo.Pago.Toma.DireccionId > 0 ? (recibo.Pago.Toma.Direccion.CalleId > 0 ? recibo.Pago.Toma.Direccion.TiposCalle.Nombre + " " + recibo.Pago.Toma.Direccion.Calles.Nombre : recibo.Pago.Toma.Direccion.Calle) + "" + (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumExt) ? " EXT." + recibo.Pago.Toma.Direccion.NumExt : string.Empty) + "" +
                                                        (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumInt) ? " INT." + recibo.Pago.Toma.Direccion.NumInt : string.Empty) : string.Empty;

            this.ConceptoPagoTextBox.InnerText = recibo.Pago.ConceptoPago.Nombre;
            this.FechaTextBox.InnerText = DateTime.Now.ToString("dd/MM/yyyy");
            this.FolioTextBox.InnerText = recibo.Pago.Toma.Folio.ToString();            
            this.NoReciboTextBox.InnerText = recibo.NoRecibo.ToString();
            this.ObservacionesTextBox.InnerText = recibo.Observaciones;
            this.AdicionalTextBox.InnerText = recibo.Adicional;
            this.CantidadLetraTextBox.InnerText = recibo.CantidadLetra;
            this.TotalTextBox.InnerText = recibo.Pago.Total.ToString("C");
            this.SubTotalTextBox.InnerText = recibo.Pago.SubTotal.ToString("C");
            this.PeriodoPagoTextBox.InnerText = recibo.Pago.PeriodoPago.Count > 0 ?
                Convert.ToDateTime(recibo.Pago.PeriodoPago.Select(p => p.MesAnoInicio).FirstOrDefault())
                    .ToString("MM-yyyy") + "/" + Convert
                    .ToDateTime(recibo.Pago.PeriodoPago.Select(p => p.MesAnoFin).FirstOrDefault()).ToString("MM-yyyy") : string.Empty;
            this.RenglonAdicional1.InnerText = recibo.RenglonAdicional1;
            this.RenglonAdicional2.InnerText = recibo.RenglonAdicional2;
            this.RenglonAdicional3.InnerText = recibo.RenglonAdicional3;

            if (recibo.Pago.DescuentProntoPago != null && recibo.Pago.Descuento != null)
            {
                this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.DescuentProntoPago).ToString("C");
                this.OtroTextBox.InnerText = Convert.ToDecimal(recibo.Pago.Descuento).ToString("C");
            }else if (recibo.Pago.DescuentProntoPago != null)
            {
                this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.DescuentProntoPago).ToString("C");
            }else if (recibo.Pago.Descuento != null)
            {
                this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.Descuento).ToString("C");
            }

            if(recibo.Pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
            {
                //this.OtroTextBox.InnerText = Convert.ToDecimal(recibo.Pago.CostoToma).ToString("C");
                this.TotalTextBox.InnerText = recibo.Pago.Total.ToString("C");
                this.SubTotalTextBox.InnerText = Convert.ToDecimal(recibo.Pago.CostoToma).ToString("C");
            }

            // Muestra codigo QR                                    
            CodigoImg.ImageUrl = "/UploadFiles/CodigosQR/" + recibo.CodigoQRurl;
            CodigoImg.Height = 100;
            CodigoImg.Width = 100;            
        }

        #endregion

    } // public partial class ReciboSistema : System.Web.UI.Page

} // namespace ComiteAgua.Print