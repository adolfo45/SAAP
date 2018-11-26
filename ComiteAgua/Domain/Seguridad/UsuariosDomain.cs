using ComiteAgua.Models;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ComiteAgua.Domain.Seguridad
{
    public class UsuariosDomain
    {

        #region * Constructor generado por Comité Agua *       

        public UsuariosDomain(DataContext applicationDbContext)
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

        public void Agregar(Usuario model)
        {

            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        } //public void Guardar(Usuario model) 

        public void Bloquear(int usuarioId,bool bloquear)
        {
            var usuario = _context.Usuario.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();
            usuario.Activo = bloquear;
            usuario.FechaCambio = DateTime.Now;
            usuario.UsuarioCambioId = usuarioId;
            _context.SaveChanges();
        }
        public void Editar(Usuario model, bool save = true)
        {
            var bd = _context.Usuario.Where(u => u.UsuarioId == model.UsuarioId).FirstOrDefault();
            bd.UserName = model.UserName;
            bd.Password = model.Password;
            bd.UsuarioCambioId = model.UsuarioCambioId;
            bd.FechaCambio = DateTime.Now;

            if (save) _context.SaveChanges();
        }
        public Usuario ObtenerUsuario(string usuario)
        {
            var result = _context.Usuario    
                .Include(u => u.UsuarioRol)
                .Include(u => u.Persona.MultiComite.MultiComitesSucursales)
                .Where(u => u.UserName.Equals(usuario) &&
                            u.Activo).FirstOrDefault();

            return result;

        } // public Usuario ObtenerUsuario(string usuario, string contraseña)       
        public Usuario ObtenerUsuarioPersona(int usuarioId)
        {
            var result = _context.Usuario
                .Include(u => u.Persona)
                .Where(u => u.UsuarioId == usuarioId).FirstOrDefault();

            return result;
        }// public Usuario ObtenerUsuarioPersona(int usuarioId)
        public List<string> ObtenerTokenFontaneros()
        {
            var tokens = _context.Usuario
                .Where(u => u.UsuarioRol.Any(r => r.RolId == (int)RolesDomain.RolesEnum.Fontanero) &&
                            !string.IsNullOrEmpty(u.TokenFirebase));

            return tokens.Select(t => t.TokenFirebase).ToList();
        }
        public IQueryable<Usuario> ObtenerUsuarios()
        {
            var usurios = _context.Usuario
                .Include(u => u.Persona)
                .Include(u => u.UsuarioRol.Select(r => r.Rol));

            return usurios;
        }//public IQueryable<Usuario> ObtenerUsuarios(int multiEmpresaId)
        public Usuario ObtenerUsuario(int usuarioId)
        {
            var usuario = _context.Usuario
                .Include(u => u.Persona)
                .Include(u => u.UsuarioRol.Select(ur => ur.Rol))
                .Where(u => u.UsuarioId == usuarioId).SingleOrDefault();

            return usuario;
        } // public Usuario ObtenerUsuario(int usuarioId)   

        public bool ValidarUsuario(int usuarioId, string usuarios)
        {
            var usuario = _context.Usuario.Where(u => u.UserName == usuarios &&
                                                      u.UsuarioId != usuarioId).FirstOrDefault();

            return usuario != null ? true : false;
        }

        #endregion

    } // public class UsuariosDomain

} // namespace ComiteAgua.Domain.Seguridad