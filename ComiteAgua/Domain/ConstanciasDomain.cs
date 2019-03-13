using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComiteAgua.ViewModels.Constancias;

namespace ComiteAgua.Domain
{
    public class ConstanciasDomain
    {
        #region * Constructor generado por Comité Agua *
        public ConstanciasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public ConstanciasDomain(DataContext applicationDbContext)
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *
        
        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        public decimal Costo = 250;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public void EditarConstanciaReciboImpreso(int constanciaId, int usuarioId)
        {
            var bd = _context.Constancia
                .Where(c => c.ConstanciaId == constanciaId).FirstOrDefault();

            bd.ReciboImpreso = true;
            bd.FechaCambio = DateTime.Now;
            bd.UsuarioCambioId = usuarioId;
            _context.SaveChanges();
        }//public void EditarConstanciaReciboImpreso(int constanciaId, int usuarioId)
        public void GuardarConstancia(Constancia model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }//public void GuardarConstancia(Constancia model)
        public List<Toma> ObtenerTomas(int? folio, string propietario, string calle)
        {
            var tomas = _context.Toma
                .Include(t => t.Propietario)
                .Include(t => t.Propietario.Persona)
                .Include(t => t.Propietario.Persona.PersonaFisica)
                .Include(t => t.PeriodoPago)
                .Include(t => t.Direccion)
                .Include(t => t.Direccion.TiposCalle)
                .Include(t => t.Direccion.Calles)
                .Include(t => t.Direccion.Colonias)
                .Include(t => t.Constancia);

            if (folio != null)
            {
                tomas = tomas.Where(t => t.Folio == folio);
            }
            if (!string.IsNullOrEmpty(propietario))
            {
                tomas = tomas.Where(t => (t.Propietario.Persona.PersonaFisica.Nombre.Trim() + " " +
                                           t.Propietario.Persona.PersonaFisica.ApellidoPaterno.Trim() + " " +
                                           t.Propietario.Persona.PersonaFisica.ApellidoMaterno.Trim()).Trim().Contains(propietario));
            }
            if (!string.IsNullOrEmpty(calle))
            {
                tomas = tomas.Where(t => (t.Direccion.TiposCalle.Nombre.Trim() + " " +
                                         t.Direccion.Calles.Nombre.Trim()).Trim().Contains(calle));
            }
            return tomas.ToList();
        }//public List<Toma> ObtenerTomas(int? folio, string propietario, string calle)        
        public Constancia ObtenerConstanciaSinImprimir(int tomaId)
        {            
            var constancia = _context.Constancia
                .Include(x => x.Pago)
                .Include(x => x.Pago.Select(y => y.Recibo))
                .Where(x => x.TomaId == tomaId &&
                            x.ReciboImpreso == false)
                .OrderByDescending(x => x.ConstanciaId).FirstOrDefault();
            return constancia;
        }//public Constancia ObtenerConstanciaSinImprimir(int tomaId)
        public Constancia ObtenerConstancia(string propietario)
        {
            var constancia = _context.Constancia
                .Include(c => c.Pago)
                .Include(c => c.Pago.Select(r => r.Recibo))
                .Where(c => c.Propietario == propietario &&
                            c.ReciboImpreso == false)
                .OrderByDescending(c => c.ConstanciaId).FirstOrDefault();
            return constancia;
        }//public Constancia ObtenerConstancia(int constanciaId)
        #endregion
    }//public class ConstanciasDomain
}//namespace ComiteAgua.Domain