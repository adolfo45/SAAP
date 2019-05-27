using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class CambioPropietarioPersonaMoralDomain
    {
        #region * Constructor generado por Comité Agua *
        public CambioPropietarioPersonaMoralDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public CambioPropietarioPersonaMoralDomain(DataContext applicationDbContext)
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
        public void Agregar(CambioPropietarioPersonaMoral model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        #endregion
    }//public class CambioPropietarioPersonaMoralDomain
}//namespace ComiteAgua.Domain