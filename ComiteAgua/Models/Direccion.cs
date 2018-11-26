using ComiteAgua.Models.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Direccion
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int DireccionId { get; set; }
        public string Calle { get; set; }
        public string NumInt { get; set; }
        public string NumExt { get; set; }
        public string Colonia { get; set; }
        public int? CalleId { get; set; }
        public Calles Calles { get; set; }
        public int? TipoCalleId { get; set; }
        public TiposCalle TiposCalle { get; set; }
        public int? ColoniaId { get; set; }
        public Colonias Colonias { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        public List<Toma> Toma { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Direccion

} // namespace ComiteAgua.Models