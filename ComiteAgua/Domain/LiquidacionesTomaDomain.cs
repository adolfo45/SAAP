using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class LiquidacionesTomaDomain
    {

        #region * Constructor generado por Comité Agua *

        public LiquidacionesTomaDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public TomasDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        public enum LiquidacionesTomaEnum
        {
            Contado = 1,
            Convenio,
            TomaExistente,
            TomaNueva
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

        public List<LiquidacionToma> ObtenerLiquidacionesToma()
        {
            var result = _context.LiquidacionToma.ToList();

            return result;
        } //  public List<LiquidacionToma> ObtenerLiquidacionesToma()

        #endregion

    } // public class LiquidacionesTomaDomain

} // namespace ComiteAgua.Domain