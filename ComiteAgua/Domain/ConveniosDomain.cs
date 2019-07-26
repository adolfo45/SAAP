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
                .Include(c => c.Toma.Propietario.Persona.PersonaMoral)
                .Include(c => c.Toma.Direccion)
                .Include(c => c.Toma.Direccion.TiposCalle)
                .Include(c => c.Toma.Direccion.Calles)
                .Include(c => c.Persona)
                .Include(c => c.Toma)
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
                .Include(c => c.Pago)
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
            var convenios = _context.Convenio
                .Include(c => c.EstatusConvenio)
                .Include(c => c.ConceptoConvenio)
                .Include(c => c.Toma)
                .Include(c => c.Toma.Propietario)
                .Include(c => c.Toma.Propietario.Persona)
                .Include(c => c.Toma.Propietario.Persona.PersonaFisica)
                .Include(c => c.Toma.Propietario.Persona.PersonaMoral)
                .OrderBy(c => c.Toma.Folio);

            var preList = convenios
               .Select(up => new ConveniosViewModel
               {
                   ConvenioId = up.ConvenioId,
                   ConceptoConvenio = up.ConceptoConvenio.Nombre,
                   EstatusConvenio = up.EstatusConvenio.Nombre,
                   TomaId = up.Toma.TomaId,
                   Folio = up.Toma.Folio,
                   NoTarjeta = up.NoTarjeta,
                   NombreCompleto = up.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? up.Toma.Propietario.Persona.PersonaFisica.Nombre + " " + up.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " + up.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : up.Toma.Propietario.Persona.PersonaMoral.Nombre
               })
               .OrderByDescending(up => up.Folio);

            return preList;
        }

        #endregion

    } // public class ConveniosDomain
} // namespace ComiteAgua.Domain