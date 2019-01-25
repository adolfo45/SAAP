using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class PagosDomain
    {

        #region * Constructor generado por Comité Agua *

        public PagosDomain(DataContext applicationDbContext)
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

        public void EditarAbonosActivos(List<Pago> pagosAbono)
        {
            foreach (var item in pagosAbono)
            {
                var bd = _context.Pago.Where(p => p.PagoId == item.PagoId).FirstOrDefault();
                bd.Activo = false;
            }
            _context.SaveChanges();
        }
        public void Guardar(Pago model)
        {
            if (model.PagoId > 0)
            {
                var bd = _context.Pago.SingleOrDefault(x => x.PagoId == model.PagoId);

                bd.SubTotal = model.SubTotal;
                bd.Descuento = model.Descuento;
                bd.Total = model.Total;
                bd.FechaCambio = DateTime.Now;
                bd.UsuarioCambioId = model.UsuarioAltaId;
            }
            else
            {               
                _context.Entry(model).State = EntityState.Added;
            }

            _context.SaveChanges();
        } // public void Guardar(Pago model) 
        public Pago ObtenerPagoToma(int tomaId = 0)
        {
            var result = _context.Pago
                .Where(p => p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva &
                            p.TomaId == tomaId).FirstOrDefault();

            return result;
        }
        public List<Pago> ObtenerPagosConvenio(int convenioId = 0)
        {
            var result = _context.Pago
                .Include(p => p.Recibo)
                .Where(p => p.ConvenioId == convenioId &&
                            p.Activo).ToList();

            return result;

        } // public List<Pago> ObtenerPagos(int convenioId = 0)
        public List<Pago> ObtenerAbonos(int tomaId)
        {
            var result = _context.Pago                
                .Where(p => p.TomaId == tomaId &
                            p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Abono &
                            p.Activo).ToList();

            return result;
        }
        public List<Pago> ObtenerPagos(DateTime fecha)
        {
            var result = _context.Pago
                .Include(p => p.ConceptoPago)
                 .Include(p => p.Recibo)
                 .Include(p => p.Convenio)
                .Where(i => DbFunctions.TruncateTime(i.FechaAlta) == DbFunctions.TruncateTime(fecha))
                .ToList();

            return result;
        }        
        public Pago ObtenerPago(int pagoId)
        {
            var pago = _context.Pago
                .Include(p => p.Toma)
                .Where(p => p.PagoId == pagoId).FirstOrDefault();

            return pago;
        }

        #endregion

    } // public class PagosDomain

} // namespace ComiteAgua.Domain