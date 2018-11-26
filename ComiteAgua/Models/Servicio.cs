using ComiteAgua.Models.Seguridad;
using ComiteAgua.Models.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Servicio
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int ServicioId { get; set; }
        public int AsuntoDescripcionId { get; set; }
        public AsuntoDescripcion AsuntoDescripcion { get; set; }
        public int EstatusServicioId { get; set; }
        public EstatusServicio EstatusServicio { get; set; }
        public int? TomaId { get; set; }
        public Toma Toma { get; set; }
        public bool Propietario { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Telefono { get; set; }
        public string UbicacionServicio { get; set; }
        public string Otro { get; set; }
        public string Observaciones { get; set; }
        public string UrlArchivo { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public Usuario UsuarioAlta { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public Usuario UsuarioCambio { get; set; }

        public List<EvidenciaServicio> EvidenciaServicio { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Servicio
} // namespace ComiteAgua.Models