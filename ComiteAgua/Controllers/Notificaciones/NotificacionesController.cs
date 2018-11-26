using ComiteAgua.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Domain.Notificaciones;
using ComiteAgua.ViewModels.Notificaciones;
using ComiteAgua.ViewModels.Seguridad;
using ComiteAgua.Filters.Security;

namespace ComiteAgua.Controllers.Notificaciones
{
    [Authenticate]
    public class NotificacionesController : MessageControllerBase
    {

        #region * Constructor generado por Adserti *

        public NotificacionesController()
        {
            _context = new DataContext();
        }

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Adserti 

        #endregion

        #region * Acciones generados por Adserti *

        public ActionResult NotificacionesLista()
        {
            var personasSeguridadDomain = new PersonasSeguridadDomain(_context);
            var personasSeguridad = personasSeguridadDomain.ObtenerNotificadores();

            var notificadoresList = personasSeguridad
              .Select(up => new PersonaSeguridadViewModel
              {
                  PersonaId = up.PersonaId,
                  Nombre = up.Nombre + " " + up.ApellidoPaterno + " " + up.ApellidoMaterno,
                  UsuarioId = up.Usuarios.Select(u => u.UsuarioId).FirstOrDefault()
              })
              .OrderByDescending(up => up.Nombre)
              .ToList();

            var notificacionesViewModel = new NotificacionesViewModel()
            {
                Notificadores = notificadoresList
            };

            return View(notificacionesViewModel);
        }

        public ActionResult _NotificacionesLista(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? notificadorId = null)
        {
            var notificacionesDomain =  new NotificacionesDomain(_context);

            var notificaciones = notificacionesDomain.ObtenerNotificaciones(fechaInicio, fechaFin, notificadorId);

            var notificacionesList = notificaciones
             .Select(up => new NotificacionesViewModel
             {
                NotificacionId = up.NotificacionId,
                Folio = up.Toma.Folio,
                Direccion = up.Toma.Direccion.Calle + (!string.IsNullOrEmpty(up.Toma.Direccion.NumExt) ? " EXT." + up.Toma.Direccion.NumExt : string.Empty) +
                            (!string.IsNullOrEmpty(up.Toma.Direccion.NumInt) ? " INT." + up.Toma.Direccion.NumInt : string.Empty) + 
                            " " + up.Toma.Direccion.Colonia,
                Categoria = up.Toma.Categoria.Abreviatura,
                FechaEntrega = up.FechaEntrega,
                UsuarioNotifico = up.UsuarioNotifico.Persona.Nombre + " " + up.UsuarioNotifico.Persona.ApellidoPaterno + " " + up.UsuarioNotifico.Persona.ApellidoMaterno,
                //NoPagos = up.Pagos.Count().ToString(),
                //Porcentaje = ((up.Pagos.Select(p => p.Total).Sum() * 5) / 100).ToString(),
                TipoNotificacion = up.TipoNotificacion.Nombre,
                Activa = up.Activa,
                Observaciones = up.Observaciones,
                OpcionNotificacion = up.CtoOpcionesNotificacion.Nombre,
                TotalPagar = up.TotalPagar.ToString()
             })
             .OrderByDescending(up => up.UsuarioNotifico)
             .ToList();            

            return PartialView(notificacionesList);

        }

        public ActionResult NotificacionesPagadas()
        {
            var personasSeguridadDomain = new PersonasSeguridadDomain(_context);
            var personasSeguridad = personasSeguridadDomain.ObtenerNotificadores();

            var notificadoresList = personasSeguridad
              .Select(up => new PersonaSeguridadViewModel
              {
                  PersonaId = up.PersonaId,
                  Nombre = up.Nombre + " " + up.ApellidoPaterno + " " + up.ApellidoMaterno,
                  UsuarioId = up.Usuarios.Select(u => u.UsuarioId).FirstOrDefault()
              })
              .OrderByDescending(up => up.Nombre)
              .ToList();

            var notificacionesViewModel = new NotificacionesViewModel()
            {
                Notificadores = notificadoresList
            };

            return View(notificacionesViewModel);
        }

        public ActionResult _NotificacionesPagadas(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? notificadorId = null)
        {
            var notificacionesDomain = new NotificacionesDomain(_context);

            var notificaciones = notificacionesDomain.ObtenerNotificacionesPagadas(fechaInicio, fechaFin, notificadorId);

            var notificacionesList = notificaciones
             .Select(up => new NotificacionesViewModel
             {
                 NotificacionId = up.Notificaciones.NotificacionId,
                 Folio = up.Toma.Folio,
                 Direccion = up.Toma.Direccion.Calle + (!string.IsNullOrEmpty(up.Toma.Direccion.NumExt) ? " EXT." + up.Toma.Direccion.NumExt : string.Empty) +
                            (!string.IsNullOrEmpty(up.Toma.Direccion.NumInt) ? " INT." + up.Toma.Direccion.NumInt : string.Empty) +
                            " " + up.Toma.Direccion.Colonia,
                 Categoria = up.Toma.Categoria.Abreviatura,
                 FechaPago = up.FechaAlta,
                 UsuarioNotifico = up.Notificaciones.UsuarioNotifico.Persona.Nombre + " " + up.Notificaciones.UsuarioNotifico.Persona.ApellidoPaterno + " " + up.Notificaciones.UsuarioNotifico.Persona.ApellidoMaterno,
                 NoRecibo = up.ConvenioId == null ? "(R) " + up.NoRecibo : "(T) " + up.NoRecibo,
                 Porcentaje = ((up.Total * 5) / 100).ToString(),
                 Total = up.Total.ToString(),
                 TipoNotificacion = up.Notificaciones.TipoNotificacion.Nombre
             })
             .OrderByDescending(up => up.UsuarioNotifico)
             .ToList();

            return PartialView(notificacionesList);
        }

        public ActionResult ReportesNotificacion(int tabActivo = 0)
        {
            var reportesDomain = new ReportesDomain(_context);
            var reportesViewModel = new ReportesViewModel();

            var reportes = reportesDomain.ObtenerReportes().ToList();

            reportesViewModel.Autorizar = reportes.Where(r => r.Revisado == false).ToList();
            reportesViewModel.Pendientes = reportes.Where(r => r.Revisado && r.Concluido == false).ToList();
            reportesViewModel.Concluidos = reportes.Where(r => r.Revisado && r.Concluido).ToList();
            reportesViewModel.UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString());            
                
            if (tabActivo == 1)
                reportesViewModel.TabAutorizacionActivo = "active";
            else if (tabActivo == 2)
                reportesViewModel.TabPendienteActivo = "active";
            else
                reportesViewModel.TabAutorizacionActivo = "active";

            return View(reportesViewModel);
        }

        public ActionResult AutorizarReporte(int reporteId, int usuarioId)
        {
            var reportesDomain = new ReportesDomain(_context);

            reportesDomain.AutorizarReporte(reporteId, usuarioId);

            return RedirectToAction("ReportesNotificacion", new { tabActivo = 1 });
        }

        public ActionResult ConcluirReporte(int reporteId, int usuarioId)
        {
            var reportesDomain = new ReportesDomain(_context);

            reportesDomain.ConcluirReporte(reporteId, usuarioId);

            return RedirectToAction("ReportesNotificacion", new { tabActivo = 2});
        }

        #endregion

        #region * Métodos creados por Adserti *
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        } // protected override void Dispose(bool disposing)
        #endregion

    } // public class NotificacionesController : Controller

} // namespace ComiteAgua.Controllers.Notificaciones