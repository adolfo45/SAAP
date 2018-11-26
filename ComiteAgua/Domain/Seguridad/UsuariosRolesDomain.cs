using ComiteAgua.Models;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Seguridad
{
    public class UsuariosRolesDomain
    {
        #region * Constructor generado por Comité Agua *
        public UsuariosRolesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public UsuariosRolesDomain(DataContext applicationDbContext)
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

        public void Agregar(List<UsuarioRol> model)
        {
            foreach (var item in model)
            {
                _context.Entry(item).State = EntityState.Added;
            }

            _context.SaveChanges();
        }
        public void Eliminar(int usuarioId, bool save = true)
        {
            var usuarioRoles = _context.UsuarioRol
                .Where(ur => ur.UsuarioId == usuarioId).ToList();

            foreach (var item in usuarioRoles)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }

            if (save) _context.SaveChanges();
        }

        #endregion
    }//public class UsuariosRolesDomain
}//namespace ComiteAgua.Domain.Seguridad