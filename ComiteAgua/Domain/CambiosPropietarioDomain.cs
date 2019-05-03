using ComiteAgua.Models;
using ComiteAgua.ViewModels.Tomas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class CambiosPropietarioDomain
    {
        #region * Constructor generado por Comité Agua *
        public CambiosPropietarioDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public CambiosPropietarioDomain(DataContext applicationDbContext)
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
        public void Agregar(CambioPropietario model)
        {
            _context.Entry(model).State = EntityState.Added;
            _context.SaveChanges();
        }//public void Agregar(PropietariosPersonaFisicaViewModel model)
        #endregion
    }//public class CambiosPropietarioDomain
}//namespace ComiteAgua.Domain