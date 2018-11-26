using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class TarifasDomain
    {

        #region * Constructor generado por Comité Agua *

        public TarifasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public TarifasDomain(DataContext applicationDbContext)

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        private readonly DataContext _context;

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Eventos generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public void GuardarTarifas(Tarifa model)
        {
            
            
            _context.Entry(model).State = EntityState.Added;
           

            _context.SaveChanges();
        }       
        public List<Tarifa> ObtenerTarifasPeriodo(DateTime periodoInicio, DateTime periodoFin, int categoriaId)
        {
            var result = _context.Tarifa
                .Where(t => t.MesAno >= periodoInicio.Year &
                            t.MesAno <= periodoFin.Year &
                            t.CategoriaId == categoriaId);

            return result.ToList();
        }
        public List<Tarifa> ObtenerTarifasAbono(DateTime periodoInicio, int categoriaId)
        {
            var result = _context.Tarifa
            //TODO INT *
                .Where(t => t.MesAno >= periodoInicio.Year &
                            t.CategoriaId == categoriaId).ToList();

            return result;
        }

        public Tarifa ObtenerTarifa(int categoriaId, int mesAno)
        {
            var tarifa = _context.Tarifa
                .Where(c => c.CategoriaId == categoriaId &&
                            c.MesAno == mesAno).FirstOrDefault();

            return tarifa;
        }

        #endregion

    } // public class TarifasDomain

} // namespace ComiteAgua.Domain