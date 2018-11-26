using ComiteAgua.Models;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Global
{
    public class PersonalidadJuridica
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PersonalidadJuridicaId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }       
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }        

        public List<Persona> Personas { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class PersonalidadJuridica

} // namespace ComiteAgua.Models