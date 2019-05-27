using ComiteAgua.Models;
using ComiteAgua.Models.Recibos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Recibos
{
    public class RecibosDomain
    {

        #region * Constructor generado por Comité Agua *

        public RecibosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } //  public RecibosDomain(DataContext applicationDbContext)

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

        public void CancelarRecibo(int reciboId, bool estado, int usuarioId)
        {
            var bd = _context.Recibo
                .Include( r => r.Pago)
                .Include(r => r.Pago.PeriodoPago)
                .Include(r => r.Pago.Convenio)
                .Where(r => r.ReciboId == reciboId).FirstOrDefault();

            if (bd.Pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
            {
                var bdToma = _context.Toma.Where(x => x.TomaId == bd.Pago.TomaId).FirstOrDefault();
                bdToma.LiquidacionTomaId = (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaNueva;
                bdToma.UsuarioCambioId = usuarioId;
                bdToma.FechaCambio = DateTime.Now;
            }
            if (bd.Pago.ConvenioId != null)
            {
                bd.Pago.Convenio.EstatusConvenioId = (int) EstatusConvenioDomain.EstatusConvenioEnum.Activo;
            }            

            if (bd.Pago.PeriodoPago.Count > 0) _context.Entry(bd.Pago.PeriodoPago.FirstOrDefault()).State = EntityState.Deleted;
            //_context.Entry(bd.Pago).State = EntityState.Deleted;
            //_context.Entry(bd).State = EntityState.Deleted;           
            bd.Pago.Activo = false;

            _context.SaveChanges();
        }
        public void Gurdar(Recibo model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        public int ObtenerNoRecibo()
        {
            var result = _context.Recibo.OrderByDescending(r => r.ReciboId).FirstOrDefault();

            return result == null ? 0 : result.NoRecibo;
        }
        public Recibo ObtenerReciboImpresion(int reciboId)
        {
            var result = _context.Recibo
                .Include(r => r.Pago)
                .Include(r => r.Pago.Toma)
                .Include(r => r.Pago.Toma.Direccion)
                .Include(r => r.Pago.Toma.Propietario)
                .Include(r => r.Pago.Toma.Propietario.Persona.PersonaFisica)
                .Include(r => r.Pago.Toma.Propietario.Persona.PersonaMoral)
                .Include(r => r.Pago.PeriodoPago)
                .Include(r => r.Pago.Toma.Convenio)
                .Include(r => r.Pago.ConceptoPago)               
                .Include(x => x.Pago.Toma.Direccion.Colonias)
                .Include(x => x.Pago.Toma.Direccion.TiposCalle)
                .Include(x => x.Pago.Toma.Direccion.Calles)
                .Include(r => r.Pago.DescuentoProntoPago)
                .Include(r => r.Pago.Constancia)
                .Include(r => r.Pago.Constancia.TiposCalle)
                .Include(r => r.Pago.Constancia.Calles)
                .Include(r => r.Pago.Constancia.Colonias)
                .Include(r => r.Pago.Constancia.TiposConstancia)
                .Include(r => r.Pago.Renta)
                .Include(r => r.Pago.Renta.TipoRenta)
                .Where(r => r.ReciboId == reciboId).FirstOrDefault();

            return result;
        }
        public Recibo ObtenerRecibo(int reciboId)
        {
            var recibo = _context.Recibo
                .Include(r => r.Pago)
                .Include(r => r.Pago.Toma)
                .Where(r => r.ReciboId == reciboId).FirstOrDefault();

            return recibo;
        }
        public IQueryable<Recibo> ObtenerRecibos(int? noRecibo, int? folio, DateTime? fecha, int? reciboId)
        {
            var recibos = _context.Recibo
                .Include(r => r.Pago)
                .Include(r => r.Pago.Toma)
                .Include(r => r.Pago.Constancia.TiposConstancia)
                .Include(r => r.Pago.Renta.TipoRenta)
                .Include(r => r.Pago.ConceptoPago)
                .Where(r => r.PagoId != null);

            if (reciboId != null)
            {
                recibos = recibos.Where(r => r.ReciboId == reciboId);
                return recibos;
            }
            if (noRecibo != null)
            {
                recibos = recibos.Where(r => r.NoRecibo == noRecibo);
            }
            if (folio != null)
            {
                recibos = recibos.Where(r => r.Pago.Toma.Folio == folio);
            }
            if (fecha != null)
            {
                recibos = recibos.Where(r => DbFunctions.TruncateTime(r.Fecha) == DbFunctions.TruncateTime(fecha));
            }

            return recibos;

        }//public IQueryable<Recibo> ObtenerRecibos(int? noRecibo, int? folio, DateTime? fecha)
       
        #endregion

    } // public class RecibosDomain
} // namespace ComiteAgua.Domain.Recibos