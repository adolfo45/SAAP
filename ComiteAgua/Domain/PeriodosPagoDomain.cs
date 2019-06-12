using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComiteAgua.ViewModels;
using System.Data.Entity;

namespace ComiteAgua.Domain
{
    public class PeriodosPagoDomain
    {

        #region * Constructor generado por Comité Agua *

        public PeriodosPagoDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public CategoriasDomain(DataContext applicationDbContext)

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

        public List<PeriodoPago> ObtenerEstadoCuenta(int tomaId)
        {
            var estadoCuenta = _context.PeriodoPago
                .Include(pe => pe.Pago)
                .Include(pe => pe.Toma)
                .Include(pe => pe.Toma.Categoria)
                .Include(pe => pe.Toma.Categoria.Tarifa)
                .Include(pe => pe.Toma.Direccion)
                .Include(pe => pe.Toma.Propietario.Persona.PersonaFisica)
                .Include(pe => pe.Toma.Propietario.Persona.PersonaMoral)
                .Include(pe => pe.Toma.Direccion.TiposCalle)
                .Include(pe => pe.Toma.Direccion.Calles)
                .Include(pe => pe.Toma.Direccion.Colonias)
                .Where(pe => pe.Toma.TomaId == tomaId)               
                .ToList();

            return estadoCuenta;
        }       
        public void Guardar(PeriodoPago model)
        {
            if (model.PeriodoPagoId > 0)
            {
                return;
            }
            else
            {
                _context.Entry(model).State = EntityState.Added;
            }

            _context.SaveChanges();

        } // public void Guardar(PeriodoPago model)
        public PeriodoPago ObtenerPeriodoPago(int tomaId = 0)
        {
            var result = _context.PeriodoPago
                .Where(p => p.TomaId == tomaId);

            return result.OrderByDescending(x => x.PeriodoPagoId).FirstOrDefault();
        } // public PeriodoPago ObtenerPeriodoPago(int tomaId = 0)        
        public List<Pago> ObtenerPeriodosPago(int tomaId = 0)
        {
            var result = _context.Pago
                .Include(pp => pp.PeriodoPago)
                .Include(pp => pp.ConceptoPago)
                .Include(pp => pp.Recibo)
                .Include(pp => pp.Convenio)
                .Where(pp => pp.TomaId == tomaId)
                .OrderByDescending(pp => pp.PagoId).ToList();

            return result;
        }

        public PeriodoPago ObtenerUltimoPeriodoPago(int tomaId)
        {
            var periodo = _context.PeriodoPago
                .Where(p => p.TomaId == tomaId)
                .OrderByDescending(p => p.PeriodoPagoId).FirstOrDefault();

            return periodo;
        }

        #endregion

    } // public class PeriodosPagoDomain

} // namespace ComiteAgua.Domain