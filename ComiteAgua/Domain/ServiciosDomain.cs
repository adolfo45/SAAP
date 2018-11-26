using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ServiciosDomain
    {

        #region * Constructor generado por Comité Agua *

        public ServiciosDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public ServiciosDomain(DataContext applicationDbContext)

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

        public void Editar(Servicio model)
        {
            var bd = _context.Servicio.Where(s => s.ServicioId == model.ServicioId).FirstOrDefault();

            bd.AsuntoDescripcionId = model.AsuntoDescripcionId;
            bd.Nombre = model.Nombre;
            bd.Direccion = model.Direccion;
            bd.Colonia = model.Colonia;
            bd.Telefono = model.Telefono;
            bd.UbicacionServicio = model.UbicacionServicio;
            bd.Otro = model.Otro;
            bd.Observaciones = model.Observaciones;
            bd.FechaCambio = DateTime.Now;
            bd.UsuarioCambioId = model.UsuarioCambioId;
            bd.UrlArchivo = model.UrlArchivo;

            _context.SaveChanges();
        }

        public Servicio ObtenerServicio(int servicioId)
        {
            var result = _context.Servicio
                .Include(s => s.AsuntoDescripcion)
                .Include(s => s.AsuntoDescripcion.Asunto)
                .Where(s => s.ServicioId == servicioId).SingleOrDefault();

            return result;
        }

        public List<Servicio> ObtenerServicios()
        {
            var result = _context.Servicio
                .Include(s => s.AsuntoDescripcion)
                .Include(s => s.AsuntoDescripcion.Asunto)
                .Include(s => s.UsuarioCambio.Persona)
                .Include(s => s.Toma)
                .Include(s => s.EvidenciaServicio)
                .ToList();

            return result;
        }

        public void Guardar(Servicio model)
        {

            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }

        #endregion

    } // public class ServiciosDomain
} // namespace ComiteAgua.Domain