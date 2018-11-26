using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Servicios
{
    public class EvidenciaServicio
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int EvidenciaServicioId { get; set; }
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
        public string UrlImagen { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public Usuario UsuarioAlta { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public Usuario UsuarioCambio { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class EvidenciaServicio
} // namespace ComiteAgua.Models.Servicios