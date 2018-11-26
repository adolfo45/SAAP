using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Transacciones_Automaticas
{
    public class TiposTransaccionesAutomaticasDomain
    {

        #region * Constructor generado por Comité Agua *

        public TiposTransaccionesAutomaticasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public TiposTransaccionesAutomaticasDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum TiposTransaccionesAutomaticasDomainEnum
        {
            CambiarEstatusConvenio = 1

        } // public enum TiposTransaccionesAutomaticasDomainEnum

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class TiposTransaccionesAutomaticasDomain
} // namespace ComiteAgua.Domain.Transacciones_Automaticas