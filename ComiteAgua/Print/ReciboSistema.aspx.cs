using ComiteAgua.Domain;
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
            this.ConvenioId.Value = this.Page.Request["convenioId"];                        

            this.InicializarPantalla();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(this.UrlOrigenHiddenField.Value))
            {
                Response.Redirect(this.UrlOrigenHiddenField.Value.Trim());
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
            var recibo = recibosDomain.ObtenerReciboImpresion(Convert.ToInt32(this.ReciboIdHiddenField.Value.Trim()));

            //Recibo Extra
            if (recibo.Pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Otro)
            {
                this.CanceladoText.Visible = !recibo.Pago.Activo;
                this.UsuarioTextBox.InnerText = recibo.Pago.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ?
                                                recibo.Pago.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : recibo.Pago.Toma.Propietario.Persona.PersonaMoral.Nombre;

                this.DireccionTextBox.InnerText = recibo.Pago.Toma.DireccionId > 0 ? (recibo.Pago.Toma.Direccion.CalleId > 0 ? recibo.Pago.Toma.Direccion.TiposCalle.Nombre + " " + recibo.Pago.Toma.Direccion.Calles.Nombre : recibo.Pago.Toma.Direccion.Calle) + "" + (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumExt) ? " EXT." + recibo.Pago.Toma.Direccion.NumExt : string.Empty) + "" +
                                                            (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumInt) ? " INT." + recibo.Pago.Toma.Direccion.NumInt : string.Empty) : string.Empty;

                this.ConceptoPagoTextBox.InnerText = recibo.Concepto;
                this.FechaTextBox.InnerText = recibo.FechaAlta.ToString("dd/MM/yyyy");
                this.FolioTextBox.InnerText = recibo.Pago.Toma.Folio.ToString();
                this.NoReciboTextBox.InnerText = recibo.NoRecibo.ToString();
               
                this.AdicionalTextBox.InnerText = recibo.Adicional;
                this.CantidadLetraTextBox.InnerText = recibo.CantidadLetra;
                this.TotalTextBox.InnerText = recibo.Pago.Total.ToString("C");
                this.SubTotalTextBox.InnerText = recibo.Pago.SubTotal.ToString("C");               
                this.RenglonAdicional1.InnerText = recibo.RenglonAdicional1;
                         
                if (recibo.Pago.Descuento != null)
                {
                    this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.Descuento).ToString("C");
                }               
                // Muestra codigo QR                                    
                CodigoImg.ImageUrl = "/UploadFiles/CodigosQR/" + recibo.CodigoQRurl;
                CodigoImg.Height = 100;
                CodigoImg.Width = 100;
                return;
            }
            //Recibo Renta
            if (recibo.Pago.RentaId != null)
            {
                this.UsuarioTextBox.InnerText = recibo.Pago.Renta.Nombre + " " + recibo.Pago.Renta.ApellidoPaterno + " " + recibo.Pago.Renta.ApellidoMaterno;
                this.DireccionTextBox.InnerText = recibo.Pago.Renta.Calle +
                                                        "" + (!string.IsNullOrEmpty(recibo.Pago.Renta.NoExt) ? " EXT." + recibo.Pago.Renta.NoExt : string.Empty) + "" +
                                                        (!string.IsNullOrEmpty(recibo.Pago.Renta.NoInt) ? " INT." + recibo.Pago.Renta.NoInt : string.Empty) +
                                                        " COL. " + recibo.Pago.Renta.Colonia + ", " + recibo.Pago.Renta.Municipio;
                this.ConceptoPagoTextBox.InnerText = recibo.Pago.ConceptoPago.Nombre;
                this.FechaTextBox.InnerText = recibo.FechaAlta.ToString("dd/MM/yyyy");
                this.NoReciboTextBox.InnerText = recibo.NoRecibo.ToString();
                this.AdicionalTextBox.InnerText = recibo.Adicional;
                this.RenglonAdicional1.InnerText = recibo.RenglonAdicional1;
                this.TotalTextBox.InnerText = recibo.Pago.Total.ToString("C");
                this.SubTotalTextBox.InnerText = recibo.Pago.SubTotal.ToString("C");
                this.CantidadLetraTextBox.InnerText = recibo.CantidadLetra;
                // Muestra codigo QR                                    
                CodigoImg.ImageUrl = "/UploadFiles/CodigosQR/" + recibo.CodigoQRurl;
                CodigoImg.Height = 100;
                CodigoImg.Width = 100;
                this.CanceladoText.Visible = !recibo.Pago.Activo;
                return;
            }//if (recibo.Pago.RentaId != null)

            //Recibo constancia
            if (recibo.Pago.ConstanciaId != null)
            {
                if (recibo.Pago.Constancia.TipoConstanciaId != (int)TipoConstanciaDomain.TipoConstanciaEnum.ConstanciaNoAdeudo)
                {               
                    this.UsuarioTextBox.InnerText = recibo.Pago.Constancia.Propietario;
                    this.DireccionTextBox.InnerText = recibo.Pago.Constancia.TiposCalle.Nombre + " " + recibo.Pago.Constancia.Calles.Nombre + 
                                                            "" + (!string.IsNullOrEmpty(recibo.Pago.Constancia.NoExt) ? " EXT." + recibo.Pago.Constancia.NoExt : string.Empty) + "" +
                                                            (!string.IsNullOrEmpty(recibo.Pago.Constancia.NoInt) ? " INT." + recibo.Pago.Constancia.NoInt : string.Empty);
                    this.ConceptoPagoTextBox.InnerText = recibo.Pago.ConceptoPago.Nombre;
                    this.FechaTextBox.InnerText = recibo.FechaAlta.ToString("dd/MM/yyyy");
                    this.NoReciboTextBox.InnerText = recibo.NoRecibo.ToString();
                    this.AdicionalTextBox.InnerText = recibo.Adicional;
                    this.TotalTextBox.InnerText = recibo.Pago.Total.ToString("C");
                    this.SubTotalTextBox.InnerText = recibo.Pago.SubTotal.ToString("C");
                    this.CantidadLetraTextBox.InnerText = recibo.CantidadLetra;
                    // Muestra codigo QR                                    
                    CodigoImg.ImageUrl = "/UploadFiles/CodigosQR/" + recibo.CodigoQRurl;
                    CodigoImg.Height = 100;
                    CodigoImg.Width = 100;
                    this.CanceladoText.Visible = !recibo.Pago.Activo;
                    return;
                }//if (recibo.Pago.Constancia.TipoConstanciaId != (int)TipoConstanciaDomain.TipoConstanciaEnum.ConstanciaNoAdeudo)
            }//if (recibo.Pago.ConstanciaId != null)

            //Recibo convenio
            if (!string.IsNullOrEmpty(this.ConvenioId.Value))
            {
                var conveniosDomain = new ConveniosDomain(_context);
                var reciboConvenio = recibosDomain.ObtenerReciboImpresion(Convert.ToInt32(this.ReciboIdHiddenField.Value));
                var convenioPagos = conveniosDomain.ObtenerPagosConvenio(Convert.ToInt32(this.ConvenioId.Value));
                var convenio = conveniosDomain.ObtenerConvenio(Convert.ToInt32(this.ConvenioId.Value));

                this.UsuarioTextBox.InnerText = convenio.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno;
                this.DireccionTextBox.InnerText = convenio.Toma.DireccionId > 0 ? (convenio.Toma.Direccion.CalleId > 0 ? convenio.Toma.Direccion.TiposCalle.Nombre + " " + convenio.Toma.Direccion.Calles.Nombre : convenio.Toma.Direccion.Calle) + "" + (!string.IsNullOrEmpty(convenio.Toma.Direccion.NumExt) ? " EXT." + convenio.Toma.Direccion.NumExt : string.Empty) + "" +
                                                                                     (!string.IsNullOrEmpty(convenio.Toma.Direccion.NumInt) ? " INT." + convenio.Toma.Direccion.NumInt : string.Empty) : string.Empty;
                this.ConceptoPagoTextBox.InnerText = "PAGO POR CONVENIO";
                this.FechaTextBox.InnerText = recibo.FechaAlta.ToString("dd/MM/yyyy");
                this.FolioTextBox.InnerText = convenio.Toma.Folio.ToString();
                this.NoReciboTextBox.InnerText = recibo.NoRecibo.ToString();
                this.CantidadLetraTextBox.InnerText = recibo.CantidadLetra;
                this.TotalTextBox.InnerText = convenioPagos.Sum(c => c.Total).ToString("C");
                this.SubTotalTextBox.InnerText = convenioPagos.Sum(c => c.Total).ToString("C");
                // Muestra codigo QR                                    
                CodigoImg.ImageUrl = "/UploadFiles/CodigosQR/" + recibo.CodigoQRurl;
                CodigoImg.Height = 100;
                CodigoImg.Width = 100;
                this.CanceladoText.Visible = !recibo.Pago.Activo;
                return;
            }//if (!string.IsNullOrEmpty(this.ConvenioId.Value))


            this.CanceladoText.Visible = !recibo.Pago.Activo;
            this.UsuarioTextBox.InnerText = recibo.Pago.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ?
                                            recibo.Pago.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                            recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                            recibo.Pago.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : recibo.Pago.Toma.Propietario.Persona.PersonaMoral.Nombre;

            this.DireccionTextBox.InnerText = recibo.Pago.Toma.DireccionId > 0 ? (recibo.Pago.Toma.Direccion.CalleId > 0 ? recibo.Pago.Toma.Direccion.TiposCalle.Nombre + " " + recibo.Pago.Toma.Direccion.Calles.Nombre : recibo.Pago.Toma.Direccion.Calle) + "" + (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumExt) ? " EXT." + recibo.Pago.Toma.Direccion.NumExt : string.Empty) + "" +
                                                        (!string.IsNullOrEmpty(recibo.Pago.Toma.Direccion.NumInt) ? " INT." + recibo.Pago.Toma.Direccion.NumInt : string.Empty) : string.Empty;

            this.ConceptoPagoTextBox.InnerText = recibo.Pago.ConceptoPago.Nombre;
            this.FechaTextBox.InnerText = recibo.FechaAlta.ToString("dd/MM/yyyy");
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

            //Suma descuentos 
            decimal descuentoProntoPago = recibo.Pago.DescuentProntoPago != null ? Convert.ToDecimal(recibo.Pago.DescuentProntoPago) : 0;
            decimal descuentoMadreSoltera = recibo.Pago.DescuentoMadreSoltera != null ? Convert.ToDecimal(recibo.Pago.DescuentoMadreSoltera) : 0;
            decimal descuentoTerceraEdad = recibo.Pago.DescuentoTerceraEdad != null ? Convert.ToDecimal(recibo.Pago.DescuentoTerceraEdad) : 0;
            decimal descuento = recibo.Pago.Descuento != null ? Convert.ToDecimal(recibo.Pago.Descuento) : 0;

            decimal totalDescuentoPersona = descuentoProntoPago + descuentoMadreSoltera + descuentoTerceraEdad;

            this.DescuentoTextBox.InnerText = totalDescuentoPersona > 0 ? totalDescuentoPersona.ToString("C") :
                descuento > 0 ? descuento.ToString("C") : string.Empty;
            this.OtroTextBox.InnerText = totalDescuentoPersona > 0 ? descuento > 0 ? descuento.ToString("C") : string.Empty : string.Empty; 

            //if (recibo.Pago.DescuentProntoPago != null && recibo.Pago.Descuento != null)
            //{
            //    this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.DescuentProntoPago).ToString("C");
            //    this.OtroTextBox.InnerText = Convert.ToDecimal(recibo.Pago.Descuento).ToString("C");
            //}else if (recibo.Pago.DescuentProntoPago != null)
            //{
            //    this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.DescuentProntoPago).ToString("C");
            //}else if (recibo.Pago.Descuento != null)
            //{
            //    this.DescuentoTextBox.InnerText = Convert.ToDecimal(recibo.Pago.Descuento).ToString("C");
            //}

            if (recibo.Pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
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