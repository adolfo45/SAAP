using ComiteAgua.Models;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain.Seguridad
{
    public class PersonasSeguridadDomain
    {

        #region * Constructor generado por Comité Agua *

        public PersonasSeguridadDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public UsuariosDomain(DataContext applicationDbContext)

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

        public void Editar(PersonaSeguridad model, bool save = true)
        {
            var bd = _context.PersonaSeguridad.Where(ps => ps.PersonaId == model.PersonaId).FirstOrDefault();
            bd.Nombre = model.Nombre;
            bd.ApellidoPaterno = model.ApellidoPaterno;
            bd.ApellidoMaterno = model.ApellidoMaterno;
            bd.Telefono = model.Telefono;
            bd.CorreoElectronico = model.CorreoElectronico;
            bd.UsuarioCambioId = model.UsuarioCambioId;
            bd.FechaCambio = DateTime.Now;
            bd.FechaNacimiento = model.FechaNacimiento;

            if (save) _context.SaveChanges();
        }
        public List<PersonaSeguridad> ObtenerPersonasSeguridad()
        {
            var result = _context.PersonaSeguridad
                .Where(x => x.AutorizaConvenios == true).ToList();

            return result;
        }
        public PersonaSeguridad ObtenerPersonaSeguridad(int personaId)
        {
            var result = _context.PersonaSeguridad
                .Where(x => x.PersonaId == personaId).FirstOrDefault();

            return result;
        }
        public IQueryable<PersonaSeguridad> ObtenerNotificadores()
        {
            var result = _context.PersonaSeguridad
                    .Include(x => x.Usuarios)
                .Where(x => x.Usuarios.Any(u => u.UsuarioRol.Any(r => r.RolId == (int)RolesDomain.RolesEnum.Notificador)));

            return result;
        }

        #endregion

    } // public class PersonasSeguridadDomain

} // namespace ComiteAgua.Domain.Seguridad