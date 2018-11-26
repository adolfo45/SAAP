using ComiteAgua.Models;
using ComiteAgua.Models.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace ComiteAgua.Domain.Notificaciones
{
    public class NotificacionesDomain
    {

        #region * Constructor generado por Adserti *

        public NotificacionesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public NotificacionesDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Adserti 

        #endregion

        #region * Eventos generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *

        public Notificacion ObtenerNotificacion(int tomaId)
        {
            var result = _context.Notificacion
                .Where(n => n.TomaId == tomaId &&
                              n.Activa).FirstOrDefault();

            return result;
        }

        public IQueryable<Notificacion> ObtenerNotificaciones(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? notificadorId = null)
        {
            var result = _context.Notificacion
                .Include(n => n.TipoNotificacion)
                .Include(n => n.UsuarioNotifico.Persona)
                .Include(n => n.Toma)
                .Include(n => n.Toma.Propietario)
                .Include(n => n.Toma.Propietario.Persona.PersonaFisica)
                .Include(n => n.Toma.Direccion)
                .Include(n => n.Toma.Categoria)
                .Include(n => n.CtoOpcionesNotificacion)
                .Include(n => n.Pagos);

            if (fechaInicio != null)
            {
                result = result.Where(x => DbFunctions.TruncateTime(x.FechaEntrega) >= DbFunctions.TruncateTime(fechaInicio));
            }

            if (fechaFin != null)
            {
                result = result.Where(x => DbFunctions.TruncateTime(x.FechaEntrega) <= DbFunctions.TruncateTime(fechaFin));
            }

            if (notificadorId != null)
            {
                result = result.Where(x => x.UsuarioNotificoId == notificadorId);
            }

            return result;
        }

        public IQueryable<Pago> ObtenerNotificacionesPagadas(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? notificadorId = null)
        {           

            var result = _context.Pago
                .Include(p => p.Notificaciones)
                .Include(p => p.Notificaciones.TipoNotificacion)
                .Include(p => p.Notificaciones.UsuarioNotifico.Persona)
                .Include(p => p.Toma)
                .Include(p => p.Toma.Propietario)
                .Include(p => p.Toma.Propietario.Persona.PersonaFisica)
                .Include(p => p.Toma.Direccion)
                .Include(p => p.Toma.Categoria)
                .Where(p => p.NotificacionId != null);

            if (fechaInicio != null)
            {
                result = result.Where(x => DbFunctions.TruncateTime(x.FechaAlta) >= DbFunctions.TruncateTime(fechaInicio));
            }

            if (fechaFin != null)
            {
                result = result.Where(x => DbFunctions.TruncateTime(x.FechaAlta) <= DbFunctions.TruncateTime(fechaFin));
            }

            if (notificadorId != null)
            {
                result = result.Where(x => x.Notificaciones.UsuarioNotificoId == notificadorId);
            }

            return result;
        }

        public void DesactivarNotificacion(int notificacionId, int usuarioId)
        {
            var bd = _context.Notificacion.SingleOrDefault(x => x.NotificacionId == notificacionId);

            bd.Activa = false;
            bd.UsuarioCambioId = usuarioId;
            bd.FechaCambio = DateTime.Now;

            _context.SaveChanges();
        }

        #endregion

    } // public class NotificacionesDomain
}// namespace ComiteAgua.Domain.Notificaciones