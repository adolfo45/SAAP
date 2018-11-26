using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity.Core.Objects;

namespace ComiteAgua.Domain
{
    public class GastosDomain
    {

        #region * Constructor generado por Comité Agua *

        public GastosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public GastosDomain(DataContext applicationDbContext)

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

        public List<Gasto> ObtenerGastos(DateTime fecha, DateTime fechaFin)
        {                       
            var result = _context.Gasto
                .Where(i => DbFunctions.TruncateTime(i.FechaAlta) >= DbFunctions.TruncateTime(fecha) && DbFunctions.TruncateTime(i.FechaAlta) <= DbFunctions.TruncateTime(fechaFin))
                .ToList();

            return result;
        }

        public List<Gasto> ObtenerGastos(DateTime fecha)
        {
            var result = _context.Gasto
               .Where(i => DbFunctions.TruncateTime(i.FechaAlta) == DbFunctions.TruncateTime(fecha))
               .ToList();

            return result;
        }

        public void Gurdar(Gasto model)
        {                         
                _context.Entry(model).State = EntityState.Added;            

                _context.SaveChanges();            
        }

        #endregion

    } // public class GastosDomain

} // namespace ComiteAgua.Domain