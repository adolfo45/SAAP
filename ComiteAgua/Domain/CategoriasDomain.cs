using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ComiteAgua.Domain
{
    public class CategoriasDomain
    {

        #region * Constructor generado por Comité Agua *

        public CategoriasDomain(DataContext applicationDbContext)
        {
            _context = applicationDbContext;
        } // public CategoriasDomain(DataContext applicationDbContext)

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

        public void Agregar(Tarifa model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        public void AgregarTarifa(Tarifa model)
        {
            _context.Entry(model).State = EntityState.Added;

            _context.SaveChanges();
        }
        public void Editar(Tarifa model)
        {
            var categoria = _context.Categoria.Where(c => c.CategoriaId == model.Categoria.CategoriaId).FirstOrDefault();

            categoria.Nombre = model.Categoria.Nombre;
            categoria.Abreviatura = model.Categoria.Abreviatura;
            categoria.UsuarioCambioId = model.Categoria.UsuarioCambioId;
            categoria.FechaCambio = DateTime.Now;

            var tarifa = _context.Tarifa.Where(t => t.TarifaId == model.TarifaId).FirstOrDefault();

            tarifa.MesAno = model.MesAno;
            tarifa.CostoTarifa = model.CostoTarifa;
            tarifa.UsuarioCambioId = model.UsuarioCambioId;
            tarifa.FechaCambio = DateTime.Now;

            _context.SaveChanges();
        }
        public List<Categoria> ObtenerCategorias()
        {
            var result = _context.Categoria
                .OrderBy(c => c.Nombre)
                .ToList();

            return result;
        }
        public IQueryable<Tarifa> ObtenerCategorias(int? mesAno)
        {
            var tarifas = _context.Tarifa
                .Include(c => c.Categoria)
                .Where(t => t.MesAno == mesAno);

            return tarifas;
        }
        public Tarifa ObtenerCategoria(int tarifaId)
        {
            var tarifa = _context.Tarifa   
                .Include(t => t.Categoria)
                .Where(t => t.TarifaId == tarifaId).FirstOrDefault();

            return tarifa;
        }        
        public IQueryable<Tarifa> ObtenerTarifas(int categoriaId)
        {
            var tarifas = _context.Tarifa
                .Include(t => t.Categoria)
                .Where(t => t.Categoria.CategoriaId == categoriaId);

            return tarifas;
        }

        public bool ValidarTarifa(int categoriaId, int mesAno, int tarifaId = 0)
        {
            var tarifa = _context.Tarifa.Where(t => t.CategoriaId == categoriaId &&
                                                    t.MesAno == mesAno &&
                                                    t.TarifaId != tarifaId).FirstOrDefault();
            return tarifa != null ? true : false;
        }

        #endregion

    } // public class CategoriasDomain

} // namespace ComiteAgua.Domain