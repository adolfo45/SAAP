using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class PeriodosPagoConvenioDomain
    {

        #region * Constructor generado por Comité Agua *

        public PeriodosPagoConvenioDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ConceptosConvenioDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum PeriodosPagoConvenioEnum
        {
            Semanal = 1,
            Quincenal,
            Mensual
        }

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public List<PeriodoPagoConvenio> ObtenerPeriodosConvenio()
        {
            var result = _context.PeriodoPagoConvenio.ToList();

            return result;
        }

        public PeriodoPagoConvenio ObtenerPeriodoPagoConvenio(int periodoPagoConvenioId)
        {
            var result = _context.PeriodoPagoConvenio
                .Where(x => x.PeriodoPagoConvenioId == periodoPagoConvenioId).FirstOrDefault();

            return result;
        }

        #endregion

    } // public class PeriodosPagoConvenioDomain

} // namespace ComiteAgua.Domain