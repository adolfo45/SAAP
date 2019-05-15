using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComiteAgua.Classes;
using System.Globalization;

namespace ComiteAgua.Domain
{
    public class PropietariosDomain
    {

        #region * Constructor generado por Comité Agua *

        public PropietariosDomain(DataContext applicationDbContext)
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

        public Propietario ObtenerPropietario(int propietarioId)
        {
            if (string.IsNullOrEmpty(propietarioId.ToString()) || propietarioId == 0) throw new ArgumentNullException("propietarioId");

            var result = _context.Propietario
                .Include(x => x.Toma)
                .Include(x => x.Toma.Select(d => d.Direccion))
                .Include(x => x.Toma.Select(d => d.Direccion.Calles))
                .Include(x => x.Toma.Select(d => d.Direccion.TiposCalle))
                .Include(x => x.Toma.Select(d => d.Direccion.Colonias))
                .Include(x => x.Persona)
                .Include(x => x.Persona.PersonaFisica)                  
                .Where(x => x.PropietarioId == propietarioId).SingleOrDefault();

            return result;

        } // public Propietario ObtenerPropietario(int propietarioId)
        public void Guardar(Propietario model)
        {
            if (model.PropietarioId > 0)
            {
                var bd = _context.PersonaFisica.Where(p => p.Persona.Propietario.Any(x => x.PropietarioId == model.PropietarioId)).FirstOrDefault();

                bd.Nombre = model.Persona.PersonaFisica.Nombre;
                bd.ApellidoPaterno = model.Persona.PersonaFisica.ApellidoPaterno;
                bd.ApellidoMaterno = model.Persona.PersonaFisica.ApellidoMaterno;
                bd.FechaNacimiento = model.Persona.PersonaFisica.FechaNacimiento;
                bd.EstadoCivilId = model.Persona.PersonaFisica.EstadoCivilId;
                bd.Telefono = model.Persona.PersonaFisica.Telefono;
                bd.CorreoElectronico = model.Persona.PersonaFisica.CorreoElectronico;
                bd.Rfc = model.Persona.PersonaFisica.Rfc;
                bd.FechaCambio = DateTime.Now;
                bd.UsuarioCambioId = model.UsuarioAltaId;
            }
            else
            {
                _context.Entry(model).State = EntityState.Added;
            }

            _context.SaveChanges();
        } // public void Guardar(Propietario model)
        public AnexGRIDResponde Listar(AnexGRID agrid)
        {            
            try
            {
                agrid.Inicializar();

                var query = _context.Propietario
                            .Include(x => x.Persona.PersonaFisica)
                            .Include(x => x.Toma)
                            .Include(x => x.Toma.Select(d => d.Direccion))
                            .Include(x => x.Toma.Select(d => d.Direccion.Colonias))
                            .Include(x => x.Toma.Select(d => d.Direccion.TiposCalle))
                             .Include(x => x.Toma.Select(d => d.Direccion.Calles))
                            .Include(x => x.Toma.Select(p => p.Pago))
                            .Include(x => x.Toma.Select(pp => pp.PeriodoPago))
                            .Include(x => x.Toma.Select(ca => ca.Categoria))
                            .Include(x => x.Toma.Select(co => co.Convenio))
                            .Include(x => x.Toma.Select(n => n.Notificacion))
                            .Include(x => x.Toma.Select(n => n.Notificacion.Select(t => t.TipoNotificacion)))
                            .Include(x => x.Toma.Select(n => n.Notificacion.Select(u => u.UsuarioNotifico.Persona)))
                            .Where(x => x.PropietarioId > 0).Take(1000);

                // Ordenar                 
                if (agrid.columna == "Folio") query = agrid.columna_orden == "DESC"
                                                      ? query.OrderByDescending(x => x.Toma.Select(t => t.Folio).FirstOrDefault())
                                                      : query.OrderBy(x => x.Toma.Select(t => t.Folio).FirstOrDefault());  
                
                if (agrid.columna == "Propietario") query = agrid.columna_orden == "DESC"
                                                  ? query.OrderByDescending(x => (x.Persona.PersonaFisica.Nombre.Trim()))
                                                  : query.OrderBy(x => (x.Persona.PersonaFisica.Nombre.Trim()));

                if (agrid.columna == "Calle") query = agrid.columna_orden == "DESC"
                                                  ? query.OrderByDescending(x => x.Toma.Select(d => d.Direccion.TiposCalle.Nombre.Trim()).FirstOrDefault())
                                                  : query.OrderBy(x => x.Toma.Select(d => d.Direccion.TiposCalle.Nombre.Trim()).FirstOrDefault());

                if (agrid.columna == "Colonia") query = agrid.columna_orden == "DESC"
                                                  ? query.OrderByDescending(x => x.Toma.Select(d => d.Direccion.Colonias.Nombre.Trim()).FirstOrDefault())
                                                  : query.OrderBy(x => x.Toma.Select(d => d.Direccion.Colonias.Nombre.Trim()).FirstOrDefault());

                if (agrid.columna == "Categoria") query = agrid.columna_orden == "DESC"
                                                  ? query.OrderByDescending(x => x.Toma.Select(d => d.Categoria.Abreviatura.Trim()).FirstOrDefault())
                                                  : query.OrderBy(x => x.Toma.Select(d => d.Categoria.Abreviatura.Trim()).FirstOrDefault());

                if (agrid.columna == "PeriodoPago") query = agrid.columna_orden == "DESC"
                                                 ? query.OrderByDescending(x => x.Toma.Select(d => d.PeriodoPago.Select(pp => pp.MesAnoFin).FirstOrDefault()).FirstOrDefault())
                                                 : query.OrderBy(x => x.Toma.Select(d => d.PeriodoPago.Select(pp => pp.MesAnoInicio).FirstOrDefault()).FirstOrDefault());

                // Filtrar               
                foreach (var f in agrid.filtros)
                {

                    if (f.columna == "EstadoToma")
                    {
                        if (f.valor.Equals("1"))
                        {
                            query = query.Where(x =>
                                x.Toma.Select(c => c.Convenio.Any(co => co.EstatusConvenioId == 1)).FirstOrDefault());
                        }
                        else if (f.valor.Equals("3"))
                        {
                            query = query.Where(x =>
                                x.Toma.Select(n => n.Notificacion.Any(no => no.Activa)).FirstOrDefault());
                        }
                        else if (f.valor.Equals("4"))
                        {
                            query = query.Where(x =>
                                x.Toma.Select(t => t.Pasiva == true).FirstOrDefault());
                        }
                        else if (f.valor.Equals("2"))
                        {
                            query = query.Where(x =>
                                x.Toma.Select(c =>
                                        c.Convenio.Any(co => co.EstatusConvenioId == 2 && co.Eliminado == null)).FirstOrDefault()
                                    );
                        }
                    }

                    if (f.columna == "Folio")
                        query = query.Where(x => x.Toma.Any(t => t.Folio.ToString() == f.valor));

                    if (f.columna == "Propietario")
                        query = query.Where(x => (x.Persona.PersonaFisica.Nombre.Trim() + " " + x.Persona.PersonaFisica.ApellidoPaterno.Trim() + " " + x.Persona.PersonaFisica.ApellidoMaterno.Trim()).Trim().Contains(f.valor));

                    if (f.columna == "Calle")
                        query = query.Where(x => x.Toma.Select(d => (d.Direccion.TiposCalle.Nombre.Trim() + " " + d.Direccion.Calles.Nombre.Trim())).FirstOrDefault().Trim().Contains(f.valor));

                    if (f.columna == "Colonia")
                        query = query.Where(x => x.Toma.Select(d => d.Direccion.Colonias.Nombre.Trim()).FirstOrDefault().Trim().Contains(f.valor));

                    if (f.columna == "Categoria")
                        query = query.Where(x => x.Toma.Select(d => d.Categoria.Abreviatura.Trim()).FirstOrDefault().Trim().StartsWith(f.valor));

                    if (f.columna == "PeriodoPago")
                        query = query.Where(x => x.Toma.Select(d => d.PeriodoPago.Select(pp => pp.UltimoPeriodoPago).FirstOrDefault()).FirstOrDefault().StartsWith(f.valor));
                }

                var propietarios = query.Skip(agrid.pagina)
                                             .Take(agrid.limite)
                                             .ToList();
                
                agrid.SetData(
                        from e in propietarios
                        select new
                        {
                            Folio = e.Toma.Select(x => x.Folio).FirstOrDefault(),
                            PropietarioId = e.PropietarioId,
                            Propietario = e.Persona.PersonaFisica.Nombre + ' ' + e.Persona.PersonaFisica.ApellidoPaterno + ' ' + e.Persona.PersonaFisica.ApellidoMaterno,
                            Calle = e.Toma.Select(d => d.Direccion).FirstOrDefault() != null ? ((e.Toma.Select(d => d.Direccion.TipoCalleId).FirstOrDefault() > 0 ? e.Toma.Select(d => d.Direccion.TiposCalle.Nombre).FirstOrDefault() : string.Empty) + ' ' + (e.Toma.Select(d => d.Direccion.CalleId).FirstOrDefault() > 0 ? e.Toma.Select(d => d.Direccion.Calles.Nombre).FirstOrDefault() : string.Empty) +
                                    (!string.IsNullOrEmpty(e.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault()) ? " INT " + e.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault() : string.Empty ) + 
                                    (!string.IsNullOrEmpty(e.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault()) ? " EXT " + e.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault() : string.Empty)) : String.Empty,
                            Colonia = e.Toma.Select(d => d.Direccion).FirstOrDefault() != null ? (e.Toma.Select(d => d.Direccion.ColoniaId).FirstOrDefault() > 0 ? e.Toma.Select(d => d.Direccion.Colonias.Nombre).FirstOrDefault() : string.Empty) : string.Empty,
                            Categoria = e.Toma.Select(d => d.Categoria.Abreviatura),
                            PeriodoPago = e.Toma.Select(d => d.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).LastOrDefault() != null ? Convert.ToDateTime(e.Toma.Select(d => d.PeriodoPago.Select(u => u.MesAnoFin).LastOrDefault()).LastOrDefault()).ToString("MMM-yyyy", new CultureInfo("es-ES")) :  e.Toma.Select(d => d.PeriodoPago.Select(u => u.UltimoPeriodoPago).LastOrDefault()).LastOrDefault(),                            
                            ConceptoPagoId = e.Toma.Select(c => c.Convenio.Select(co => co.EstatusConvenioId).LastOrDefault()).FirstOrDefault() == (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo ? Convert.ToInt32(ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio) : 
                                             e.Toma.Select(l => l.LiquidacionTomaId).FirstOrDefault() == (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaNueva ? Convert.ToInt32(ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva) : Convert.ToInt32(ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua),
                            TomaId = e.Toma.Select(x => x.TomaId),
                            Activa = e.Toma.Select(t => t.Activa).FirstOrDefault() == true ? 1 : 0,
                            CategoriaId = e.Toma.Select(t => t.CategoriaId).FirstOrDefault(),
                            ConvenioActivo = e.Toma.Select(c => c.Convenio.Select(ca => ca.EstatusConvenioId).LastOrDefault()).LastOrDefault() == (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo ? true : false,
                            ConvenioVencido = e.Toma.Select(c => c.Convenio.Select(ca => ca.EstatusConvenioId).LastOrDefault()).LastOrDefault() == (int)EstatusConvenioDomain.EstatusConvenioEnum.Cancelado &&
                                e.Toma.Select(c => c.Convenio.Select(ca => ca.Eliminado).LastOrDefault()).LastOrDefault() == null ? true : false,
                            Notificacion = e.Toma.Any(c => c.Notificacion.Any(ca => ca.Activa)) ? true : false,
                            NotificacionDetalle = e.Toma.Select(no => no.Notificacion.Where(ne => ne.Activa)
                                                    .Select(n => n.UsuarioNotifico.Persona.Nombre + " " + n.UsuarioNotifico.Persona.ApellidoPaterno + " " + n.UsuarioNotifico.Persona.ApellidoMaterno + ", " + n.TipoNotificacion.Nombre + ", " + n.FechaEntrega).LastOrDefault()).LastOrDefault(),
                            Pasiva = e.Toma.Select(t => t.Pasiva).FirstOrDefault() == true ? 1 : 0,
                            ConvenioId = e.Toma.Select(c => c.Convenio.Select(ca => ca.ConvenioId).LastOrDefault()).LastOrDefault()
                        }
                    ,
                    query.Count()
                );

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return agrid.responde();

        } // public AnexGRIDResponde Listar(AnexGRID agrid)

        #endregion

    } // public class PropietariosDomain

} // namespace ComiteAgua.Domain