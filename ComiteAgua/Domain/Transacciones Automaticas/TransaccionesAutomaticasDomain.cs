using ComiteAgua.Models;
using ComiteAgua.Models.TransaccionesAutomaticas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Transacciones_Automaticas
{
    public class TransaccionesAutomaticasDomain
    {

        #region * Constructor generado por Comité Agua *

        public TransaccionesAutomaticasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public TransaccionesAutomaticasDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public TransaccionAutomatica ObtenerTransaccion(int tipoTransaccionId)
        {
            var result = _context.TransaccionAutomatica
                .Where(t => t.TipoTransaccionAutomaticaId == tipoTransaccionId &&
                            DbFunctions.TruncateTime(t.FechaAlta) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

            return result;
        }

        public TransaccionAutomatica CambiarEstatusConveniosVencido(int usuarioAltaId)
        {
            var result = _context.Convenio
                .Where(c => DbFunctions.TruncateTime(c.FechaFin) < DbFunctions.TruncateTime(DateTime.Now) &&
                            c.EstatusConvenioId != (int)EstatusConvenioDomain.EstatusConvenioEnum.Cancelado).ToList();


            for (int x = 0; x <= result.Count - 1; x++)
            {
                result[x].EstatusConvenioId = (int) EstatusConvenioDomain.EstatusConvenioEnum.Cancelado;
                result[x].FechaCambio = DateTime.Now;
                result[x].UsuarioCambioId = usuarioAltaId;
            }

            var transaccionAutomatica = new TransaccionAutomatica()
            {
                TipoTransaccionAutomaticaId = (int)TiposTransaccionesAutomaticasDomain.TiposTransaccionesAutomaticasDomainEnum.CambiarEstatusConvenio,
                FechaAlta = DateTime.Now,
                UsuarioAltaId = usuarioAltaId
            };
            
            _context.Entry(transaccionAutomatica).State = EntityState.Added;

            _context.SaveChanges();

            return transaccionAutomatica;

        }

        #endregion

    } // public class TransaccionesAutomaticasDomain
} // namespace ComiteAgua.Domain.Transacciones_Automaticas