﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComiteAgua.Models
{
    public class Descuento
    {

        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        public int DescuentoId { get; set; }
        public string Nombre { get; set; }
        public int ModoDescuentoId { get; set; }
        public ModoDescuento ModoDescuento { get; set; }
        public DateTime? PeriodoDescuentoInicio { get; set; }
        public DateTime? PeriodoDescuentoFin { get; set; }
        public DateTime? MesAnoPago { get; set; }
        public decimal Total { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public int UsuarioAltaId { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? UsuarioCambioId { get; set; }

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        #endregion

    } // public class Descuento

} // namespace ComiteAgua.Models