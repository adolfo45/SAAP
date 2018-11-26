using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ConceptosConvenioDomain
    {

        #region * Constructor generado por Comité Agua *

        public ConceptosConvenioDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ConceptosConvenioDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum ConceptosConvenioDomainEnum
        {
            TomaNueva = 1,
            SuministroAgua

        } // public enum ConceptosConvenioDomainEnum

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public List<ConceptoConvenio> ObtenerConceptosConvenio()
        {
            var result = _context.ConceptoConvenio.ToList();

            return result;
        }

        #endregion

    } // public class ConceptosConvenioDomain

} // namespace ComiteAgua.Domain