using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class EstatusConvenioDomain
    {

        #region * Constructor generado por Comité Agua *

        public EstatusConvenioDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public EstatusConvenioDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum EstatusConvenioEnum
        {
            Activo = 1,
            Cancelado,
            Concluido
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

        #endregion

    } // public class EstatusConvenioDomain

} // namespace ComiteAgua.Domain