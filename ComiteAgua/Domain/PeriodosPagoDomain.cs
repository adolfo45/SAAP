using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComiteAgua.ViewModels;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

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
        public List<PeriodoPago> ObtenerDeudores(DateTime? periodoInicio, DateTime? periodoFin, int? calleId)
        {
            var query = new List<PeriodoPago>();

            if (periodoInicio != null && periodoFin != null)
            {
                query = _context.PeriodoPago
               .Include(pe => pe.Pago)
               .Include(pe => pe.Toma)
               .Include(pe => pe.Toma.Convenio)
               .Include(pe => pe.Toma.Categoria)
               .Include(pe => pe.Toma.Categoria.Tarifa)
               .Include(pe => pe.Toma.Direccion)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaFisica)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaMoral)
               .Include(pe => pe.Toma.Direccion.TiposCalle)
               .Include(pe => pe.Toma.Direccion.Calles)
               .Include(pe => pe.Toma.Direccion.Colonias)
               .OrderByDescending(pe => pe.PeriodoPagoId).DistinctBy(p => p.TomaId)
               .Where(pe => (pe.MesAnoFin >= periodoInicio &&
                            pe.MesAnoFin <= periodoFin))
               .ToList();

                if (calleId != null)
                {
                    query = query.Where(p => p.Toma.Direccion.CalleId == calleId).ToList();
                }
            } else if (periodoInicio != null)
            {
                query = _context.PeriodoPago
               .Include(pe => pe.Pago)
               .Include(pe => pe.Toma)
               .Include(pe => pe.Toma.Convenio)
               .Include(pe => pe.Toma.Categoria)
               .Include(pe => pe.Toma.Categoria.Tarifa)
               .Include(pe => pe.Toma.Direccion)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaFisica)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaMoral)
               .Include(pe => pe.Toma.Direccion.TiposCalle)
               .Include(pe => pe.Toma.Direccion.Calles)
               .Include(pe => pe.Toma.Direccion.Colonias)
               .OrderByDescending(pe => pe.PeriodoPagoId).DistinctBy(p => p.TomaId)
               .Where(pe => pe.MesAnoFin == periodoInicio)
               .ToList();               

                if (calleId != null)
                {
                    query = query.Where(p => p.Toma.Direccion.CalleId == calleId).ToList();
                }
            } else if (periodoFin != null)
            {
                query = _context.PeriodoPago
              .Include(pe => pe.Pago)
              .Include(pe => pe.Toma)
              .Include(pe => pe.Toma.Convenio)
              .Include(pe => pe.Toma.Categoria)
              .Include(pe => pe.Toma.Categoria.Tarifa)
              .Include(pe => pe.Toma.Direccion)
              .Include(pe => pe.Toma.Propietario.Persona.PersonaFisica)
              .Include(pe => pe.Toma.Propietario.Persona.PersonaMoral)
              .Include(pe => pe.Toma.Direccion.TiposCalle)
              .Include(pe => pe.Toma.Direccion.Calles)
              .Include(pe => pe.Toma.Direccion.Colonias)
              .OrderByDescending(pe => pe.PeriodoPagoId).DistinctBy(p => p.TomaId)
              .Where(pe => pe.MesAnoFin == periodoFin)
              .ToList();

                if (calleId != null)
                {
                    query = query.Where(p => p.Toma.Direccion.CalleId == calleId).ToList();
                }
            }
            else
            {
                query = _context.PeriodoPago
               .Include(pe => pe.Pago)
               .Include(pe => pe.Toma)
               .Include(pe => pe.Toma.Convenio)
               .Include(pe => pe.Toma.Categoria)
               .Include(pe => pe.Toma.Categoria.Tarifa)
               .Include(pe => pe.Toma.Direccion)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaFisica)
               .Include(pe => pe.Toma.Propietario.Persona.PersonaMoral)
               .Include(pe => pe.Toma.Direccion.TiposCalle)
               .Include(pe => pe.Toma.Direccion.Calles)
               .Include(pe => pe.Toma.Direccion.Colonias)
               .OrderByDescending(pe => pe.PeriodoPagoId).DistinctBy(p => p.TomaId)
               .ToList();

                query = query.Where(p => p.Toma.Direccion.CalleId == calleId &&
                                        (p.MesAnoFin != null ? Convert.ToDateTime(p.MesAnoFin).Year < DateTime.Now.Year : p.MesAnoFin == null)).ToList();
            }

            query = query.Where(c => c.Toma.Convenio.Select(co => co.EstatusConvenioId).LastOrDefault() != (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo).ToList();

            return query;
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

        } //public void Guardar(PeriodoPago model)
        public PeriodoPago ObtenerPeriodoPago(int tomaId = 0)
        {
            var result = _context.PeriodoPago
                .Where(p => p.TomaId == tomaId);

            return result.OrderByDescending(x => x.PeriodoPagoId).FirstOrDefault();
        } //public PeriodoPago ObtenerPeriodoPago(int tomaId = 0)        
        public List<Pago> ObtenerPeriodosPago(int tomaId = 0)
        {
            var result = _context.Pago
                .Include(pp => pp.PeriodoPago)
                .Include(pp => pp.ConceptoPago)
                .Include(pp => pp.Recibo)
                .Include(pp => pp.Convenio)
                .Include(pp => pp.Toma)
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