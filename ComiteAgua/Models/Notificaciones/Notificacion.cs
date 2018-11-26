using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Notificaciones
{
    public class Notificacion
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int NotificacionId { get; set; }
        public int TipoNotificacionId { get; set; }
        public TipoNotificacion TipoNotificacion { get; set; }
        public int TomaId { get; set; }
        public Toma Toma { get; set; }
        public int? OpcionNotificacionId { get; set; }
        public CtoOpcionesNotificacion CtoOpcionesNotificacion { get; set; }
        public decimal TotalPagar { get; set; }       
        public DateTime FechaEntrega { get; set; }      
        public int? UsuarioNotificoId { get; set; }
        public Usuario UsuarioNotifico { get; set; }
        public bool Activa { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public Usuario UsuarioAlta { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public Usuario UsuarioCambio { get; set; }

        public List<Pago> Pagos { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Notificacion

} // namespace ComiteAgua.Models.Notificaciones