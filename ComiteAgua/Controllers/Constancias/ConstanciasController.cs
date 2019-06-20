using ComiteAgua.Domain;
using ComiteAgua.Domain.Recibos;
using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Filters.Security;
using ComiteAgua.Models;
using ComiteAgua.Models.Recibos;
using ComiteAgua.ViewModels.Constancias;
using ComiteAgua.ViewModels.Tomas;
using MessagingToolkit.QRCode.Codec;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsertiVS2017ClassLibrary;

namespace ComiteAgua.Controllers.Constancias
{
    [Authenticate]
    public class ConstanciasController : MessageControllerBase
    {
        #region * Constructor generado por Comité Agua *
        public ConstanciasController()
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

        public ActionResult ConstanciaNoAdeudoFiltros()
        {           
            var constanciasViewModel = new ConstanciasViewModel()
            {                
                Accion = ComiteAgua.Accion.Consultar,
            };

            return View("ConstanciaNoAdeudo",constanciasViewModel);
        }
        public ActionResult _ConstanciaNoAdeudoConsulta(int? folio, string propietario, string calle)
        {         
            ConstanciasDomain constanciasDomain = new ConstanciasDomain(_context);
            var tomasPreList = constanciasDomain.ObtenerTomas(folio, propietario,calle);

            var tomas = tomasPreList
                .Select(t => new TomasViewModel
                {
                    TomaId = t.TomaId,
                    Folio = t.Folio,
                    Propietario = t.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? t.Propietario.Persona.PersonaFisica.Nombre + " " +
                                  t.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                  t.Propietario.Persona.PersonaFisica.ApellidoMaterno : t.Propietario.Persona.PersonaMoral.Nombre,
                    Calle = t.Direccion != null ? ((t.Direccion.TipoCalleId > 0 ? t.Direccion.TiposCalle.Nombre : string.Empty) + ' ' + (t.Direccion.CalleId > 0 ? t.Direccion.Calles.Nombre : string.Empty) +
                                    (!string.IsNullOrEmpty(t.Direccion.NumInt) ? " INT " + t.Direccion.NumInt : string.Empty) +
                                    (!string.IsNullOrEmpty(t.Direccion.NumExt) ? " EXT " + t.Direccion.NumExt : string.Empty)) : String.Empty,
                    Colonia = t.Direccion != null ? (t.Direccion.ColoniaId > 0 ? t.Direccion.Colonias.Nombre : string.Empty) : string.Empty,
                    UltimoPeriodoPago = t.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault() != null ? 
                                        Convert.ToDateTime(t.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).ToString("MMM-yyyy", new CultureInfo("es-ES")) : t.PeriodoPago.Select(p => p.UltimoPeriodoPago).LastOrDefault(),
                    ReciboImpreso = t.Constancia.Count > 0 ? t.Constancia.Select(c => c.ReciboImpreso).LastOrDefault() : true
                })
                .OrderBy(t => t.Folio)
                .ToList();            

            return PartialView("_ConstanciaNoAdeudoConsulta", tomas);
        }
        public ActionResult ConstanciaNoServicio()
        {
            var direccionesDomain = new DireccionesDomain(_context);
            var tipoCalles = direccionesDomain.ObtenerTiposCalle().ToList();
            var calles = direccionesDomain.ObtenerCalles().ToList();
            var colonias = direccionesDomain.ObtenerColonias().ToList();
            var constanciaNoServicioVM = new ConstanciaNoServicioViewModel()
            {
                TiposCalle = tipoCalles,
                Calles = calles,
                Colonias = colonias,
                Accion = Accion.Consultar,
            };

            return View(constanciaNoServicioVM);
        }   
        public ActionResult DescargarConstanciaNoAdeudo(int tomaId, string costo, string downloadToken)
        {
            var constanciasDomain = new ConstanciasDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            int usuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString());

            var toma = tomasDomain.ObtenerToma(tomaId);            

            //Guarda Constancia
            var constancia = new Constancia()
            {
                TomaId = tomaId,
                UsuarioAltaId = usuarioAltaId,
                FechaAlta = DateTime.Now,
                ReciboImpreso = false,
                TipoConstanciaId = (int)TipoConstanciaDomain.TipoConstanciaEnum.ConstanciaNoAdeudo
            };
            constanciasDomain.GuardarConstancia(constancia);

            //Guarda Pago
            var pago = new Pago()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Constancia,
                TomaId = tomaId,
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(costo)),
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(costo)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = usuarioAltaId,
                Activo = true,
                ConstanciaId = constancia.ConstanciaId                
            };

            pagosDomain.Guardar(pago);
           
            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));           

            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     "CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {4}{0}" +
                                                     "CAJERO(A): {5}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     DateTime.Now.ToString("dd/MM/yyyy"),
                                                     toma.Folio,
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
                Adicional = "CONSTANCIA DE NO ADEUDO",
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                //RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                //RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
            };

            recibosDomain.Gurdar(recibo);

            //Descarga archivo
            var tomasVM = new TomasViewModel()
            {
                TomaId = toma.TomaId,
                Folio = toma.Folio,
                Propietario = toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                  toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                  toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : toma.Propietario.Persona.PersonaMoral.Nombre,
                Calle = toma.Direccion != null ? ((toma.Direccion.TipoCalleId > 0 ? toma.Direccion.TiposCalle.Nombre : string.Empty) + ' ' + (toma.Direccion.CalleId > 0 ? toma.Direccion.Calles.Nombre : string.Empty) +
                                    (!string.IsNullOrEmpty(toma.Direccion.NumInt) ? " INT " + toma.Direccion.NumInt : string.Empty) +
                                    (!string.IsNullOrEmpty(toma.Direccion.NumExt) ? " EXT " + toma.Direccion.NumExt : string.Empty)) : String.Empty,
                Colonia = toma.Direccion != null ? (toma.Direccion.ColoniaId > 0 ? toma.Direccion.Colonias.Nombre : string.Empty) : string.Empty,
                UltimoPeriodoPago = toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault() != null ?
                                    Convert.ToDateTime(toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault()).ToString("MMM-yyyy", new CultureInfo("es-ES")) :
                                    toma.PeriodoPago.Select(p => p.UltimoPeriodoPago).LastOrDefault(),
                ReciboImpreso = toma.Constancia != null ? toma.Constancia.Select(c => c.ReciboImpreso).LastOrDefault() : true,
            };
            // generar archivo word
            //if (model.ConvenioId == 0) { 
            string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/ConstanciaNoAdeudo.docx");
            string rutaArchivo = @"/UploadFiles/ConstanciasNoAdeudo/";
            string nombreArchivo = "Constancia no adeudo.docx";

            using (DocX document = DocX.Load(rutaFormato))
            {
                document.ReplaceText("{NombreUsuario}", tomasVM.Propietario, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Direccion}", "Calle: " + tomasVM.Calle + ", Col. " + tomasVM.Colonia, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Folio}", tomasVM.Folio.ToString(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //Si no existe el directorio se crea
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                } // if (!Directory.Exists(ruta))
                document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
            }

            var urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;

            // fin archivo word
            if (!string.IsNullOrEmpty(urlArchivo))
            {
                FileInfo archivo = new FileInfo(urlArchivo);

                if (archivo.Exists)
                {
                    Response.ClearContent();
                    Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                    Response.AddHeader("Content-Length", archivo.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.TransmitFile(archivo.FullName);
                    Response.End();
                }
            }
            
            return null;
        }   
        public ActionResult DescargarConstanciaNoServicio(ConstanciaNoServicioViewModel model)
        {
            var direccionesDomain = new DireccionesDomain(_context);
            var constanciasDomain = new ConstanciasDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);

            int usuarioId = Convert.ToInt32(Session["UsuarioId"].ToString());           
            model.TiposCalle = direccionesDomain.ObtenerTiposCalle().ToList();
            model.Calles = direccionesDomain.ObtenerCalles().ToList();
            model.Colonias = direccionesDomain.ObtenerColonias().ToList();

            //Guarda Constancia
            var constancia = new Constancia()
            {
                Propietario = AdsertiFunciones.FormatearTexto(model.Propietario),
                TipoCalleId = model.TipoCalleId,
                CalleId = model.CalleId,
                ColoniaId = model.ColoniaId,
                NoInt = model.NoInt,
                NoExt = model.NoExt,
                FechaLetra = this.ConvertirFecha(DateTime.Now.Day.ToString()).ToLower() + " días del mes de " +
                             //DateTime.Now.Month.ToString().ToLower() + " de " +
                             DateTime.Now.ToString("MMMMMMMM").ToLower() + " de " +
                             this.ConvertirFecha(DateTime.Now.Year.ToString()).ToLower(),
                UsuarioAltaId = usuarioId,
                FechaAlta = DateTime.Now,
                ReciboImpreso = false,
                TipoConstanciaId = (int)TipoConstanciaDomain.TipoConstanciaEnum.ConstanciaNoServicio
            };
            constanciasDomain.GuardarConstancia(constancia);
            model.Fecha = constancia.FechaLetra;
            //Guarda Pago
            var pago = new Pago()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Constancia,                
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Costo)),
                Total = constanciasDomain.Costo,
                FechaAlta = DateTime.Now,
                UsuarioAltaId = usuarioId,
                Activo = true,
                ConstanciaId = constancia.ConstanciaId
            };

            pagosDomain.Guardar(pago);

            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));

            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     //"CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {3}{0}" +
                                                     "CAJERO(A): {4}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     DateTime.Now.ToString("dd/MM/yyyy"),
                                                     //toma.Folio,
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
                Adicional = "CONSTANCIA DE NO SERVICO",
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                //RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                //RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
            };

            recibosDomain.Gurdar(recibo);

            //Descarga archivo .doc
            var tipoCalle = direccionesDomain.ObtenerTiposCalle()
                            .Where(x => x.TipoCalleId == model.TipoCalleId).FirstOrDefault().Nombre;
            var calle = direccionesDomain.ObtenerCalles()
                        .Where(x => x.CalleId == model.CalleId).FirstOrDefault().Nombre;
            var colonia = direccionesDomain.ObtenerColonias()
                        .Where(x => x.ColoniaId == model.ColoniaId).FirstOrDefault().Nombre;

            var ConstanciaNoServicioVM = new ConstanciaNoServicioViewModel()
            {
                Calle = tipoCalle + ' ' + calle +
                                    (!string.IsNullOrEmpty(model.NoInt) ? " No. Int. " + model.NoInt : string.Empty) +
                                    (!string.IsNullOrEmpty(model.NoExt) ? " No. Ext. " + model.NoExt : string.Empty),
                Colonia = colonia               
            };
           
            // generar archivo word
            //if (model.ConvenioId == 0) { 
            string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/ConstanciaNoServicio.docx");
            string rutaArchivo = @"/UploadFiles/ConstanciaNoServicio/";
            string nombreArchivo = "Constancia no servicio.docx";

            using (DocX document = DocX.Load(rutaFormato))
            {
                document.ReplaceText("{Propietario}", model.Propietario.ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Direccion}", "Calle " + ConstanciaNoServicioVM.Calle + ", Col " + ConstanciaNoServicioVM.Colonia, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Fecha}", model.Fecha, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //Si no existe el directorio se crea
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                } // if (!Directory.Exists(ruta))
                document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
            }

            var urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;

            // fin archivo word
            if (!string.IsNullOrEmpty(urlArchivo))
            {
                FileInfo archivo = new FileInfo(urlArchivo);

                if (archivo.Exists)
                {
                    Response.ClearContent();
                    Response.AppendCookie(new HttpCookie("fileDownloadToken", model.DownloadToken));
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                    Response.AddHeader("Content-Length", archivo.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.TransmitFile(archivo.FullName);
                    Response.End();
                }
            }
            
            return null;
        }   
        public ActionResult ImprimirReciboConstanciaNoAdeudo(int tomaId)
        {
            var constanciasDomain = new ConstanciasDomain(_context);
            int usuarioId = Convert.ToInt32(Session["UsuarioId"].ToString());

            var constancia = constanciasDomain.ObtenerConstanciaSinImprimir(tomaId);
            constanciasDomain.EditarConstanciaReciboImpreso(constancia.ConstanciaId, usuarioId);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + constancia.Pago.Select(r => r.Recibo.Select(re => re.ReciboId).FirstOrDefault()).FirstOrDefault());
            return null;
        }    
        public ActionResult ImprimirReciboConstanciaNoServicio(string propietario)
        {
            var constanciasDomain = new ConstanciasDomain(_context);
            int usuarioId = Convert.ToInt32(Session["UsuarioId"].ToString());

            var constancia = constanciasDomain.ObtenerConstancia(propietario);
            constanciasDomain.EditarConstanciaReciboImpreso(constancia.ConstanciaId, usuarioId);
            int reciboId = constancia.Pago.Select(r => r.Recibo.Select(re => re.ReciboId).FirstOrDefault()).FirstOrDefault();
            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + reciboId);
            return null;
        }
        
        #endregion

        #region * Métodos creados por Comité Agua *

        public string ConvertirFecha(string num)
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
            return res;
        }//public string ConvertirFecha(string num)
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
    }//public class ConstanciasController : MessageControllerBase
}//namespace ComiteAgua.Controllers.Constancias