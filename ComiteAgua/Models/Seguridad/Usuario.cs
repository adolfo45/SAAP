using ComiteAgua.Global;
using ComiteAgua.Models.Notificaciones;
using ComiteAgua.Models.Recibos;
using ComiteAgua.Models.Rentas;
using ComiteAgua.Models.Servicios;
using ComiteAgua.Models.TransaccionesAutomaticas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models.Seguridad
{
    public class Usuario
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }
        public PersonaSeguridad Persona { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public short? IntentosFallidosLogin { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }
        public bool Activo { get; set; }
        public string TokenFirebase { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }        
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        public List<UsuarioRol> UsuarioRol { get; set; }
        public List<Notificacion> NotificacionesAlta { get; set; }
        public List<Notificacion> NotificacionesCambio { get; set; }
        public List<Notificacion> NotificacionesNotifico { get; set; }
        public List<TransaccionAutomatica> TransaccionesAlta { get; set; }
        public List<TransaccionAutomatica> TransaccionesCambio { get; set; }
        public List<Recibo> ReciboAlta { get; set; }
        public List<Recibo> ReciboCambio { get; set; }
        public List<Reporte> ReporteAlta { get; set; }
        public List<Reporte> ReporteCambio { get; set; }
        public List<EvidenciaServicio> EvidenciaServicioAlta { get; set; }
        public List<EvidenciaServicio> EvidenciaServicioCambio { get; set; }
        public List<Servicio> ServicioAlta { get; set; }
        public List<Servicio> ServicioCambio { get; set; }
        public List<DescuentoProntoPago> DescuentoProntoPagoAlta { get; set; }
        public List<DescuentoProntoPago> DescuentoProntoPagoCambio { get; set; }
        public List<Constancia> ConstanciaAlta { get; set; }
        public List<Constancia> ConstanciaCambio { get; set; }
        public List<TiposConstancia> TiposConstanciaAlta { get; set; }
        public List<TiposConstancia> TiposConstanciaCambio { get; set; }
        public List<Renta> RentaAlta { get; set; }
        public List<Renta> RentaCambio { get; set; }
        public List<TipoRenta> TipoRentaAlta { get; set; }
        public List<TipoRenta> TipoRentaCambio { get; set; }
        public List<CambioPropietario> CambioPropietarioAlta { get; set; }
        public List<CambioPropietario> CambioPropietarioCambio { get; set; }
        public List<TipoPersona> TipoPersonaAlta { get; set; }
        public List<TipoPersona> TipoPersonaCambio { get; set; }
        public List<CambioPropietarioPersonaMoral> CambioPropietarioMoralAlta { get; set; }
        public List<CambioPropietarioPersonaMoral> CambioPropietarioMoralCambio{ get; set; }
        public List<ArchivoPersona> ArchivoPersonaAlta { get; set; }
        public List<ArchivoPersona> ArchivoPersonaCambio { get; set; }
        public List<TipoArchivo> TipoArchivoAlta { get; set; }
        public List<TipoArchivo> TipoArchivoCambio { get; set; }
        public List<TipoDescuento> TipoDescuentoAlta { get; set; }
        public List<TipoDescuento> TipoDescuentoCambio { get; set; }
        public List<Descuento> DescuentoAlta { get; set; }
        public List<Descuento> DescuentoCambio { get; set; }
        public List<DescuentoPago> DescuentoPagoAlta { get; set; }
        public List<DescuentoPago> DescuentoPagoCambio { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Usuario

} // namespace ComiteAgua.Models.Seguridad