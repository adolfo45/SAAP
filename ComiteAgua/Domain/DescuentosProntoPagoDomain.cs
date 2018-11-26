using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class DescuentosProntoPagoDomain
    {
        #region * Constructor generado por Comité Agua *
        public DescuentosProntoPagoDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public DescuentosProntoPagoDomain(DataContext applicationDbContext)
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public void Agregar(DescuentoProntoPago model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        public IQueryable<DescuentoProntoPago> ConsultarDescuentos()
        {
            var descuentos = _context.DescuentoProntoPago;
            return descuentos;
        }
        public DescuentoProntoPago ConsultarDescuento(int descuentoId)
        {
            var descuento = _context.DescuentoProntoPago.Where(d => d.DescuentoId == descuentoId).FirstOrDefault();

            return descuento;
        }
        public DescuentoProntoPago ConsultarDescuentoVigente(DateTime periodoInicio, DateTime periodoFin)
        {
            var fechaCadenaInicio = "01/01/" + DateTime.Now.AddYears(1).Year;
            var fechaCadenaFin = "01/12/" + DateTime.Now.AddYears(1).Year;
            //var fechaCadenaInicio = "01/01/" + DateTime.Now.Year;
            //var fechaCadenaFin = "01/12/" + DateTime.Now.Year;
            var periodoInicioDescuento = Convert.ToDateTime(fechaCadenaInicio);
            var periodoFinDescuento = Convert.ToDateTime(fechaCadenaFin);
            var descuentoVigente = periodoInicio.Month == periodoInicioDescuento.Month &&
                                   periodoFin.Month == periodoFinDescuento.Month &&
                                   periodoInicio.Year == periodoInicioDescuento.Year &&
                                   periodoFin.Year == periodoFinDescuento.Year;
            if (descuentoVigente)
            {
                var descuentoProntoPago = _context.DescuentoProntoPago.Where(d => d.MesAno.Month == DateTime.Now.Month &&
                                                                        d.MesAno.Year == DateTime.Now.Year).FirstOrDefault();
                return descuentoProntoPago;
            }
            return null;
        }
        public void Editar(DescuentoProntoPago model)
        {
            var bd = _context.DescuentoProntoPago.Where(d => d.DescuentoId == model.DescuentoId).FirstOrDefault();

            bd.MesAno = model.MesAno;
            bd.MontoPoncentaje = model.MontoPoncentaje;
            bd.FechaCambio = DateTime.Now;
            bd.UsuarioCambioId = model.UsuarioCambioId;
            _context.SaveChanges();
        }
        public bool ValidarDescuento(int descuentoId, DateTime mesAno)
        {
            var descuento = _context.DescuentoProntoPago.Where(dp => dp.MesAno == mesAno &&
                                                                     dp.DescuentoId != descuentoId).FirstOrDefault();

            return descuento != null ? true : false;
        }

        #endregion
    }//public class DescuentosProntoPagoDomain
}//namespace ComiteAgua.Domain