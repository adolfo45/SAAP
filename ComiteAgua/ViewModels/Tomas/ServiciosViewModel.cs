using ComiteAgua.Models;
using ComiteAgua.Models.Servicios;
using ComiteAgua.ViewModels.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Tomas
{
    public class ServiciosViewModel
    {

        #region * Constructor generado por Comité Agua *

        public ServiciosViewModel()
        {
            Asuntos = new List<Asunto>();
            AsuntosDescripcion = new List<Models.AsuntoDescripcion>();
        }

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        
        public string ServicioId { get; set; }

        [Display(Name = "Asunto")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string AsuntoId { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string AsuntoDescripcionId { get; set; }        
        public string TomaId { get; set; }        
        public bool Propietario { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string NombreReporto { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Direccion { get; set; }

        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Colonia { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Ubicación Servicio")]
        [Required(ErrorMessage = "La {0} es requerido.")]
        public string UbicacionServicio { get; set; }

        [Display(Name = "Otro")]
        public string Otro { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public string RutaArchivo { get; set; }
        public bool Imprimir { get; set; }
        public List<Asunto> Asuntos { get; set; }
        public List<AsuntoDescripcion> AsuntosDescripcion { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public int EstatusServicioId { get; set; }
        public List<EvidenciaServiciosViewModel> EvidenciaServicios { get; set; }
        public int Folio { get; set; }

        public List<Servicio> NoIniciado { get; set; }
        public List<Servicio> EnProceso { get; set; }
        public List<Servicio> Concluido { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class ServiciosViewModel
} // namespace ComiteAgua.ViewModels.Tomas