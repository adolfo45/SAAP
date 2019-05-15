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
        public void Eliminar(int gastoId)
        {
            var gasto = _context.Gasto
                .Where(g => g.GastoId == gastoId).FirstOrDefault();

            _context.Entry(gasto).State = EntityState.Deleted;

            _context.SaveChanges();
        }        
        public List<Gasto> ObtenerGastos(DateTime fecha)
        {
            var result = _context.Gasto
               .Where(i => DbFunctions.TruncateTime(i.FechaAlta) == DbFunctions.TruncateTime(fecha))
               .ToList();

            return result;
        }
        public Gasto ObtenerGasto(int gastoId)
        {
            var gasto = _context.Gasto
                .Include(g => g.ArchivoGasto)
                .Where(g => g.GastoId == gastoId).FirstOrDefault();

            return gasto;
        }
        public void Gurdar(Gasto model)
        {                         
                _context.Entry(model).State = EntityState.Added;            

                _context.SaveChanges();            
        }
        #endregion

    } // public class GastosDomain

} // namespace ComiteAgua.Domain