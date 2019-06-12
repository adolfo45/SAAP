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
        public ActionResult Categorias()
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var categorias = categoriasDomain.ObtenerCategorias();
            var categoriasVM = new CategoriasViewModel()
            {
                Categorias = categorias
            };
            return View("Categorias", categoriasVM);
        }
        public ActionResult Descuentos()
        {
            var descuentosProntoPagoDomain = new DescuentosProntoPagoDomain(_context);
            var descuentos = descuentosProntoPagoDomain.ConsultarDescuentos().ToList();

            var descuentosViewModel = new DescuentosViewModel()
            {
                Descuentos = descuentos.Where(d => d.MesAno.Year == DateTime.Now.Year).ToList(),
                Accion = Accion.Consultar,                
            };
            return View(descuentosViewModel);
        }
        public ActionResult DescuentosVarios(int tipoDescuentoId)
        {
            var descuentosDomain = new DescuentosDomain(_context);
            var descuentos = descuentosDomain.Consultar(tipoDescuentoId);
            ViewBag.TipoDescuentoId = tipoDescuentoId;
            var descuentosPreList = descuentos
                .Select(up => new DescuentosVariosViewModel
                {
                    DescuentoVariosId = up.DescuentoVariosId,
                    TipoDescuentoId = up.TipoDescuentoId,
                    TipoDescuento = up.TipoDescuento.Nombre,
                    Porcentaje = up.Porcentaje.ToString() + "%",
                    FechaAlta = up.FechaAlta.ToString("dd-MM-yyyy")
                })
                .OrderByDescending(up => up.DescuentoVariosId)
                .Take(1)
                .ToList();
            ViewBag.TituloLabel = tipoDescuentoId == (int)TiposDescuentoDomain.TiposDescuentoEnum.MadreSoltera ? "Descuento Madre Soltera" :
                                       "Descuento Tercera Edad";
            return View(descuentosPreList);
        }
        public ActionResult DescuentosVariosAdministracion(int tipoDescuentoId)
        {
            var descuentosVariosVM = new DescuentosVariosViewModel();
            descuentosVariosVM.TipoDescuentoId = tipoDescuentoId;
            descuentosVariosVM.UrlRetorno = Url.Action("DescuentosVarios", "Catalogos", new { tipoDescuentoId  = tipoDescuentoId });
            return View(descuentosVariosVM);
        }
        public ActionResult EditAddCategoria(int? categoriaId, string urlRetorno)
        {
            var categoriasDomain = new CategoriasDomain(_context);
            var categoriaVM = new CategoriasViewModel();

            if (categoriaId != null)
            { 
                var categoria = categoriasDomain.ObtenerCategoria(Convert.ToInt32(categoriaId));
                categoriaVM = new CategoriasViewModel()
                {
                    CategoriaId = categoria.CategoriaId,                   
                    Categoria = categoria.Nombre,
                    Abreviatura = categoria.Abreviatura,                   
                    Accion = Accion.Editar,
                    UrlRetorno = urlRetorno
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

                categoria = new Categoria()
                {
                    Nombre = AdsertiFunciones.FormatearTexto(model.Categoria),
                    Abreviatura = AdsertiFunciones.FormatearTexto(model.Abreviatura),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"]),
                    FechaAlta = DateTime.Now
                };
                
                categoriasDomain.Agregar(categoria);
                ShowToastMessage("Éxito", "Registro guardado exitosamente", ToastMessage.ToastType.Success);
                return RedirectToAction("Categorias");
            }
            else
            {                
                var existe = categoriasDomain.ValidarTarifa(model.CategoriaId, Convert.ToInt32(model.MesAno), model.TarifaId);

                if (existe)
                {
                    ShowToastMessage("Alerta", "La categoría ya existe en el año", ToastMessage.ToastType.Warning);
                    return View("EditarCategoria", model);
                }

                categoria = new Categoria()
                {
                    CategoriaId = model.CategoriaId,
                    Nombre = AdsertiFunciones.FormatearTexto(model.Categoria),
                    Abreviatura = AdsertiFunciones.FormatearTexto(model.Abreviatura),
                    UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"])
                };
                
                categoriasDomain.Editar(categoria);
                ShowToastMessage("Éxito", "Registro guardado exitosamente", ToastMessage.ToastType.Success);
                return RedirectToAction("Categorias");
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
        public ActionResult GuardarDescuentoVarios(DescuentosVariosViewModel viewModel)
        {
            var descuentosDomain = new DescuentosDomain(_context);
            var descuento = new Descuento();
            descuento.TipoDescuentoId = viewModel.TipoDescuentoId;
            descuento.Porcentaje = Convert.ToInt32(AdsertiFunciones.FormatearNumero(viewModel.Porcentaje));
            descuento.FechaAlta = DateTime.Now;
            descuento.UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString());
            descuentosDomain.Guardar(descuento);

            ShowToastMessage("Éxito", "Registro guardado exitosamente", ToastMessage.ToastType.Success);
            return RedirectToAction("DescuentosVarios", new { tipoDescuentoId = viewModel.TipoDescuentoId });
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