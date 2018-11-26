using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class AsuntosDomain
    {

        #region * Constructor generado por Comité Agua *

        public AsuntosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public AsuntosDomain(DataContext applicationDbContext)

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

        public List<Asunto> ObtenerAsuntos()
        {
            var result = _context.Asunto.ToList();

            return result;
        } // public List<Asunto> ObtenerAsuntos()

        #endregion

    } // public class AsuntosDomain
} // namespace ComiteAgua.Domain