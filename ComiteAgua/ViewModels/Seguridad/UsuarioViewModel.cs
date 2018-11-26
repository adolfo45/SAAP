using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Seguridad
{
    public class UsuarioViewModel
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Password { get; set; }
        public List<Usuario> Usuarios { get; set; }       
        public string Nombre { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Usuario { get; set; }
        public IList<RolViewModel> RolesVM { get; set; }
        public IEnumerable<Rol> Roles { get; set; }
        public bool Seleccionado { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public List<int> RolIds { get; set; }
        public string UrlRetorno { get; set; }
        public PersonaSeguridadViewModel Persona { get; set; }
        public string Accion { get; set; }
        public string Titulo
        {
            get
            {
                if (Accion == ComiteAgua.Accion.Agregar)
                    return "Agregar Usuario";
                if (Accion == ComiteAgua.Accion.Editar)
                    return "Editar Usuario";
                if (Accion == ComiteAgua.Accion.Consultar)
                    return "Cosultar Usuario";
                return "Error";
            } // get
        } // public string Titulo

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class UsuarioViewModel
}//namespace ComiteAgua.ViewModels.Seguridad