using ComiteAgua.Domain;
using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComiteAgua.ViewModels.Catalogos;
using ComiteAgua.Filters.Security;
using AdsertiVS2017ClassLibrary;

namespace ComiteAgua.Controllers.Catalogos
{
    [Authenticate]
    public class CatalogosController : MessageControllerBase
    {
        #region * Constructor generado por Comité Agua *
        public CatalogosController()
        {
            _context = new DataContext();
        }
        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *
        private readonly DataContext _context;
        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *
        // GET: Catalogos
        public ActionResult Categorias(int? mesAno)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var tarifas = categoriasDomain.ObtenerCategorias(mesAno).OrderBy(t => t.Categoria.Nombre).ToList();
            var categoriasVM = new CategoriasViewModel()
            {
                MesAnoFiltro = mesAno,
                Categoria = tarifas.Select(c => c.Categoria.Nombre).FirstOrDefault(),
                Abreviatura = tarifas.Select(c => c.Categoria.Abreviatura).FirstOrDefault(),
                Tarifas = tarifas
            };
            return View("Categorias", categoriasVM);
        }
        public ActionResult ConsultarCategoria(CategoriasViewModel model)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var tarifas = categoriasDomain.ObtenerCategorias(model.MesAnoFiltro).ToList();
            var categoriasVM = new CategoriasViewModel()
            {
                MesAnoFiltro = model.MesAnoFiltro,               
                Tarifas = tarifas
            };
            return View("Categorias", categoriasVM);
        }
        public ActionResult Descuentos()
        {
            var descuentosProntoPagoDomain = new DescuentosProntoPagoDomain(_context);
            var descuentos = descuentosProntoPagoDomain.ConsultarDescuentos().ToList();

            var descuentosViewModel = new DescuentosViewModel()
            {
                Descuentos = descuentos,
                Accion = Accion.Consultar,                
            };
            return View(descuentosViewModel);
        }
        public ActionResult EditAddCategoria(int? tarifaId, int? mesAnoFiltro, string urlRetorno)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var categoriaVM = new CategoriasViewModel();
            if (tarifaId != null) { 
                var tarifa = categoriasDomain.ObtenerCategoria(Convert.ToInt32(tarifaId));
                categoriaVM = new CategoriasViewModel()
                {
                    CategoriaId = tarifa.CategoriaId,                   
                    Categoria = tarifa.Categoria.Nombre,
                    Abreviatura = tarifa.Categoria.Abreviatura,                   
                    Accion = Accion.Editar,
                    MesAnoFiltro = mesAnoFiltro,
                    UrlRetorno = urlRetorno,
                    MesAno = tarifa.MesAno,
                    CostoTarifa = tarifa.CostoTarifa.ToString("C")
                };
            }
            else
            {
                categoriaVM.Accion = Accion.Agregar;
                categoriaVM.UrlRetorno = urlRetorno;
            }
                      
            return View("EditarCategoria", categoriaVM);
        }
        public ActionResult EditAddTarifa(int categoriaId, int? mesAnoFiltro, string urlRetorno)
        {
            var categoriaVM = new CategoriasViewModel();
            categoriaVM.CategoriaId = categoriaId;
            categoriaVM.UrlRetorno = urlRetorno;
            categoriaVM.MesAnoFiltro = mesAnoFiltro;
            return View(categoriaVM);
        }
        public ActionResult EditAddDescuento(int? descuentoId, string urlRetorno)
        {
            var descuentosProntoPagoDomain = new DescuentosProntoPagoDomain(_context);
            var descuentosVM = new DescuentosViewModel();
            if (descuentoId == null)
            {
                descuentosVM.Accion = Accion.Agregar;
                descuentosVM.UrlRetorno = urlRetorno;
            }
            else
            {
                var descuento = descuentosProntoPagoDomain.ConsultarDescuento(Convert.ToInt32(descuentoId));
                descuentosVM.DescuentoId = descuento.DescuentoId;
                descuentosVM.MesAno = descuento.MesAno.ToString("yyyy-MM");
                descuentosVM.MontoPoncentaje = descuento.MontoPoncentaje.ToString();
                descuentosVM.Accion = Accion.Editar;
                descuentosVM.UrlRetorno = urlRetorno;
;           }

            return View(descuentosVM);
        }
        public ActionResult GuardarCategoria(CategoriasViewModel model)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var categoria = new Categoria();

            if (model.Accion == Accion.Agregar)
            {
                var tarifa = new Tarifa()
                {                   
                    MesAno = Convert.ToInt32(model.MesAno),
                    CostoTarifa = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoTarifa)),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"]),
                    FechaAlta = DateTime.Now,
                    Categoria = new Categoria()
                    {                        
                        Nombre = AdsertiFunciones.FormatearTexto(model.Categoria),
                        Abreviatura = AdsertiFunciones.FormatearTexto(model.Abreviatura),
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"]),
                        FechaAlta = DateTime.Now
                    }
                };
                categoriasDomain.Agregar(tarifa);
                return RedirectToAction("EditAddCategoria", new{ urlRetorno = model.UrlRetorno});
            }
            else
            {                
                var existe = categoriasDomain.ValidarTarifa(model.CategoriaId, Convert.ToInt32(model.MesAno), model.TarifaId);

                if (existe)
                {
                    ShowToastMessage("Alerta", "La categoría ya existe en el año", ToastMessage.ToastType.Warning);
                    return View("EditarCategoria", model);
                }
                var tarifa = new Tarifa()
                {
                    TarifaId = model.TarifaId,
                    MesAno = Convert.ToInt32(model.MesAno),
                    CostoTarifa = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoTarifa)),
                    UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"]),
                    Categoria = new Categoria()
                    {
                        CategoriaId = model.CategoriaId,
                        Nombre = AdsertiFunciones.FormatearTexto(model.Categoria),
                        Abreviatura = AdsertiFunciones.FormatearTexto(model.Abreviatura),
                        UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"])
                    }
                };
                categoriasDomain.Editar(tarifa);
                return RedirectToAction("Categorias",new{ mesAno = model.MesAnoFiltro});
            }
           
        }
        public ActionResult GuardarDescuento(DescuentosViewModel model)
        {
            var descuentosProntoPagoDomain = new DescuentosProntoPagoDomain(_context);

            var existe = descuentosProntoPagoDomain.ValidarDescuento(model.DescuentoId, Convert.ToDateTime(model.MesAno));

            if (existe)
            {
                ShowToastMessage("Alerta", "El descuento ya existe para este año", ToastMessage.ToastType.Warning);
                return View("EditAddDescuento", model);
            }

            var descuentoProntoPago = new DescuentoProntoPago()
            {
                MesAno = Convert.ToDateTime(model.MesAno),
                MontoPoncentaje = Convert.ToInt32(AdsertiFunciones.FormatearNumero(model.MontoPoncentaje))                
            };

            if (model.Accion == Accion.Agregar)
            {
                descuentoProntoPago.UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString());
                descuentoProntoPago.FechaAlta = DateTime.Now;
                descuentosProntoPagoDomain.Agregar(descuentoProntoPago);
                return RedirectToAction("Index","Home");
            }
            else
            {
                descuentoProntoPago.DescuentoId = model.DescuentoId;
                descuentoProntoPago.UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString());
                descuentosProntoPagoDomain.Editar(descuentoProntoPago);
                return RedirectToAction("Descuentos");
            }
            
        }
        public ActionResult GuardarTarifa(CategoriasViewModel model)
        {
            var categoriasDomain = new CategoriasDomain(_context);

            var existe = categoriasDomain.ValidarTarifa(model.CategoriaId, Convert.ToInt32(model.MesAno));

            if (existe)
            {
                ShowToastMessage("Alerta", "La categoría ya existe en el año", ToastMessage.ToastType.Warning);
                return View("EditAddTarifa", model);
            }

            var tarifa = new Tarifa()
            {
                CategoriaId = model.CategoriaId,
                MesAno = Convert.ToInt32(model.MesAno),
                CostoTarifa = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoTarifa)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };
            categoriasDomain.AgregarTarifa(tarifa);
            return RedirectToAction("Categorias", new { mesAno = model.MesAnoFiltro });
        }        
        public ActionResult Limpiar()
        {
            var categoriasVM = new CategoriasViewModel()
            {
                Tarifas = new List<Tarifa>()
            };
            return View("Categorias", categoriasVM);
        }
        public ActionResult DetalleCategoria(int categoriaId, int? mesAnoFiltro, string urlRetorno)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var tarifas = categoriasDomain.ObtenerTarifas(categoriaId);

            var categoriasVM = new CategoriasViewModel()
            {
                Tarifas = tarifas.OrderByDescending(x => x.MesAno).ToList(),
                UrlRetorno = urlRetorno,
                MesAnoFiltro = mesAnoFiltro
            };
            return View(categoriasVM);
        }        

        #endregion

        #region * Métodos creados por Comité Agua *
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        } // protected override void Dispose(bool disposing)
        #endregion

    }//public class CatalogosController : Controller
}//namespace ComiteAgua.Controllers.Catalogos