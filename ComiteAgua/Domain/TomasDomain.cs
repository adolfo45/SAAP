using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class TomasDomain
    {

        #region * Constructor generado por Comité Agua *

        public TomasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public TomasDomain(DataContext applicationDbContext)

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

        public void Guardar(Toma model)
        {
            if (model.TomaId > 0)
            {
                var bd = _context.Toma.Where(t => t.TomaId == model.TomaId).FirstOrDefault();

                bd.CategoriaId = model.CategoriaId;
                //bd.LiquidacionTomaId = model.LiquidacionTomaId;
                bd.PropietarioId = model.PropietarioId;
                bd.DireccionId = model.DireccionId;
                bd.Folio = model.Folio;
                bd.Observaciones = model.Observaciones;
                bd.Activa = model.Activa;
                bd.FechaCambio = DateTime.Now;
                bd.UsuarioCambioId = model.UsuarioAltaId;
                bd.Pasiva = model.Pasiva;
            }
            else
            {
                _context.Entry(model).State = EntityState.Added;
            }

            _context.SaveChanges();
        } // public void Guardar(Toma model)       
        public void EditarDireccionToma(Toma model)
        {
            var tomaBD = _context.Toma.Where(t => t.TomaId == model.TomaId).SingleOrDefault();

            tomaBD.DireccionId = model.DireccionId;
            tomaBD.Activa = model.Activa;
            tomaBD.FechaCambio = model.FechaCambio;
            tomaBD.UsuarioCambioId = model.UsuarioCambioId;

            _context.SaveChanges();
        } // public void EditarDireccionToma(Toma model
        public void EditarActiva(int tomaId, bool activa, int usuarioId)
        {
            var tomaBD = _context.Toma.Where(t => t.TomaId == tomaId).FirstOrDefault();

            tomaBD.Activa = activa;
            tomaBD.FechaCambio = DateTime.Now;
            tomaBD.UsuarioCambioId = usuarioId;

            _context.SaveChanges();
        } // public void EditarActiva(int tomaId, bool activa, int usuarioId)
        public bool ExisteFolio(int folio)
        {
            var result = _context.Toma.Where(t => t.Folio == folio).FirstOrDefault();

            return result != null;
        }
        public void Eliminar(int propietarioId)
        {                       
            var toma = _context.Toma
                .Include(t => t.Convenio)
                .Include(t => t.Pago)
                .Include(t => t.PeriodoPago)
                .Include(t => t.Notificacion)
                .Include(t => t.Pago.Select(r => r.Recibo))
                .Include(t => t.Direccion)
                .Include(t => t.Servicio)
                .Include(t => t.Reporte)
                .Include(t => t.Servicio.Select(e => e.EvidenciaServicio))
                .Where(t => t.PropietarioId == propietarioId).FirstOrDefault();

            if (toma != null)
            {                
                if (toma.PeriodoPago.Count > 0)
                {
                    while (toma.PeriodoPago.Count > 0)
                    {
                        _context.Entry(toma.PeriodoPago.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }
                if (toma.Pago.Count > 0)
                {
                    while (toma.Pago.Count > 0)
                    {
                        if (toma.Pago.Select(r => r.Recibo).FirstOrDefault().Count > 0)
                            _context.Entry(toma.Pago.Select(r => r.Recibo.FirstOrDefault()).FirstOrDefault()).State =
                                EntityState.Deleted;
                        _context.Entry(toma.Pago.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }               
                if (toma.Convenio.Count > 0)
                {
                    while (toma.Convenio.Count > 0)
                    {
                        _context.Entry(toma.Convenio.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }                
                if (toma.Notificacion.Count > 0)
                {
                    while (toma.Notificacion.Count > 0)
                    {
                        _context.Entry(toma.Notificacion.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }                
                if (toma.DireccionId != null)
                { 
                    _context.Entry(toma.Direccion).State = EntityState.Deleted;
                }                
                if (toma.Servicio.Count > 0)
                {
                    while (toma.Servicio.Count > 0)
                    {
                        if (toma.Servicio.Select(e => e.EvidenciaServicio).FirstOrDefault().Count > 0)
                        {
                            while (toma.Servicio.Select(e => e.EvidenciaServicio).FirstOrDefault().Count > 0)
                            {
                                _context.Entry(toma.Servicio.Select(e => e.EvidenciaServicio.FirstOrDefault()).FirstOrDefault()).State = EntityState.Deleted;
                            }
                        }                                                                
                        _context.Entry(toma.Servicio.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }                
                if (toma.Reporte.Count > 0)
                {
                    while(toma.Reporte.Count > 0)
                    {
                        _context.Entry(toma.Reporte.FirstOrDefault()).State = EntityState.Deleted;
                    }
                }
                _context.Entry(toma).State = EntityState.Deleted;
            }            

            var propietario = _context.Propietario
                .Include(t => t.Persona)
                .Include(t => t.Persona.PersonaFisica)
                .Where(t => t.PropietarioId == propietarioId).FirstOrDefault();

            _context.Entry(propietario.Persona.PersonaFisica).State = EntityState.Deleted;
            _context.Entry(propietario.Persona).State = EntityState.Deleted;
            _context.Entry(propietario).State = EntityState.Deleted;           

            _context.SaveChanges();
        }
        public void EditarConceptoPago(int tomaId, int liquidacionTomaId, int usuarioId)
        {
            var bd = _context.Toma.Where(t => t.TomaId == tomaId).FirstOrDefault();

            bd.LiquidacionTomaId = liquidacionTomaId;
            bd.UsuarioCambioId = usuarioId;
            bd.FechaCambio = DateTime.Now;

            _context.SaveChanges();
        }//public void EditarConceptoPago(int tomaId, int conceptoPagoId, int usuarioId)
        public Toma ObtenerToma(int tomaId)
        {
            var result = _context.Toma
                .Include(t => t.Propietario)
                .Include(t => t.Propietario.Persona.PersonaFisica)
                .Include(t => t.PeriodoPago)
                .Include(t => t.Pago)
                .Include(t => t.Direccion)
                .Include(t => t.Pago.Select(c => c.ConceptoPago))   
                .Include(t => t.Convenio)
                .Include(t => t.Notificacion)
                .Include(t => t.Direccion.TiposCalle)
                .Include(t => t.Direccion.Calles)
                .Include(t => t.Direccion.Colonias)
                .Include(t => t.Constancia)
                .Where(t => t.TomaId == tomaId).FirstOrDefault();

            return result;
        }
        public List<Toma> ObtenerTomas()
        {
            var result = _context.Toma
                .Include(t => t.PeriodoPago)
                .Include(t => t.Propietario.Persona.PersonaFisica)
                .Include(t => t.Direccion)
                .Include(t => t.Direccion.TiposCalle)
                .Include(t => t.Direccion.Calles)
                .Include(t => t.Direccion.Colonias)
                .Include(t => t.Categoria)
                .Include(t => t.Convenio)
                .Include(t => t.Notificacion)
                .Include(t => t.Convenio.Select(c => c.EstatusConvenio))
                .Include(t => t.Convenio.Select(c => c.ConceptoConvenio))
                .OrderBy(t => t.Folio);

            return result.OrderBy(t => t.Propietario.Persona.PersonaFisica.Nombre).ToList();
        }
        public int ObtenerFolioToma(int tomaId)
        {
            var result = _context.Toma.FirstOrDefault(t => t.TomaId == tomaId).Folio;

            return result;
        }
        public bool ValidarTomaExistente(int folio, int tomaId)
        {
            var toma = _context.Toma
                .Where(t => t.Folio == folio &&
                            t.TomaId != tomaId).FirstOrDefault();

            return toma != null ? true : false;
        }//public bool ValidarTomaExistente(int tomaId)

        #endregion

    } // public class TomasDomain

} // namespace ComiteAgua.Domain