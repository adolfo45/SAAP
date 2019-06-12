using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ComiteAgua.Domain
{
    public class DescuentosDomain
    {
        #region * Constructor generado por Comité Agua *
        public DescuentosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public DescuentosDomain(DataContext applicationDbContext)
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *
        public List<Descuento> Consultar(int TipoDescuentoId)
        {
            var descuentos = _context.Descuento
                .Include(d => d.TipoDescuento)
                .Where(d => d.TipoDescuentoId == TipoDescuentoId).ToList();

            return descuentos;
        }
        public void Guardar(Descuento model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        #endregion
    }//public class DescuentosDomain
}//namespace ComiteAgua.Domain.Catalogos