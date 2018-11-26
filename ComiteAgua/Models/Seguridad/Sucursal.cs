using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Seguridad
{
    public class Sucursal
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int SucursalId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }        
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }        

        public List<MultiComiteSucursal> MultiComitesSucursales { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Sucursal

} // namespace ComiteAgua.Models.Seguridad