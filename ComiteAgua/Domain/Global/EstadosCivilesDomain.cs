using ComiteAgua.Global;
using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Global
{
    public class EstadosCivilesDomain
    {

        #region * Constructor generado por Comité Agua *

        public EstadosCivilesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public EstadosCivilesDomain(DataContext applicationDbContext)

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

        public List<EstadoCivil> ObtenerEstadosCiviles()
        {
            var result = _context.EstadoCivil.ToList();

            return result;
        }

        #endregion

    } // public class EstadosCivilesDomain

} // namespace ComiteAgua.Domain.Global