using ComiteAgua.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Seguridad
{
    public class PersonaSeguridad
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PersonaId { get; set; }
        public int MultiComiteId { get; set; }
        public MultiComite MultiComite { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? EstadoCivilId { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }
        public bool? AutorizaConvenios { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        public IList<Usuario> Usuarios { get; set; }
        public IList<Convenio> Convenios { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PersonaSeguridad

} // namespace ComiteAgua.Models.Seguridad