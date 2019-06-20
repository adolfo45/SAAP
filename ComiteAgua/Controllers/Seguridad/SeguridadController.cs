using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Models;
using ComiteAgua.ViewModels.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsertiVS2017ClassLibrary;
using ComiteAgua.Models.Seguridad;

namespace ComiteAgua.Controllers.Seguridad
{
    public class SeguridadController : MessageControllerBase
    {
        #region * Constructor generado por Comité Agua *
        public SeguridadController()
        {
            _context = new DataContext();
        }
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        public ActionResult AdministrarUsuarios(int usuarioId = 0, string urlRetorno = null)
        {
            try
            {
                var usuariosDomain = new UsuariosDomain(_context);
                var usuarioViewModel = new UsuarioViewModel();
                var rolesDomain = new RolesDomain(_context);

                var roles = rolesDomain.ObtenerRoles();

                var usurioRolList = roles
                    .Select(up => new RolViewModel()
                    {
                        RolId = up.RolId,
                        Nombre = up.Nombre,
                    }).ToList();

                if (usuarioId > 0)
                {
                    var usuario = usuariosDomain.ObtenerUsuario(usuarioId);

                    usuarioViewModel = new UsuarioViewModel()
                    {
                        UsuarioId = usuarioId,
                        Usuario = usuario.UserName,
                        Password = usuario.Password,
                        Accion = Accion.Editar,
                        RolIds = usuario.UsuarioRol.Select(r => r.RolId).ToList(),
                        UrlRetorno = urlRetorno,
                        PersonaId = usuario.PersonaId,
                        Persona = new PersonaSeguridadViewModel()
                        {
                            PersonaId = usuario.Persona.PersonaId,
                            Nombre = usuario.Persona.Nombre,
                            ApellidoPaterno = usuario.Persona.ApellidoPaterno,
                            ApellidoMaterno = usuario.Persona.ApellidoMaterno,
                            CorreoElectroico = usuario.Persona.CorreoElectronico,
                            Telefono = usuario.Persona.Telefono,
                            FechaNacimiento = usuario.Persona.FechaNacimiento.ToString("yyyy-MM-dd")
                        }
                    };

                    for (int x = 0; x <= usurioRolList.Count - 1; x++)
                    {
                        if (usuario.UsuarioRol.Select(r => r.RolId).Contains(usurioRolList[x].RolId))
                        {
                            usurioRolList[x].Seleccionado = true;
                        }
                    }

                }
                else
                {
                    usuarioViewModel.Accion = Accion.Agregar;
                    usuarioViewModel.UrlRetorno = urlRetorno;
                }
                usuarioViewModel.RolesVM = usurioRolList;
                return View(usuarioViewModel);

            }
            catch (Exception ex)
            {
                ShowToastMessage("Error", ex.Message, ToastMessage.ToastType.Error);
                return RedirectToAction("Index", "Home");
            }
        }//public ActionResult AdministrarUsuarios(int usuarioId = 0, string urlRetorno = null)
        public ActionResult Usuarios()
        {
            try
            {
                var usuariosDomain = new UsuariosDomain(_context);
                var usuarios = usuariosDomain.ObtenerUsuarios();
                var usuarioViewModel = new UsuarioViewModel()
                {
                    Usuarios = usuarios.OrderBy(up => up.Persona.Nombre).ToList(),                    
                };

                return View(usuarioViewModel);
            }
            catch (Exception ex)
            {
                ShowToastMessage("Error", ex.Message, ToastMessage.ToastType.Error);
                return RedirectToAction("Index", "Home");
            }
        }//public ActionResult ConsultarUsuarios()            
        public ActionResult Guardar(UsuarioViewModel model)
        {
            var usuariosDomain = new UsuariosDomain(_context);
            var personasSeguridadDomain = new PersonasSeguridadDomain(_context);
            var usuariosRolesDomain = new UsuariosRolesDomain(_context);
            var usuarioRolList = new List<UsuarioRol>();

            var existe = usuariosDomain.ValidarUsuario(model.UsuarioId, model.Usuario);

            if (existe)
            {
                ShowToastMessage("Alerta", "Este usuario ya existe", ToastMessage.ToastType.Warning);
                return View("AdministrarUsuarios", model);
            }

            if (!model.RolesVM.Any(r => r.Seleccionado))
            {
                ShowToastMessage("Alerta", "Debe seleccionar al menos un rol", ToastMessage.ToastType.Warning);
                return View("AdministrarUsuarios", model);
            }

            foreach (var item in model.RolesVM.Where(r => r.Seleccionado))
            {
                var usuarioRol = new UsuarioRol()
                {
                    UsuarioId = model.UsuarioId,
                    RolId = item.RolId,
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };
                usuarioRolList.Add(usuarioRol);
            }

            if (model.UsuarioId > 0)
            {                
                var usuarios = new Usuario
                {
                    UsuarioId = model.UsuarioId,
                    UserName = model.Usuario,
                    Password = model.Password,
                    UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };
                usuariosDomain.Editar(usuarios, false);

                var persona = new PersonaSeguridad
                {
                    PersonaId = model.PersonaId,
                    Nombre = model.Persona.Nombre,
                    ApellidoPaterno = model.Persona.ApellidoPaterno,
                    ApellidoMaterno = model.Persona.ApellidoMaterno,
                    Telefono = model.Persona.Telefono,
                    CorreoElectronico = model.Persona.CorreoElectroico,
                    UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaNacimiento = Convert.ToDateTime(model.Persona.FechaNacimiento)
                };
                personasSeguridadDomain.Editar(persona, false);
                usuariosRolesDomain.Eliminar(model.UsuarioId, false);
                usuariosRolesDomain.Agregar(usuarioRolList);
            }
            else
            {
                var usuario = new Usuario
                {
                    UserName = model.Usuario,
                    Password = model.Password,
                    IntentosFallidosLogin = 0,                   
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    UsuarioRol = usuarioRolList,
                    Activo = true,
                    Persona = new PersonaSeguridad
                    {
                        MultiComiteId = Convert.ToInt32(Session["MultiComiteId"].ToString()),
                        Telefono = !string.IsNullOrEmpty(model.Persona.Telefono) ? AdsertiFunciones.FormatearNumero(model.Persona.Telefono) : string.Empty,
                        Nombre = AdsertiFunciones.FormatearTexto(model.Persona.Nombre),
                        ApellidoPaterno = AdsertiFunciones.FormatearTexto(model.Persona.ApellidoPaterno),
                        ApellidoMaterno = !string.IsNullOrEmpty(model.Persona.ApellidoMaterno) ? AdsertiFunciones.FormatearTexto(model.Persona.ApellidoMaterno) : string.Empty,
                        FechaNacimiento = Convert.ToDateTime(model.Persona.FechaNacimiento),
                        CorreoElectronico = !string.IsNullOrEmpty(model.Persona.CorreoElectroico) ? AdsertiFunciones.FormatearTexto(model.Persona.CorreoElectroico) : string.Empty,
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                        FechaAlta = DateTime.Now                       
                    }
                };

                usuariosDomain.Agregar(usuario);               
            }            

            return RedirectToAction("Usuarios");
        }//public ActionResult Guardar(UsuarioViewModel model)
        public ActionResult UsuarioBoquearDesbloquear(int usuarioId, bool bloquear)
        {
            var usuariosDomain = new UsuariosDomain(_context);
            usuariosDomain.Bloquear(usuarioId, bloquear);
            return RedirectToAction("Usuarios");
        }//public ActionResult UsuarioBoquearDesbloquear(int usuarioId, bool bloquear)

        #endregion

        #region * Métodos creados por Comité Agua *
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        } // protected override void Dispose(bool disposing)
        #endregion
                
    }
}