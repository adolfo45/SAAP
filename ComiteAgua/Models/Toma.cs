using ComiteAgua.Models.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Toma
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int TomaId { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get;set;}
        public int? LiquidacionTomaId { get; set; }
        public LiquidacionToma LiquidacionToma { get; set; }
        public int PropietarioId { get; set; }
        public Propietario Propietario { get; set; }
        public int? DireccionId { get; set; }
        public Direccion Direccion { get; set; }        
        public int Folio { get; set; }
        public string Observaciones { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public bool? Pasiva { get; set; } 
        
        public List<Pago> Pago { get; set; }
        public List<Convenio> Convenio { get; set; }
        public List<PeriodoPago> PeriodoPago { get; set; }
        public List<Servicio> Servicio { get; set; }        
        public List<Notificacion> Notificacion { get; set; }
        public List<Reporte> Reporte { get; set; }
        public List<Constancia> Constancia { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Toma

} // namespace ComiteAgua.Models