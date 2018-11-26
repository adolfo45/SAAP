using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComiteAgua.Models;
using ComiteAgua.Domain;

namespace ComiteAgua.Print
{
    public partial class Recibo : System.Web.UI.Page
    {        

        private static string tomaId;
        private static string totalAbono;

        public Recibo()
        {
            _context = new DataContext();
        }

        private readonly DataContext _context;

        protected void Page_Load(object sender, EventArgs e)
        {
            tomaId = this.Page.Request["TomaId"];
            totalAbono = this.Page.Request["TotalAbono"];

            this.InicializarPantalla();
        }

        public void InicializarPantalla()
        {
            var tomasDomain = new TomasDomain(_context);
            decimal total = 0;            

            var toma = tomasDomain.ObtenerToma(Convert.ToInt32(tomaId));
            var abono = !string.IsNullOrEmpty(totalAbono) ? Convert.ToDecimal(totalAbono) : 0;                       

            this.UsuarioTextBox.InnerText = toma.Propietario.Persona.PersonaFisica.Nombre + " " + toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " + toma.Propietario.Persona.PersonaFisica.ApellidoMaterno;
            this.DireccionTextBox.InnerText = toma.Direccion.Calle + (!string.IsNullOrEmpty(toma.Direccion.NumInt) ? " " + toma.Direccion.NumInt : string.Empty) + " " + (!string.IsNullOrEmpty(toma.Direccion.NumExt) ? " " + toma.Direccion.NumExt : string.Empty);
            this.ConceptoPagoTextBox.InnerText = toma.Pago.Select(x => x.ConceptoPago.Nombre).LastOrDefault();                        
           
            this.FechaTextBox.InnerText = DateTime.Now.ToString("dd-MM-yyyy");
            this.FolioTextBox.InnerText = toma.Folio.ToString();          

            this.DescuentoTextBox.InnerText = toma.Pago.Select(p => p.Descuento).LastOrDefault() != null ? Convert.ToDecimal(toma.Pago.Select(p => p.Descuento).LastOrDefault()).ToString("C") : "";

            if (toma.Pago.Select(c => c.ConceptoPagoId).LastOrDefault() == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Abono)
            {
                this.TotalTextBox.InnerText = toma.Pago.Select(p => p.Total).LastOrDefault().ToString("C");
                this.SubTotalTextBox.InnerText = toma.Pago.Select(p => p.Total).LastOrDefault().ToString("C");
                this.TipoPagoTextBox.InnerText = abono > 0 ? "ABONO ANTERIOR " + abono.ToString("C") : string.Empty;
                this.PeriodoPagoTextBox.InnerText = (toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault() != null ? Convert.ToDateTime(toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault()).ToString("MM-yyyy") : string.Empty) + "/" + (toma.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault() != null ? Convert.ToDateTime(toma.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).ToString("MM-yyyy") : string.Empty);
            }
            else if (toma.Pago.Select(c => c.ConceptoPagoId).LastOrDefault() == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua)
            {
                total = toma.Pago.Select(p => p.Total).LastOrDefault() - abono;

                this.TotalTextBox.InnerText = total.ToString("C");
                this.SubTotalTextBox.InnerText = toma.Pago.Select(p => p.SubTotal).LastOrDefault().ToString("C");
                this.AbonoTextBox.InnerText = abono > 0 ? abono.ToString("C") : string.Empty;
                var totalAbonos = (List<Pago>)Session["Abonos"];
                this.TipoPagoTextBox.InnerText = totalAbonos.Count() > 0 ? "ABONO ANTERIOR " + Convert.ToDecimal(totalAbonos.Sum(a => a.Abono)).ToString("C") : string.Empty;
                this.PeriodoPagoTextBox.InnerText = (toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault() != null ? Convert.ToDateTime(toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault()).ToString("MM-yyyy") : string.Empty) + "/" + (toma.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault() != null ? Convert.ToDateTime(toma.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).ToString("MM-yyyy") : string.Empty);
            }
            if (toma.Pago.Select(c => c.ConceptoPagoId).LastOrDefault() == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio)
            {
                var totalConvenio = toma.Convenio.Select(c => c.Total).LastOrDefault().ToString("C");
                var subTotalConvenio = toma.Convenio.Select(c => c.SubTotal).LastOrDefault().ToString("C");
                var descuentoConvenio = toma.Convenio.Select(c => c.Descuento).LastOrDefault(); 

                this.TotalTextBox.InnerText = totalConvenio;
                this.SubTotalTextBox.InnerText = subTotalConvenio;
                this.PeriodoPagoTextBox.InnerText = Convert.ToDateTime(toma.Convenio.Select(p => p.PeriodoInicio).LastOrDefault()).ToString("MM-yyyy") + "/" + Convert.ToDateTime(toma.Convenio.Select(p => p.PeriodoFin).LastOrDefault()).ToString("MM-yyyy");
                this.DescuentoTextBox.InnerText = descuentoConvenio != null ? Convert.ToDecimal(descuentoConvenio).ToString("C") : string.Empty; 
            }
            
            this.CantidadLetraTextBox.Value = "";
            this.TextoPersonalTextBox.Value = "";

            if (toma.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault() < toma.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()) { 
                this.AbonoTextBox.InnerText = !string.IsNullOrEmpty(toma.Pago.Select(p => p.Abono).LastOrDefault().ToString()) ? Convert.ToDecimal(toma.Pago.Select(p => p.Abono).LastOrDefault()).ToString("C") : string.Empty;
            }

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home");         
        }
    }
}