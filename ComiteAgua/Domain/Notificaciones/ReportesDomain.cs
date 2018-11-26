using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComiteAgua.Models.Notificaciones;

namespace ComiteAgua.Domain.Notificaciones
{
    public class ReportesDomain
    {

        #region * Constructor generado por Comité Agua *

        public ReportesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ReportesDomain(DataContext applicationDbContext)

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

        public IQueryable<Reporte> ObtenerReportes()
        {
            var result = _context.Reporte
                .Include(r => r.UsuarioAlta.Persona)
                .Include(r => r.Toma.Direccion)
                .Include(r => r.Toma);

            return result;
        }

        public void AutorizarReporte(int reporteId, int usuarioId)
        {
            var reporte = _context.Reporte
                .Where(r => r.ReporteId == reporteId).FirstOrDefault();

            reporte.Revisado = true;
            reporte.FechaCambio = DateTime.Now;
            reporte.UsuarioCambioId = usuarioId;

            _context.SaveChanges();
        }

        public void ConcluirReporte(int reporteId, int usuarioId)
        {
            var reporte = _context.Reporte
                .Where(r => r.ReporteId == reporteId).FirstOrDefault();

            reporte.Concluido = true;
            reporte.FechaCambio = DateTime.Now;
            reporte.UsuarioCambioId = usuarioId;

            _context.SaveChanges();
        }

        #endregion

    } // public class ReportesDomain

} // namespace ComiteAgua.Domain.Notificaciones