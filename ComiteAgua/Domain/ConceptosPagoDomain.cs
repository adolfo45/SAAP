using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ConceptosPagoDomain
    {

        #region * Constructor generado por Comité Agua *

        public ConceptosPagoDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ConceptosPagoDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum ConceptosPagoDomainEnum
        {
            SuministroAgua = 1,
            Abono,
            Convenio,
            TomaNueva,
            Constancia
        } // ConceptosPagoDomainEnum

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

    } // public class ConceptosPagoDomain

} // namespace ComiteAgua.Domain