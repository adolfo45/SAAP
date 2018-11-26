using ComiteAgua.Models;
using Novacode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class FormatosWordDomain
    {

        #region * Constructor generado por Comité Agua *

        public FormatosWordDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public PagosDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public string FormatoConvenio(int convenioId, string primerPago, string pagosDe)
        {            
            string rutaFormato = HttpContext.Current.Server.MapPath("~/Print/Convenios.docx"); 
            string rutaArchivo = @"/UploadFiles/Convenios/";
            string nombreArchivo = DateTime.Now.ToString("_ddMMyyyyHHmmss") + ".docx";          

            nombreArchivo = "Convenio" + nombreArchivo;
           
            using (DocX document = DocX.Load(rutaFormato))
            {
                ConveniosDomain conveniosDomain = new ConveniosDomain(_context);
                var convenio = conveniosDomain.ObtenerConvenio(convenioId);

                document.ReplaceText("{NombreRepresentante}", convenio.Persona.Nombre + " " + convenio.Persona.ApellidoPaterno + " " + convenio.Persona.ApellidoMaterno, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Cargo}", convenio.Persona.Cargo, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{NombreUsuario}", convenio.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                     convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " + 
                                     convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno
                                     ,false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Direccion}", convenio.Toma.Direccion.Calle + (!string.IsNullOrEmpty(convenio.Toma.Direccion.NumExt) ?
                                     " NO EXT " + convenio.Toma.Direccion.NumExt : string.Empty) + (!string.IsNullOrEmpty(convenio.Toma.Direccion.NumInt) ?
                                     " NO INT " + convenio.Toma.Direccion.NumInt : string.Empty) , false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{AdeudoAñoInicio}", Convert.ToDateTime(convenio.PeriodoInicio).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{AdeudoAñoFin}", Convert.ToDateTime(convenio.PeriodoFin).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Total}", convenio.Total.ToString("C"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{DiaInicio}", convenio.FechaInicio.ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{MesInicio}", convenio.FechaInicio.ToString("MMMMMMMMMM").ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{AñoInicio}", convenio.FechaInicio.ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{DiaFin}", convenio.FechaFin.ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{MesFin}", convenio.FechaFin.ToString("MMMMMMMMMM").ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{AñoFin}", convenio.FechaFin.ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{PrimerPago}", primerPago, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{PagosDe}", pagosDe, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Periodos}", convenio.PeriodoPagoConvenio.Nombre, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{DiaHoy}", DateTime.Now.ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{MesHoy}", DateTime.Now.ToString("MMMMMMMMMM").ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{AñoHoy}", DateTime.Now.ToString("yyyy").ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //Si no existe el directorio se crea
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(rutaArchivo)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(rutaArchivo));
                } // if (!Directory.Exists(ruta))
                document.SaveAs(HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
            }

            return HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;
        } // public static string FormatoCartaCompromisoCliente(string clienteId, string multiEmpresaId)

        #endregion

    } // public class FormatosWordDomain

} // namespace ComiteAgua.Domain