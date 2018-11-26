using ComiteAgua.Models;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Seguridad
{
    public class RolesDomain
    {

        #region * Constructor generado por Adserti *

        public RolesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public RolesDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        public enum RolesEnum
        {

            Secretario = 1,
            Tesorero = 2,
            Presidente = 3,
            Notificador = 4,
            AuxiliarAdministrativo = 5,
            Fontanero = 6,
            Sistemas = 7

        }

        #endregion

        #region * Variables declaradas por Adserti *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Adserti 

        #endregion

        #region * Eventos generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *
        public IEnumerable<Rol> ObtenerRoles()
        {
            var roles = _context.Rol;

            return roles;
        }
        #endregion

    } // public class RolesDomain
} // namespace ComiteAgua.Domain.Seguridad