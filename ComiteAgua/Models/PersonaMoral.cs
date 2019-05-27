using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class PersonaMoral
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }        
        public string CorreoElectronico { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }


        public List<CambioPropietarioPersonaMoral> CambioPropietarioPersonaMoral { get; set; }
        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PersonaMoral

} // namespace ComiteAgua.Models