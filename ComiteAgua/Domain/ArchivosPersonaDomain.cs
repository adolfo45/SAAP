using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ComiteAgua.Domain
{
    public class ArchivosPersonaDomain
    {
        #region * Constructor generado por Comité Agua *
        public ArchivosPersonaDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        }//public ArchivosPersonaDomain(DataContext applicationDbContext)
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
        public ArchivoPersona ObtenerArchivoPersona(int archivoPersonaId)
        {
            var archivoPersona = _context.ArchivoPersona
                .Where(a => a.ArchivoPersonaId == archivoPersonaId).FirstOrDefault();

            return archivoPersona;
        }
        public void Eliminar(int archivoPersonaId)
        {
            var model = _context.ArchivoPersona
                .Where(ap => ap.ArchivoPersonaId == archivoPersonaId).FirstOrDefault();

            _context.Entry(model).State = EntityState.Deleted;

            _context.SaveChanges();
        }
        public void Guardar(ArchivoPersona model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }//public void Guardar(TipoArchivo model)
        public IList<ArchivoPersona> Obtener(int personaId, int tipoArchivoId)
        {
            var archivos = _context.ArchivoPersona
                .Where(p => p.PersonaId == personaId &&
                            p.TipoArchivoId == tipoArchivoId);

            return archivos.ToList();
        }  
        #endregion
    }//public class ArchivosPersonaDomain
}//namespace ComiteAgua.Domain