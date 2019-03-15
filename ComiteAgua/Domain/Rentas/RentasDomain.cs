using ComiteAgua.Models;
using ComiteAgua.Models.Rentas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Rentas
{
    public class RentasDomain
    {

        #region * Constructor generado por Comité Agua *
        public RentasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public RentasDomain(DataContext applicationDbContext)
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

        public void Guardar(Renta model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }//public void Guardar(Renta model)

        #endregion

    }//public class RentasDomain
}//namespace ComiteAgua.Domain.Rentas