using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Notificaciones
{
    public class Reporte
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ReporteId { get; set; }
        public int TomaId { get; set; }
        public Toma Toma { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public string UrlImagen { get; set; }
        public bool Revisado { get; set; }
        public bool Concluido { get; set; }
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

    } // public class Reporte
} // namespace ComiteAgua.Models.Notificaciones