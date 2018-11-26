using ComiteAgua.Models;
using ComiteAgua.Models.Direcciones;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class DireccionesDomain
    {

        #region * Constructor generado por Comité Agua *

        public DireccionesDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public AccesoriosDomain(ApplicationDbContext applicationDbContext)

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

        public void Guardar(Direccion model)
        {
            if (model.DireccionId > 0)
            {
                var bd = _context.Direccion.Where(d => d.DireccionId == model.DireccionId).FirstOrDefault();

                bd.CalleId = model.CalleId;
                bd.NumInt = model.NumInt;
                bd.NumExt = model.NumExt;
                bd.ColoniaId = model.ColoniaId;
                bd.TipoCalleId = model.TipoCalleId;
                bd.FechaCambio = DateTime.Now;
                bd.UsuarioCambioId = model.UsuarioAltaId;
            }
            else
            {
                _context.Entry(model).State = EntityState.Added;
            }

            _context.SaveChanges();
        } // public void Guardar(Propietario model)       

        public IQueryable<Colonias> ObtenerColonias()
        {
            var colonias = _context.Colonias;

            return colonias;
        }

        public IQueryable<Calles> ObtenerCalles()
        {
            var calles = _context.Calles;

            return calles;
        }

        public IQueryable<TiposCalle> ObtenerTiposCalle()
        {
            var tipoCalle = _context.TiposCalle;

            return tipoCalle;
        }

        #endregion

    } // public class DireccionesDomain

} // namespace ComiteAgua.Domain