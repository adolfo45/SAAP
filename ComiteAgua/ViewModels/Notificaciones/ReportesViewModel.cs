using ComiteAgua.Models.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.ViewModels.Notificaciones
{
    public class ReportesViewModel
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
       
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime FechaCambio { get; set; }
        public int UsuarioCambioId { get; set; }        
        public string TabAutorizacionActivo { get; set; }
        public string TabPendienteActivo { get; set; }
        public string TabConcluidoActivo { get; set; }

        public List<Reporte> Autorizar { get; set; }
        public List<Reporte> Pendientes { get; set; }
        public List<Reporte> Concluidos { get; set; }
        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class ReportesViewModel
} // namespace ComiteAgua.ViewModels.Notificaciones