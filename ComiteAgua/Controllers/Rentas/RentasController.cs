using ComiteAgua.Domain.Rentas;
using ComiteAgua.Models;
using ComiteAgua.Models.Rentas;
using ComiteAgua.ViewModels.Rentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsertiVS2017ClassLibrary;
using ComiteAgua.Domain;
using ComiteAgua.Domain.Recibos;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using ComiteAgua.Models.Recibos;
using ComiteAgua.Domain.Seguridad;

namespace ComiteAgua.Controllers.Rentas
{
    public class RentasController : MessageControllerBase
    {

        #region * Constructor generado por Comité Agua *
        public RentasController()
        {
            _context = new DataContext();
        }//public RentasController()
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        public ActionResult Renta()
        {
            var RentasVM = new RentasViewModel()
            {
                Accion = Accion.Agregar
            };
            return View("Rentas",RentasVM);
        }//public ActionResult RentaRetroexcavadora()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(RentasViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {              
                return View("Rentas", viewModel);
            }

            var rentasDomain = new RentasDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);

            int usuarioId = Convert.ToInt32(Session["UsuarioId"].ToString());
            DateTime fecha = DateTime.Now;

            //Guarda Renta
            var renta = new Renta()
            {
                TipoRentaId = (int)TiposRentaDomain.TiposRentaEnum.MaquinaRetroexcavadora,
                Nombre = AdsertiFunciones.FormatearTexto(viewModel.Nombre),
                ApellidoPaterno = AdsertiFunciones.FormatearTexto(viewModel.ApellidoPaterno),
                ApellidoMaterno = !string.IsNullOrEmpty(viewModel.ApellidoMaterno) ? 
                                    AdsertiFunciones.FormatearTexto(viewModel.ApellidoMaterno) : 
                                    string.Empty,
                Calle = AdsertiFunciones.FormatearTexto(viewModel.Calle),
                Colonia = AdsertiFunciones.FormatearTexto(viewModel.Colonia),
                Municipio = AdsertiFunciones.FormatearTexto(viewModel.Municipio),
                NoInt = viewModel.NoInt,
                NoExt = viewModel.NoExt,
                Costo = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(viewModel.Costo)),
                FechaAlta = fecha,
                UsuarioAltaId = usuarioId                
            };
            rentasDomain.Guardar(renta);

            //Guarda Pago
            var pago = new Pago()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Renta,                
                SubTotal = renta.Costo,
                Total = renta.Costo,
                FechaAlta = fecha,
                UsuarioAltaId = usuarioId,
                Activo = true,
                RentaId = renta.RentaId
            };

            pagosDomain.Guardar(pago);

            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));

            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +                                                     
                                                     "CANTIDAD PAGADA: {3}{0}" +
                                                     "CAJERO(A): {4}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     DateTime.Now.ToString("dd/MM/yyyy"),                                                     
                                                     pago.Total.ToString("C"),
                                                     usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
            //Genera codigoQR
            var QQRurl = this.GenerarCodigoQR(informacionRecibo);

            var recibo = new Recibo()
            {
                PagoId = pago.PagoId,
                Fecha = DateTime.Now,
                CodigoQRurl = QQRurl,
                NoRecibo = noRecibo,
                //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                Adicional = "RENTA RETROEXCAVADORA",
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                //RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                //RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
            };

            recibosDomain.Gurdar(recibo);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId + "&RentaId=" + renta.RentaId);

            return View();

        }//public ActionResult Guardar(RentasViewModel model)

        #endregion

        #region * Métodos creados por Comité Agua *

        protected string Convertir(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res + " PESOS 00/100 M.N";
        }//protected string Convertir(string num)
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }//protected override void Dispose(bool disposing)     
        public string GenerarCodigoQR(string informacionPago)
        {

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(informacionPago);
            System.Drawing.Image QR = (System.Drawing.Image)img;

            MemoryStream ms = new MemoryStream();

            QR.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = ms.ToArray();

            string rutaArchivo = @"/UploadFiles/CodigosQR/";
            string nombreArchivo = DateTime.Now.ToString("_ddMMyyyyHHmmss") + ".png";

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
            } // if (!Directory.Exists(ruta))

            img.Save(Server.MapPath(rutaArchivo) + nombreArchivo, ImageFormat.Png);

            string codigoQRurl = nombreArchivo;

            return codigoQRurl;
        }//public string GenerarCodigoQR(string informacionPago)
        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UN";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }//private string toText(double value)

        #endregion

    }//public class RentasController : MessageControllerBase
}//namespace ComiteAgua.Controllers.Rentas