using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class AsuntosDescripcionDomain
    {

        #region * Constructor generado por Comité Agua *

        public AsuntosDescripcionDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public AsuntosDescripcionDomain(DataContext applicationDbContext)

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

        public List<AsuntoDescripcion> ObtenerAsuntosDescripcion(int asuntoId)
        {
            var result = _context.AsuntoDescripcion
                .Where(a => a.AsuntoId == asuntoId).ToList();

            return result;
        }       

        #endregion

    } // public class AsuntosDescripcionDomain
} // namespace ComiteAgua.Domain