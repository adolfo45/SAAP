using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Seguridad
{
    public class MultiComiteSucursal
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        public int MultiComiteId { get; set; }  
        public MultiComite MultiComite { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }        
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }        

        public List<Persona> Personas { get; set; }
        public List<Gasto> Gastos { get; set; }

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class MultiComiteSucursal

} // namespace ComiteAgua.Models.Seguridad