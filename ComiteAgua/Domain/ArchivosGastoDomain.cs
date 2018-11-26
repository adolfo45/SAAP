using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ArchivosGastoDomain
    {

        #region * Constructor generado por Comité Agua *

        public ArchivosGastoDomain(DataContext applicationDbContext)
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

        public void Gurdar(ArchivoGasto model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }

        public List<ArchivoGasto> ObtenerArchivosGastos(int gastoId)
        {
            var result = _context.ArchivoGasto
                .Where(x => x.GastoId == gastoId)
                .ToList();

            return result;
        }

        #endregion

    } // public class ArchivosGastoDomain

} // namespace ComiteAgua.Domain