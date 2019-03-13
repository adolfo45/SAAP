using ComiteAgua.Models.Direcciones;
using ComiteAgua.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Constancia
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 
        public int ConstanciaId { get; set; }
        public int? TomaId { get; set; }
        public Toma Toma { get; set; }
        public int TipoConstanciaId { get; set; }
        public TiposConstancia TiposConstancia { get; set; }
        public string Propietario { get; set; }
        public int? TipoCalleId { get; set; }
        public TiposCalle TiposCalle { get; set; }
        public int? CalleId { get; set; }
        public Calles Calles { get; set; }
        public int? ColoniaId { get; set; }
        public Colonias Colonias { get; set; }
        public string NoInt { get; set; }
        public string NoExt { get; set; }
        public string FechaLetra { get; set; }     
        public bool ReciboImpreso { get; set; }        
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public Usuario UsuarioAlta { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }
        public Usuario UsuarioCambio { get; set; }

        public List<Pago> Pago { get; set; }
        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion
    }//public class Constancia
}//namespace ComiteAgua.Models