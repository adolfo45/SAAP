using ComiteAgua.Models.Seguridad;
using ComiteAgua.ViewModels.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Notificaciones
{
    public class NotificacionesViewModel
    {

        #region * Constructor generado por Adserti *

        #endregion

        #region * Enumeraciones declaradas por Adserti *

        #endregion

        #region * Variables declaradas por Adserti *

        #endregion

        #region * Propiedades declaradas por Adserti 

        [Display(Name = "Notificador")]
        public int NotificacionId { get; set; }
        public int TipoNotificacionId { get; set; }
        public string TipoNotificacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public string FechaCambio { get; set; }
        public int UsuarioCambioId { get; set; }
        public int TomaId { get; set; }
        public bool Activa { get; set; }
        public string TotalPagar { get; set; }
        public int UsuarioNotificoId { get; set; }
        public string UsuarioNotifico { get; set; }

        [Display(Name = "Fecha Entrega Inicio")]
        public string FechaInicioFiltro { get; set; }

        [Display(Name = "Fecha Entrega Fin")]
        public string FechaFinFiltro { get; set; }

        [Display(Name = "Fecha Pago Inicio")]
        public string FechaPagoInicioFiltro { get; set; }

        [Display(Name = "Fecha Pago Fin")]
        public string FechaPagoFinFiltro { get; set; }
        public int Folio { get; set; }
        public string Direccion { get; set; }
        public string Categoria { get; set; }
        public string NoPagos { get; set; }
        public string Porcentaje { get; set; }   
        public string Total { get; set; }
        public DateTime FechaPago { get; set; }
        public string NoRecibo { get; set; }
        public string Observaciones { get; set; }
        public string OpcionNotificacion { get; set; }
        public IList<PersonaSeguridadViewModel> Notificadores { get; set; }


        #endregion

        #region * Eventos generados por Adserti *

        #endregion

        #region * Métodos creados por Adserti *

        #endregion

    } // public class NotificacionesViewModel
} // namespace ComiteAgua.ViewModels.Notificaciones