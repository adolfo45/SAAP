using ComiteAgua.Global;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Persona
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int PersonaId { get; set; }
        public int PersonalidadJuridicaId { get; set; }
        public PersonalidadJuridica PersonalidadJuridica { get; set; }
        public int SucursalId { get; set; }        
        public int MultiComiteId { get; set; }
        public MultiComiteSucursal MultiComiteSucursal { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }        
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }        

        public PersonaFisica PersonaFisica { get; set; }
        public PersonaMoral PersonaMoral { get; set; }
        public List<Propietario> Propietario { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Persona

} // namespace ComiteAgua.Models