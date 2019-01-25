using ComiteAgua.Models;
using ComiteAgua.ViewModels.Tomas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ConveniosDomain
    {

        #region * Constructor generado por Comité Agua *

        public ConveniosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ConveniosDomain(DataContext applicationDbContext)

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

        public void CambiarEstatusConvenio(int convenioId, int estatusConvenioId, int usuarioId)
        {
            var convenioBD = _context.Convenio.Where(c => c.ConvenioId == convenioId).FirstOrDefault();

            convenioBD.EstatusConvenioId = (int)EstatusConvenioDomain.EstatusConvenioEnum.Concluido;
            convenioBD.FechaCambio = DateTime.Now;
            convenioBD.UsuarioCambioId = usuarioId;

            _context.SaveChanges();
        }
        public void Editar(Convenio model)
        {
            var bd = this.ObtenerConvenio(model.ConvenioId);           

            bd.PeriodoPagoConvenioId = model.PeriodoPagoConvenioId;
            bd.FechaInicio = model.FechaInicio;
            bd.FechaFin = model.FechaFin;
            bd.SubTotal = model.SubTotal;
            bd.Descuento = model.Descuento;
            bd.Total = model.Total;
            bd.Observaciones = model.Observaciones;
            bd.FechaCambio = DateTime.Now;
            bd.UsuarioCambioId = model.UsuarioAltaId;
            bd.PeriodoInicio = model.PeriodoInicio;
            bd.PeriodoFin = model.PeriodoFin;
            bd.PersonaId = model.PersonaId;
            bd.PimerPago = model.PimerPago;
            bd.Pagos = model.Pagos;
            bd.RutaArchivo = model.RutaArchivo;
            bd.ConceptoConvenioId = model.ConceptoConvenioId;
            bd.NoTarjeta = model.NoTarjeta;
            bd.EstatusConvenioId = model.EstatusConvenioId;

            _context.SaveChanges();
        }
        public void EliminarConvenio(int convenioId, int usuarioCambioId)
        {
            var result = _context.Convenio.Where(c => c.ConvenioId == convenioId).FirstOrDefault();

            result.Eliminado = true;
            result.FechaCambio = DateTime.Now;
            result.UsuarioCambioId = usuarioCambioId;

            _context.SaveChanges();
        }
        public void Guardar(Convenio model)
        {            
            _context.Entry(model).State = EntityState.Added;
           
            _context.SaveChanges();
        } // public void Guardar(Toma model)      
        public Convenio ObtenerConvenio(int convenioId)
        {
            var result = _context.Convenio
                .Include(c => c.EstatusConvenio)
                .Include(c => c.PeriodoPagoConvenio)
                .Include(c => c.ConceptoConvenio)
                .Include(c => c.Toma.Propietario.Persona.PersonaFisica)
                .Include(c => c.Toma.Direccion)
                .Include(c => c.Toma.Direccion.TiposCalle)
                .Include(c => c.Toma.Direccion.Calles)
                .Include(c => c.Persona)
                .Where(c => c.ConvenioId == convenioId).FirstOrDefault();

            return result;
        }
        public Convenio ObtenerConvenioTomaNueva(int tomaId = 0)
        {
            var result = _context.Convenio
                .Where(c => c.ConceptoConvenioId == (int)ConceptosConvenioDomain.ConceptosConvenioDomainEnum.TomaNueva &&
                            c.TomaId == tomaId)                
                .FirstOrDefault();

            return result;
        } // public Convenio ObtenerConvenioTomaNueva(int tomaId = 0)
        public Convenio ObtenerConvenioActivo(int tomaId = 0)
        {
            var result = _context.Convenio
                .Where(c => c.TomaId == tomaId &&
                            (c.EstatusConvenioId == (int) EstatusConvenioDomain.EstatusConvenioEnum.Activo ||
                             c.EstatusConvenioId == (int) EstatusConvenioDomain.EstatusConvenioEnum.Cancelado) &&
                            c.Eliminado == null)
                .OrderByDescending(c => c.ConvenioId).FirstOrDefault();

            return result;
        } // public Convenio ObtenerConvenioActivo(int tomaId = 0)               
        public IQueryable<Pago> ObtenerPagosConvenio(int convenioId)
        {
            var result = _context.Pago
                .Where(p => p.ConvenioId == convenioId);

            return result;
        } 
        public IQueryable<ConveniosViewModel> ObtenerConvenios()
        {
            var query = _context.Convenio
               .Join(_context.EstatusConvenio,
                   c => c.EstatusConvenioId,
                   ec => ec.EstatusConvenioId,
                   (c, ec) => new { Convenio = c, EstatusConvenio = ec })
               .Join(_context.ConceptoConvenio,
                   c_c => c_c.Convenio.ConceptoConvenioId,
                   cc => cc.ConceptoConvenioId,
                   (c_c, cc) => new { c_c.Convenio, c_c.EstatusConvenio, ConceptoConvenio = cc })
               .Join(_context.Toma,
                    c_c_c => c_c_c.Convenio.TomaId,
                    t => t.TomaId,
                    (c_c_c, t) => new { c_c_c.Convenio, c_c_c.EstatusConvenio, c_c_c.ConceptoConvenio, Toma = t })
               .Join(_context.Propietario,
                    t_t => t_t.Toma.PropietarioId,
                    p => p.PropietarioId,
                    (t_t, p) => new { t_t.Toma, t_t.Convenio, t_t.EstatusConvenio, t_t.ConceptoConvenio, Propietario = p })
                .Join(_context.Persona,
                    p_p => p_p.Propietario.PersonaId,
                    per => per.PersonaId,
                    (p_p, per) => new { p_p.Propietario, p_p.Toma, p_p.Convenio, p_p.EstatusConvenio, p_p.ConceptoConvenio, Persona = per })
                .Join(_context.PersonaFisica,
                    per_per => per_per.Persona.PersonaId,
                    pf => pf.PersonaId,
                    (per_per, pf) => new { per_per.Persona, per_per.Propietario, per_per.Toma, per_per.Convenio, per_per.EstatusConvenio, per_per.ConceptoConvenio, PersonaFisica = pf })
                    .Select(c => new ConveniosViewModel
                    {
                        ConvenioId = c.Convenio.ConvenioId,
                        ConceptoConvenio = c.ConceptoConvenio.Nombre,
                        EstatusConvenio = c.EstatusConvenio.Nombre,
                        TomaId = c.Toma.TomaId,
                        Folio = c.Toma.Folio,
                        NoTarjeta = c.Convenio.NoTarjeta,
                        NombreCompleto = c.PersonaFisica.Nombre + " " + c.PersonaFisica.ApellidoPaterno + " " + c.PersonaFisica.ApellidoMaterno
                    })
                    .OrderBy(x => x.Folio);

            return query;
        }

        #endregion

    } // public class ConveniosDomain
} // namespace ComiteAgua.Domain