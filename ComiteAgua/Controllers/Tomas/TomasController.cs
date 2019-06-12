using ComiteAgua.Domain;
using ComiteAgua.Global;
using ComiteAgua.Models;
using ComiteAgua.ViewModels.Tomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComiteAgua.Domain.Global;
using AdsertiVS2017ClassLibrary;
using ComiteAgua.ViewModels;
using System.IO;
using System.Data.Entity;
using System.Globalization;
using Rotativa;
using System.Configuration;
using System.Web.UI;
using ComiteAgua.Domain.Seguridad;
using ComiteAgua.Models.Seguridad;
using Novacode;
using System.Xml.Serialization;
using ComiteAgua.Domain.Notificaciones;
using ComiteAgua.Domain.Recibos;
using ComiteAgua.Models.Recibos;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using ComiteAgua.Filters.Security;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using ComiteAgua.Models.Servicios;
using ComiteAgua.ViewModels.Servicios;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;

namespace ComiteAgua.Controllers.Tomas
{
    [Authenticate]
    public class TomasController : MessageControllerBase
    {

        #region * Constructor generado por Comité Agua *

        public TomasController()
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

        // GET: Tomas
        [Route("tomas/tabs")]
        public ActionResult Tabs(int propietarioId = 0, bool cambioPropietario = false)
        {           
            TabsViewModel viewModel = new TabsViewModel();
            var propietariosDomain = new PropietariosDomain(_context);
            var estadosCivilesDomain = new EstadosCivilesDomain(_context);
            var categoriasDomain = new CategoriasDomain(_context);
            var liquidacionesTomaDomain = new LiquidacionesTomaDomain(_context);
            var conceptosConvenioDomain = new ConceptosConvenioDomain(_context);
            var periodosPagoConvenioDomain = new PeriodosPagoConvenioDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            var conveniosDomain = new ConveniosDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var direccionesDomain = new DireccionesDomain(_context);           

            // obtienen catalogos
            var estadosCiviles = estadosCivilesDomain.ObtenerEstadosCiviles();
            var categorias = categoriasDomain.ObtenerCategorias();            

            // si el propietario ya esxiste carga informacion
            if (propietarioId > 0 && !cambioPropietario) {
                // Obtiene al propietario
                var propietario = propietariosDomain.ObtenerPropietario(propietarioId);
                // Obtiene el pago de toma nueva
                var pagoTomaNueva = pagosDomain.ObtenerPagoToma(propietario.Toma.Select(t => t.TomaId).FirstOrDefault());
                // obtiene el convenio de toma nueva
                var convenioTomaNueva = conveniosDomain.ObtenerConvenioTomaNueva(propietario.Toma.Select(t => t.TomaId).FirstOrDefault());
                // obtiene ultimo periodo pago
                var ultimoPeriodoPago = periodosPagoDomain.ObtenerPeriodoPago(propietario.Toma.Select(t => t.TomaId).FirstOrDefault());
               
                if (cambioPropietario)
                {
                    //ViewBag.ReciboId = reciboId;
                }
                viewModel = new TabsViewModel()
                {
                    TabPeriodoPagos = string.Empty,
                    TabTomaActivo = propietario.Toma.Select(x => x.TomaId).FirstOrDefault() == 0 ? "active" : string.Empty,
                    TabDireccionActivo = propietario.Toma.Select(x => x.TomaId).FirstOrDefault() > 0 &&
                                         propietario.Toma.Select(x => x.DireccionId).FirstOrDefault() == null ? "active" : string.Empty,
                    TabPagosActivo = propietario.Toma.Select(x => x.DireccionId).FirstOrDefault() > 0 && (convenioTomaNueva == null && pagoTomaNueva == null) ? "active" : string.Empty,
                    TabPropietarioActivo = (propietario.Toma.Select(x => x.Activa).FirstOrDefault() ||
                                            propietario.Toma.Select(x => x.DireccionId).FirstOrDefault() > 0) &&
                                            propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? "active" : string.Empty,
                    TabPropietarioMoralActivo = (propietario.Toma.Select(x => x.Activa).FirstOrDefault() ||
                                                 propietario.Toma.Select(x => x.DireccionId).FirstOrDefault() > 0) &&
                                                 propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaMoral ? "active" : string.Empty,
                    TabTomaHabilitado = propietario.PropietarioId == 0 ? "disabled" : string.Empty,
                    TabDireccionHabilitado = propietario.Toma.Select(x => x.TomaId).FirstOrDefault() == 0 ? "disabled" : string.Empty,
                    TabPagosHabilitado = propietario.Toma.Select(x => x.DireccionId).FirstOrDefault() == null ? "disabled" : string.Empty,
                    Accion = Accion.Editar,
                    CambioPropietario = cambioPropietario,
                    TipoPersonaId = propietario.Persona.TipoPersonaId,
                    PropietarioId = propietarioId,
                    LiquidacionTomaId = propietario.Toma.Select(t => t.LiquidacionTomaId).FirstOrDefault(),
                    DireccionId = propietario.Toma.Select(d => d.DireccionId).FirstOrDefault(),
                    PropietarioPersonaMoral = new PropietarioPersonaMoralViewModel()
                    {
                        PropietarioId = propietario.PropietarioId,
                        Nombre = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.Nombre : string.Empty,
                        Rfc = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.Rfc : string.Empty,
                        CorreoElectronico = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.CorreoElectronico : string.Empty,
                        
                    },
                    PropietariosPersonaFisica = new PropietariosPersonaFisicaViewModel()
                    {
                        PropietarioId = propietario.PropietarioId,
                        PersonaId = propietario.PersonaId,
                        Nombre = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Nombre : string.Empty,
                        ApellidoPaterno = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.ApellidoPaterno : string.Empty,
                        ApellidoMaterno = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.ApellidoMaterno : string.Empty,
                        FechaNacimiento = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.FechaNacimiento != null ?
                                          Convert.ToDateTime(propietario.Persona.PersonaFisica.FechaNacimiento).ToString("yyyy-MM-dd") : string.Empty : string.Empty,
                        Telefono = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Telefono : string.Empty,
                        CorreoElectronico = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.CorreoElectronico : string.Empty,
                        Rfc = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Rfc : string.Empty,
                        EstadoCivilId = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.EstadoCivilId : (int?)null,
                        EstadosCiviles = estadosCiviles,
                        ExisteArchivoPersona = propietario.Persona.ArchivoPersona.Count > 0,
                        Toma = new TomasViewModel()
                        {
                            TomaId = propietario.Toma.Select(x => x.TomaId).FirstOrDefault(),
                            CategoriaId = propietario.Toma.Select(x => x.CategoriaId).FirstOrDefault(),
                            Folio = propietario.Toma.Count > 0 ? propietario.Toma.Select(x => x.Folio).FirstOrDefault() : (int?)null,
                            Observaciones = propietario.Toma.Select(x => x.Observaciones).FirstOrDefault(),
                            Categorias = categorias,
                            LiquidacionId = Convert.ToInt32(propietario.Toma.Select(x => x.LiquidacionTomaId).FirstOrDefault()),
                            LiquidacionesToma = liquidacionesTomaDomain.ObtenerLiquidacionesToma(),
                            PropietarioId = propietario.PropietarioId,
                            DireccionId = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.DireccionId).FirstOrDefault() : (int?)null,
                            Activa = propietario.Toma.Select(t => t.Activa).FirstOrDefault(),
                            Pasiva = propietario.Toma.Select(t => t.Pasiva).FirstOrDefault() == null ? false : Convert.ToBoolean(propietario.Toma.Select(t => t.Pasiva).FirstOrDefault()),
                            PersonaId = propietario.PersonaId,
                            Direccion = new DireccionesViewModel()
                            {
                                DireccionId = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.DireccionId).FirstOrDefault() : 0,
                                Calle = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.Calle).FirstOrDefault() : string.Empty,
                                NumInt = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.NumInt).FirstOrDefault() : string.Empty,
                                NumExt = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.NumExt).FirstOrDefault() : string.Empty,
                                Colonia = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.Colonia).FirstOrDefault() : string.Empty,
                                PropietarioId = propietario.PropietarioId,
                                TomaId = propietario.Toma.Select(t => t.TomaId).FirstOrDefault(),
                                Calles = direccionesDomain.ObtenerCalles().ToList(),
                                Colonias = direccionesDomain.ObtenerColonias().ToList(),
                                TiposCalle = direccionesDomain.ObtenerTiposCalle().ToList(),
                                CalleId = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.CalleId).FirstOrDefault() : 0,
                                ColoniaId = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.ColoniaId).FirstOrDefault() : 0,
                                TipoCalleId = propietario.Toma.Select(x => x.Direccion).FirstOrDefault() != null ? propietario.Toma.Select(x => x.Direccion.TipoCalleId).FirstOrDefault() : 0,
                                TipoPersonaId = propietario.Persona.TipoPersonaId
                            },
                            Convenio = new ConveniosViewModel()
                            {
                                PropietarioId = propietario.PropietarioId,
                                ConvenioId = convenioTomaNueva != null ? convenioTomaNueva.ConvenioId : 0,
                                ConceptoConvenioId = convenioTomaNueva != null ? convenioTomaNueva.ConceptoConvenioId : 0,
                                TomaId = propietario.Toma.Select(x => x.TomaId).FirstOrDefault(),
                                EstatusConvenioId = convenioTomaNueva != null ? convenioTomaNueva.EstatusConvenioId : 0,
                                PeriodoPagoConvenioId = convenioTomaNueva != null ? convenioTomaNueva.PeriodoPagoConvenioId : 0,
                                FechaInicio = convenioTomaNueva != null ? convenioTomaNueva.FechaInicio.ToString("yyyy-MM-dd") : string.Empty,
                                FechaFin = convenioTomaNueva != null ? convenioTomaNueva.FechaFin.ToString("yyyy-MM-dd") : string.Empty,
                                SubTotal = convenioTomaNueva != null ? convenioTomaNueva.SubTotal.ToString() : string.Empty,
                                Descuento = convenioTomaNueva != null ? convenioTomaNueva.Descuento.ToString() : string.Empty,
                                Total = convenioTomaNueva != null ? convenioTomaNueva.Total.ToString() : string.Empty,
                                Observaciones = convenioTomaNueva != null ? convenioTomaNueva.Observaciones : string.Empty,
                                ConceptosConvenio = conceptosConvenioDomain.ObtenerConceptosConvenio(),
                                PeriodosPagoConvenio = periodosPagoConvenioDomain.ObtenerPeriodosConvenio(),
                                PeriodoInicio = convenioTomaNueva != null ? Convert.ToDateTime(convenioTomaNueva.PeriodoInicio).ToString("yyyy-MM") : string.Empty,
                                PeriodoFin = convenioTomaNueva != null ? Convert.ToDateTime(convenioTomaNueva.PeriodoFin).ToString("yyyy-MM") : string.Empty
                            },
                            Pagos = new PagosViewModel()
                            {
                                PagoId = pagoTomaNueva != null ? pagoTomaNueva.PagoId : 0,
                                PropietarioId = propietario.PropietarioId,
                                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva,
                                TomaId = propietario.Toma.Select(x => x.TomaId).FirstOrDefault(),
                                SubTotal = pagoTomaNueva != null ? pagoTomaNueva.SubTotal.ToString() : string.Empty,
                                Descuento = pagoTomaNueva != null ? pagoTomaNueva.Descuento.ToString() : string.Empty,
                                Total = pagoTomaNueva != null ? pagoTomaNueva.Total.ToString() : string.Empty,
                                PeriodoPagoId = ultimoPeriodoPago != null ? ultimoPeriodoPago.PeriodoPagoId : 0
                            },
                            PeriodoPago = new PeriodosPagoViewModel()
                            {
                                PeriodoPagoId = ultimoPeriodoPago != null ? ultimoPeriodoPago.PeriodoPagoId : 0,
                                TomaId = propietario.Toma.Select(x => x.TomaId).FirstOrDefault(),
                                PropietarioId = propietario.PropietarioId,
                                PagoId = ultimoPeriodoPago != null ? ultimoPeriodoPago.PagoId : 0,
                                MesAnoInicio = string.Empty,
                                MesAnoFin = string.Empty                           
                            }
                        }
                    },                    
                };
            }
            else
            {
                if (cambioPropietario)
                {
                    // Obtiene al propietario
                    var propietario = propietariosDomain.ObtenerPropietario(propietarioId);

                    viewModel = new TabsViewModel()
                    {
                        TabPropietarioActivo = "active",
                        TabPropietarioMoralActivo = "active",
                        TabTomaActivo = "disabled",
                        TabDireccionActivo = "disabled",
                        TabPagosActivo = "disabled",
                        Accion = Accion.Agregar,
                        CambioPropietario = cambioPropietario,
                        TipoPersonaId = propietario.Persona.TipoPersonaId,
                        PropietarioPersonaMoral = new PropietarioPersonaMoralViewModel()
                        {
                            PropietarioId = propietarioId,
                            CambioPropietario = cambioPropietario,
                            Nombre = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.Nombre : string.Empty,
                            Rfc = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.Rfc : string.Empty,
                            CorreoElectronico = propietario.Persona.PersonaMoral != null ? propietario.Persona.PersonaMoral.CorreoElectronico : string.Empty
                        },
                        PropietariosPersonaFisica = new PropietariosPersonaFisicaViewModel()
                        {
                            PropietarioId = propietarioId,
                            EstadosCiviles = estadosCiviles,  
                            CambioPropietario = cambioPropietario,
                            Nombre = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Nombre : string.Empty,
                            ApellidoPaterno = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.ApellidoPaterno : string.Empty,
                            ApellidoMaterno = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.ApellidoMaterno : string.Empty,
                            FechaNacimiento = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.FechaNacimiento != null ?
                                          Convert.ToDateTime(propietario.Persona.PersonaFisica.FechaNacimiento).ToString("yyyy-MM-dd") : string.Empty : string.Empty,
                            Telefono = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Telefono : string.Empty,
                            CorreoElectronico = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.CorreoElectronico : string.Empty,
                            Rfc = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.Rfc : string.Empty,
                            EstadoCivilId = propietario.Persona.PersonaFisica != null ? propietario.Persona.PersonaFisica.EstadoCivilId : (int?)null                           
                        }
                    };
                }
                else
                {
                   
                    viewModel = new TabsViewModel()
                    {
                        TabPropietarioActivo = "active",
                        TabPropietarioMoralActivo = "active",
                        TabTomaActivo = "disabled",
                        TabDireccionActivo = "disabled",
                        TabPagosActivo = "disabled",
                        Accion = Accion.Agregar,                        
                        PropietarioPersonaMoral = new PropietarioPersonaMoralViewModel(),
                        PropietariosPersonaFisica = new PropietariosPersonaFisicaViewModel()
                        {
                            EstadosCiviles = estadosCiviles,
                            Toma = new TomasViewModel()
                            {
                                Categorias = categorias,
                                LiquidacionesToma = liquidacionesTomaDomain.ObtenerLiquidacionesToma(),
                                Direccion = new DireccionesViewModel()
                                {
                                    Calles = direccionesDomain.ObtenerCalles().ToList(),
                                    Colonias = direccionesDomain.ObtenerColonias().ToList(),
                                    TiposCalle = direccionesDomain.ObtenerTiposCalle().ToList()                                    
                                },
                            }

                        }
                    };
                }    
            }           

            return View(viewModel);

        }
        // Vista parcial
        [Route("tomas/tabs/propietarios")]
        public ActionResult _PropietariosPersonaFisicaRegistro(PropietariosPersonaFisicaViewModel model)
        {                      
            return PartialView(model);
        }
        public ActionResult _PropietariosPersonaMoralRegistro(PropietarioPersonaMoralViewModel model)
        {
            return PartialView(model);
        }
        // Vista parcial
        [Route("tomas/tabs/tomas")]
        public ActionResult _TomasRegistro(TomasViewModel model)
        {            
            return PartialView(model);
        }
        public ActionResult _DireccionesRegistro(DireccionesViewModel model)
        {
            return PartialView(model);
        }
        public ActionResult _ConveniosRegistro(ConveniosViewModel model)
        {
            return PartialView(model);
        }
        public ActionResult _PagosTomaNueva(PagosViewModel model)
        {
            return PartialView(model);
        }
        public ActionResult _PeriodoPago(PeriodosPagoViewModel model)
        {
            return PartialView(model);
        }
        public ActionResult _ResultadoConsultarGastos(DateTime fecha)
        {
            List<GastosViewModel> gastosLista = new List<GastosViewModel>();
            GastosDomain gastosDomain = new GastosDomain(_context);
            ArchivosGastoDomain archivosGastoDomain = new ArchivosGastoDomain(_context);            

            var gastos = gastosDomain.ObtenerGastos(fecha);          

            foreach (var item in gastos)
            {
                List<ArchivosGastoViewModel> listaArchivo = new List<ArchivosGastoViewModel>();

                var listaArchivos = archivosGastoDomain.ObtenerArchivosGastos(item.GastoId);

                foreach (var archivo in listaArchivos)
                {
                    ArchivosGastoViewModel archivoGartoViewModel = new ArchivosGastoViewModel()
                    {
                        Nombre = archivo.Nombre
                    };
                    listaArchivo.Add(archivoGartoViewModel);
                }                

                GastosViewModel gastosViewModel = new GastosViewModel()
                {
                    GastoId = item.GastoId,
                    Concepto = item.Concepto,
                    Total = item.Total.ToString("C"),
                    ArchivosGasto = listaArchivo
                };

                gastosLista.Add(gastosViewModel);                
            }

            return PartialView("_ResultadoConsultarGastos", gastosLista);
        }
        [HttpPost]
        public ActionResult _ListaArchivos(HttpPostedFileBase file)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            List<ArchivoClass> listaArchivoClass = new List<ArchivoClass>();
            Archivo archivoClass = new Archivo();

            var nombre = System.IO.Path.ChangeExtension(file.FileName, null);
            var listFile = (List<Archivo>)Session["ListaArchivos"];
            if (Session["ListaArchivos"] == null)
            {
                
                    archivoClass = new Archivo()
                    {
                        ArchivoId = 1,
                        Nombre = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName)
                    };

                    ArchivoClass archivo = new ArchivoClass()
                    {
                        File = file,
                        FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                        nombre = nombre
                    };

                    listaHttp.Add(archivo);
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                    Session["ListaHttp"] = listaHttp;
               
            }
            else
            {
                if (listFile.Count > 0)
                {
                    listaArchivo = (List<Archivo>)Session["ListaArchivos"];
                    listaHttp = (List<ArchivoClass>)Session["ListaHttp"];

                    archivoClass = new Archivo()
                    {
                        ArchivoId = listaArchivo.LastOrDefault().ArchivoId + 1,
                        Nombre = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName)
                    };

                    ArchivoClass archivo = new ArchivoClass()
                    {
                        File = file,
                        FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                        nombre = nombre
                    };

                    listaHttp.Add(archivo);
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                    Session["ListaHttp"] = listaHttp;
                }
                else
                {
                    archivoClass = new Archivo()
                    {
                        ArchivoId = 1,
                        Nombre = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName)
                    };

                    ArchivoClass archivo = new ArchivoClass()
                    {
                        File = file,
                        FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                        nombre = nombre
                    };

                    listaHttp.Add(archivo);
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                    Session["ListaHttp"] = listaHttp;
                }
            }      
            return PartialView("_ListaArchivos", listaArchivo);           
        }
        [HttpPost]
        public ActionResult _ListaArchivosPersona(FormCollection formCollection)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();           
            Archivo archivoClass = new Archivo();
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            string nombre = AdsertiFunciones.FormatearTexto(formCollection["name"]);           
            var file = Request.Files[0];
            int personaId = Convert.ToInt32(formCollection["PersonaId"]);
            int tipoArchivoId = Convert.ToInt32(formCollection["TipoArchivoId"]);

            // valida si ya se guardo el propietario
            if (personaId == 0) { 
                var nombreArchivo = System.IO.Path.ChangeExtension(file.FileName, null);
                var listFile = (List<Archivo>)Session["ListaArchivos"];
                if (Session["ListaArchivos"] == null)
                {
                    archivoClass = new Archivo()
                    {
                        ArchivoId = 1,
                        Nombre = nombre,
                        UrlArchivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                        TipoArchivoId = tipoArchivoId
                    };
                    ArchivoClass archivo = new ArchivoClass()
                    {
                        File = file,
                        FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                        nombre = nombre                        
                    };
                    listaHttp.Add(archivo);
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                    Session["ListaHttp"] = listaHttp;
                }
                else
                {
                    if (listFile.Count > 0)
                    {
                        listaArchivo = (List<Archivo>)Session["ListaArchivos"];
                        listaHttp = (List<ArchivoClass>)Session["ListaHttp"];

                        archivoClass = new Archivo()
                        {
                            ArchivoId = listaArchivo.LastOrDefault().ArchivoId + 1,
                            Nombre = nombre,
                            UrlArchivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                            TipoArchivoId = tipoArchivoId
                        };

                        ArchivoClass archivo = new ArchivoClass()
                        {
                            File = file,
                            FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                            nombre = nombre
                        };

                        listaHttp.Add(archivo);
                        listaArchivo.Add(archivoClass);
                        Session["ListaArchivos"] = listaArchivo;
                        Session["ListaHttp"] = listaHttp;
                    }
                    else
                    {
                        archivoClass = new Archivo()
                        {
                            ArchivoId = 1,
                            Nombre = nombre,
                            UrlArchivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                            TipoArchivoId = tipoArchivoId
                        };

                        ArchivoClass archivo = new ArchivoClass()
                        {
                            File = file,
                            FechaHora = DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                            nombre = nombre
                        };

                        listaHttp.Add(archivo);
                        listaArchivo.Add(archivoClass);
                        Session["ListaArchivos"] = listaArchivo;
                        Session["ListaHttp"] = listaHttp;
                    }
                }
            }
            else
            {     
                string archivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName);

                string rutaArchivo = @"/UploadFiles/Persona/";

                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                } //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))

                file.SaveAs(Server.MapPath(rutaArchivo + archivo));

                ArchivoPersona archivoPersona = new ArchivoPersona()
                {
                    Nombre = nombre,
                    PersonaId = personaId,
                    UrlArchivo = archivo,
                    TipoArchivoId = tipoArchivoId,
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };
                archivosPersonaDomain.Guardar(archivoPersona);
                archivoClass = new Archivo()
                {
                    ArchivoId = archivoPersona.ArchivoPersonaId,
                    Nombre = nombre,
                    UrlArchivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                    TipoArchivoId = tipoArchivoId
                };
               
                if (Session["ListaArchivos"] == null)
                {
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                }
                else
                {
                    listaArchivo = (List<Archivo>)Session["ListaArchivos"];
                    listaArchivo.Add(archivoClass);
                    Session["ListaArchivos"] = listaArchivo;
                }               
            }
            return PartialView("_ListaArchivosPersona", listaArchivo);            
        }
        [HttpPost]
        public ActionResult _ListaArchivosToma(FormCollection formCollection)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            Archivo archivoClass = new Archivo();
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            string nombre = AdsertiFunciones.FormatearTexto(formCollection["name"]);
            var file = Request.Files[0];
            int personaId = Convert.ToInt32(formCollection["PersonaId"]);
            int tipoArchivoId = Convert.ToInt32(formCollection["TipoArchivoId"]);
                        
            string archivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName);

            string rutaArchivo = @"/UploadFiles/Persona/";

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
            } //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))

            file.SaveAs(Server.MapPath(rutaArchivo + archivo));

            ArchivoPersona archivoPersona = new ArchivoPersona()
            {
                Nombre = nombre,
                PersonaId = personaId,
                UrlArchivo = archivo,
                TipoArchivoId = tipoArchivoId,
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };
            archivosPersonaDomain.Guardar(archivoPersona);
            archivoClass = new Archivo()
            {
                ArchivoId = archivoPersona.ArchivoPersonaId,
                Nombre = nombre,
                UrlArchivo = nombre + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(file.FileName),
                TipoArchivoId = tipoArchivoId,
                PersonaId = personaId
            };

            if (Session["ListaArchivos"] == null)
            {
                listaArchivo.Add(archivoClass);
                Session["ListaArchivos"] = listaArchivo;
            }
            else
            {
                listaArchivo = (List<Archivo>)Session["ListaArchivos"];
                listaArchivo.Add(archivoClass);
                Session["ListaArchivos"] = listaArchivo;
            }
            
            return PartialView("_ListaArchivosToma", listaArchivo);
        }
        public ActionResult _ResultadoCorte(DateTime fecha)
        {
            var model = this.ObtenerCorteViewModel(fecha);

            return PartialView("_ResultadoCorte", model);
        }
        public ActionResult ConsultarCorte()
        {
            return View();
        }
        public ActionResult ConceptoPago(int tomaId, int conceptoPagoId, int convenioId = 0, string urlRetorno = null)
        {
            if (conceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio)
            {
                var conveniosDomain = new ConveniosDomain(_context);
                var pagosDomain = new PagosDomain(_context);

                var convenio = conveniosDomain.ObtenerConvenio(convenioId);

                var pagosConvenio = pagosDomain.ObtenerPagosConvenio(convenio.ConvenioId);

                var totalPagosConvenio = pagosConvenio.Sum(t => t.Total);

                var pagosViewModel = new PagosViewModel()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = tomaId,
                    ResumenPagoConvenio = totalPagosConvenio.ToString("C") + " DE " + convenio.Total.ToString("C") + " RESTAN " + (convenio.Total - totalPagosConvenio).ToString("C"),
                    ConvenioId = convenio.ConvenioId,
                    TotalPagosConvenio = totalPagosConvenio,
                    TotalConvenio = convenio.Total,
                    RestanConvenio = convenio.Total - totalPagosConvenio,
                    FolioTarjeta = convenio.NoTarjeta,
                    EstatusConvenioId = convenio.EstatusConvenioId,
                    UrlRetorno = urlRetorno,
                    FechaAltaFormato = DateTime.Now.ToString("yyyy-MM-dd")
                };

                return View("~/Views/Tomas/RegistroPagosConvenio.cshtml", pagosViewModel);

            }
            else if (conceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua)
            {
                var periodosPagoDomain = new PeriodosPagoDomain(_context);
                var tomasDomain = new TomasDomain(_context);
                var pagosDomain = new PagosDomain(_context);

                var toma = tomasDomain.ObtenerToma(tomaId);
                var periodoPago = periodosPagoDomain.ObtenerPeriodoPago(tomaId);

                var pagosViewModel = new PagosViewModel()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua,
                    TomaId = tomaId,
                    MesAnoInicio = periodoPago != null ? Convert.ToDateTime(periodoPago.MesAnoFin).AddMonths(1).ToString("yyyy-MM") : string.Empty,
                    CategoriaId = toma.CategoriaId,
                    Notificada = toma.Notificacion.Any(n => n.Activa)
                };

                return View("~/Views/Tomas/RegistroPagosSuministroAgua.cshtml", pagosViewModel);
            }
            else if(conceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
            {
                var periodosPagoDomain = new PeriodosPagoDomain(_context);
                var tomasDomain = new TomasDomain(_context);
                var pagosDomain = new PagosDomain(_context);
                Session["Abonos"] = null;

                var toma = tomasDomain.ObtenerToma(tomaId);
                var periodoPago = periodosPagoDomain.ObtenerPeriodoPago(tomaId);
                var abonos = pagosDomain.ObtenerAbonos(tomaId);
                Session["Abonos"] = abonos;

                var pagosViewModel = new PagosViewModel()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva,
                    TomaId = tomaId,
                    MesAnoInicio = DateTime.Now.ToString("yyyy-MM"),
                    MesAnoFin = DateTime.Now.ToString("yyyy-MM"),
                    CategoriaId = toma.CategoriaId,
                    AbonoAnterior = abonos != null ? abonos.Sum(a => a.Abono).ToString() : "0",
                    Notificada = toma.Notificacion.Any(n => n.Activa)                    
                };

                return View("~/Views/Tomas/RegistroPagosSuministroAgua.cshtml", pagosViewModel);
            }
            return HttpNotFound();
        }
        public JsonResult EliminarToma(int propietarioId)
        {
            var tomasDomain = new TomasDomain(_context);
            tomasDomain.Eliminar(propietarioId);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegistroAbonosSuministroAgua(int tomaId, int categoriaId)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            Session["Abonos"] = null;

            var periodoPago = periodosPagoDomain.ObtenerPeriodoPago(tomaId);
            var abonos = pagosDomain.ObtenerAbonos(tomaId);
            Session["Abonos"] = abonos;

            var pagosViewModel = new PagosViewModel()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Abono,
                TomaId = tomaId,
                MesAnoInicio = periodoPago.MesAnoFin != null ? Convert.ToDateTime(periodoPago.MesAnoFin).AddMonths(1).ToString("yyyy-MM") : string.Empty,
                CategoriaId = categoriaId,
                AbonoAnterior = abonos != null ? abonos.Sum(a => a.Abono).ToString() : "0" 
            };

            return View(pagosViewModel);
        }
        [Route("tarifas")]
        public ActionResult RegistroTarifas()
        {
            return View();
        }
        public ActionResult RegistroConvenioSuministroAgua(int tomaId, bool editar = true)
        {
            var conveniosViewModel = new ConveniosViewModel();
            var periodosPagoConvenioDomain = new PeriodosPagoConvenioDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var personasSeguridadDomain = new PersonasSeguridadDomain(_context);
            var conveniosDomain = new ConveniosDomain(_context);
            var conceptosConvenioDomain = new ConceptosConvenioDomain(_context);
            List<PersonaSeguridad> listaPersona = new List<PersonaSeguridad>();

            var periodoPago = periodosPagoDomain.ObtenerPeriodoPago(tomaId);
            var toma = tomasDomain.ObtenerToma(tomaId);
            var personasSeguridad = personasSeguridadDomain.ObtenerPersonasSeguridad();
            var convenio = conveniosDomain.ObtenerConvenioActivo(tomaId);

            foreach (var item in personasSeguridad)
            {
                PersonaSeguridad personaSeguridad = new PersonaSeguridad()
                {
                    PersonaId = item.PersonaId,
                    Nombre = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno
                };
                listaPersona.Add(personaSeguridad);
            }

            if (convenio != null && editar) { 
                conveniosViewModel = new ConveniosViewModel()
                {
                    ConvenioId = convenio.ConvenioId,                   
                    TomaId = tomaId,
                    PeriodosPagoConvenio = periodosPagoConvenioDomain.ObtenerPeriodosConvenio(),
                    PeriodoPagoConvenioId =convenio.PeriodoPagoConvenioId,
                    FechaInicio = convenio.FechaInicio.ToString("yyyy-MM-dd"),
                    FechaFin = convenio.FechaFin.ToString("yyyy-MM-dd"),
                    SubTotal = convenio.SubTotal.ToString(),
                    Descuento = convenio.Descuento.ToString(),
                    Total = convenio.Total.ToString(),
                    Observaciones = convenio.Observaciones,
                    PeriodoInicio = Convert.ToDateTime(convenio.PeriodoInicio).ToString("yyyy-MM"),
                    PeriodoFin = Convert.ToDateTime(convenio.PeriodoFin).ToString("yyyy-MM"),
                    CategoriaId = toma.CategoriaId,
                    PersonasSeguridad = listaPersona,
                    PersonaId =Convert.ToInt32(convenio.PersonaId),
                    PimerPago = convenio.PimerPago.ToString(),
                    PagosDe = convenio.Pagos.ToString(),
                    RutaArchivo = convenio.RutaArchivo,
                    BotonImprimirVisible = true,
                    ConceptosConvenio = conceptosConvenioDomain.ObtenerConceptosConvenio(),
                    ConceptoConvenioId = convenio.ConceptoConvenioId,
                    NoTarjeta = convenio.NoTarjeta,
                    EstatusConvenioId = convenio.EstatusConvenioId,
                    TotalPagos = convenio.Pago.Where(p => p.Activo).Sum(p => p.Total)
                };
            }
            else
            {
                conveniosViewModel = new ConveniosViewModel()
                {
                    TomaId = tomaId,
                    PeriodosPagoConvenio = periodosPagoConvenioDomain.ObtenerPeriodosConvenio(),
                    PeriodoInicio = periodoPago != null ? Convert.ToDateTime(periodoPago.MesAnoFin).AddMonths(1).ToString("yyyy-MM") : string.Empty,
                    CategoriaId = toma.CategoriaId,
                    PersonasSeguridad = listaPersona,
                    BotonImprimirVisible = false,
                    ConceptosConvenio = conceptosConvenioDomain.ObtenerConceptosConvenio()
                };
            }
            return View(conveniosViewModel);
        }      
        public JsonResult CalcularMontoPeriodo(DateTime periodoInicio, DateTime periodoFin, int categoriaId, int tomaId)
        {
            var tarifasDomain = new TarifasDomain(_context);
            var descuentosProntoPagoDomain = new DescuentosProntoPagoDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var descuentosDomain = new DescuentosDomain(_context);

            decimal total = 0;
            decimal? porcentajeDescuento = null;
            decimal? neto = null;
            int? porcentaje = null;
            bool terceraEdad = false;
            int? porcentajeTerceraEdad = null;
            decimal? cantidadPorcentajeTerceraEdad = null;
            decimal totalResul = 0;
            bool madreSoltera = false;
            decimal? cantidadPorcentajeMadreSoltera = null;
            int? porcentajeMadreSoltera = null;
            int? descuentoMadreSolteraId = null;
            int? descuentoTerceraEdadId = null;


            var tarifas = tarifasDomain.ObtenerTarifasPeriodo(periodoInicio, periodoFin, categoriaId);
            var descuentoExiste = descuentosProntoPagoDomain.ConsultarDescuentoVigente(periodoInicio, periodoFin);                     

            while (periodoInicio <= periodoFin)
            {
                var costoTarifa = tarifas.Where(t => t.MesAno == periodoInicio.Year).FirstOrDefault();
                if (costoTarifa != null)
                {
                    var mensualidad = (costoTarifa.CostoTarifa / 12);

                    total += mensualidad;                    
                }
                periodoInicio = periodoInicio.AddMonths(1);
            }

            var newtotal = Math.Round(total);

            if (descuentoExiste != null)
            {
                porcentajeDescuento = descuentoExiste.MontoPoncentaje * newtotal / 100;
                neto = newtotal - porcentajeDescuento;
                porcentaje = descuentoExiste.MontoPoncentaje;
            }

            //Calcula tercera edad para mostrar etiqueta
            var toma = tomasDomain.ObtenerToma(tomaId);
            if (toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica)
            {
                var fechaNacimiento = toma.Propietario.Persona.PersonaFisica.FechaNacimiento;
                var descuento = descuentosDomain.Consultar(Convert.ToInt32(TiposDescuentoDomain.TiposDescuentoEnum.TerceraEdad))
                    .OrderByDescending(d => d.DescuentoVariosId)
                    .Take(1).FirstOrDefault();
                if (fechaNacimiento != null)
                {
                    if ((DateTime.Now.Year - Convert.ToDateTime(fechaNacimiento).Year == 60 &&
                        Convert.ToDateTime(fechaNacimiento).Month >= DateTime.Now.Month &&
                        Convert.ToDateTime(fechaNacimiento).Day >= DateTime.Now.Day) || (DateTime.Now.Year - Convert.ToDateTime(fechaNacimiento).Year > 60))
                    {
                        terceraEdad = true;
                        porcentajeTerceraEdad = descuento != null ? descuento.Porcentaje : 0;
                        cantidadPorcentajeTerceraEdad = descuento != null ? descuento.Porcentaje * newtotal / 100 : 0;
                        descuentoTerceraEdadId = descuento != null ? descuento.DescuentoVariosId : (int?)null;
                    }
                }
            }//if (toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica)

            var pd = porcentajeDescuento == null ? 0 : Convert.ToDecimal(porcentajeDescuento);
            var cpte = cantidadPorcentajeTerceraEdad == null ? 0 : Convert.ToDecimal(cantidadPorcentajeTerceraEdad);

            totalResul = newtotal - pd - cpte;

            //Es madre soltera
            if (toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica)
            {
                var descuento = descuentosDomain.Consultar(Convert.ToInt32(TiposDescuentoDomain.TiposDescuentoEnum.MadreSoltera))
                   .OrderByDescending(d => d.DescuentoVariosId)
                   .Take(1).FirstOrDefault();
                if (toma.Propietario.Persona.PersonaFisica.EstadoCivilId == (int)EstadosCivilesDomain.EstadosCivilesEnum.MadreSoltera)
                {
                    madreSoltera = true;
                    porcentajeMadreSoltera = descuento != null ? descuento.Porcentaje : 0;
                    cantidadPorcentajeMadreSoltera = descuento != null ? descuento.Porcentaje * newtotal / 100 : 0;
                    descuentoMadreSolteraId = descuento != null ? descuento.DescuentoVariosId : (int?)null;
                }
            }//if (toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica)

           
            var cpms = cantidadPorcentajeMadreSoltera == null ? 0 : Convert.ToDecimal(cantidadPorcentajeMadreSoltera);
            totalResul -= cpms;

            return Json(new { success = Math.Truncate(newtotal),
                descuentoresult = porcentajeDescuento,
                totalresult = neto,
                porcentajeresult = porcentaje,
                terceraEdad = terceraEdad,
                porcentajeTerceraEdad = porcentajeTerceraEdad,
                cantidadPorcentajeTerceraEdad = cantidadPorcentajeTerceraEdad,
                totalResul = totalResul,
                porcentajeMadreSoltera = porcentajeMadreSoltera,
                cantidadPorcentajeMadreSoltera = cantidadPorcentajeMadreSoltera,
                madreSoltera = madreSoltera,
                descuentoTerceraEdadId = descuentoTerceraEdadId,
                descuentoMadreSolteraId = descuentoMadreSolteraId
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CalcularPeriodoAbono(decimal subTotal, DateTime periodoInicio, int categoriaId)
        {
            var tarifasDomain = new TarifasDomain(_context);
            decimal abono = subTotal;
            DateTime? periodoFin = periodoInicio;
            decimal nuevoAbono = 0;

            var tarifas = tarifasDomain.ObtenerTarifasAbono(periodoInicio, categoriaId);

            foreach (var item in tarifas)
            {
                abono = abono - item.CostoTarifa;

                if (abono >= 0)
                {
                    //TODO INT
                    //periodoFin = item.MesAno;
                    nuevoAbono = abono;
                }
                else
                {
                    break;
                }                
            }

            return Json(new { nuevoAbono = nuevoAbono, periodoFin = Convert.ToDateTime(periodoFin).ToString("yyyy-MM") }, JsonRequestBehavior.AllowGet);
        }                
        public JsonResult ObtenerConvenio(int convenioId)
        {
            var conveniosDomain = new ConveniosDomain(_context);

            var convenio = conveniosDomain.ObtenerConvenio(convenioId);

            var conveniosViewModel = new ConveniosViewModel()
            {
                Propietario = convenio.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ?
                              convenio.Toma.Propietario.Persona.PersonaFisica.Nombre + " " + convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " + convenio.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno :
                              convenio.Toma.Propietario.Persona.PersonaMoral.Nombre,
                ConceptoConvenio = convenio.ConceptoConvenio.Nombre,
                EstatusConvenios = convenio.EstatusConvenio.Nombre,
                PeriodoPago = convenio.PeriodoPagoConvenio.Nombre,
                FechaInicio = convenio.FechaInicio.ToString("dd-MM-yyyy"),
                FechaFin = convenio.FechaFin.ToString("dd-MM-yyyy"),
                SubTotal = convenio.SubTotal.ToString("C"),
                Descuento = !string.IsNullOrEmpty(convenio.Descuento.ToString()) ? Convert.ToDecimal(convenio.Descuento).ToString("C") : string.Empty,
                Total = convenio.Total.ToString("C"),
                Observaciones = convenio.Observaciones,
                PeriodoInicio = Convert.ToDateTime(convenio.PeriodoInicio).ToString("MMMMMMMMMM yyyy"),
                PeriodoFin = Convert.ToDateTime(convenio.PeriodoFin).ToString("MMMMMMMMMM yyyy"),
                Persona = convenio.Persona.Nombre + " " + convenio.Persona.ApellidoPaterno + " " + convenio.Persona.ApellidoMaterno,
                PagosDe = Convert.ToDecimal(convenio.Pagos).ToString("C")
            };

            return Json(new { convenioViewModel = conveniosViewModel }, JsonRequestBehavior.AllowGet);

        }      
        public ActionResult _TarjetaConvenio(int convenioId)
        {
            var pagosDomain = new PagosDomain(_context);
            var conveniosDomain = new ConveniosDomain(_context);
            var pagosViewModelList = new List<PagosViewModel>();

            var pagosConveio = pagosDomain.ObtenerPagosConvenio(convenioId);
            var convenio = conveniosDomain.ObtenerConvenio(convenioId);

            decimal saldo = convenio.Total;

            foreach (var item in pagosConveio)
            {
                saldo -= item.Total;
                var pagosViewModel = new PagosViewModel()
                {
                    PagoId = item.PagoId,
                    FechaAbono = item.FechaAlta.ToString("dd/MM/yyyy"),
                    AbonoConvenio = item.Total.ToString("C"),
                    Saldo = saldo.ToString("C"),
                    AdeudoTotal = convenio.Total.ToString("C"),
                    ReciboImpreso = item.Recibo.Count > 0,
                    NoRecibo = item.Recibo.Count > 0 ? item.Recibo.Select(r => r.NoRecibo).FirstOrDefault() : (int?)null
                };
                pagosViewModelList.Add(pagosViewModel);
            }

            return PartialView("~/Views/Tomas/_TarjetaConvenio.cshtml", pagosViewModelList);
        }
        public ActionResult HistorialPagos(int tomaId)
        {
            var periodosPagoViewModelList = new List<PeriodosPagoViewModel>();
            var periodosPagoViewModel = new PeriodosPagoViewModel();

            var periodosPagoDomain = new PeriodosPagoDomain(_context);

            var periodosPago = periodosPagoDomain.ObtenerPeriodosPago(tomaId);

            foreach (var item in periodosPago)
            {
                decimal total = 0;
                if (item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio)
                {
                    var pagosDomain = new PagosDomain(_context);

                    total = pagosDomain.ObtenerPagosConvenio(Convert.ToInt32(item.ConvenioId)).Sum(c => c.Total);
                }
                periodosPagoViewModel = new PeriodosPagoViewModel()
                {
                    MesAnoInicio = item.PeriodoPago.Count > 0 ? Convert.ToDateTime(item.PeriodoPago.Select(pp => pp.MesAnoInicio).FirstOrDefault()).ToString("MMMMMMMMMM yyyy").ToUpper() : string.Empty,
                    MesAnoFin = item.PeriodoPago.Count > 0 ? Convert.ToDateTime(item.PeriodoPago.Select(pp => pp.MesAnoFin).FirstOrDefault()).ToString("MMMMMMMMMM yyyy").ToUpper() : string.Empty,
                    TotalPago = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio ? total.ToString("C") : item.Total.ToString("C"),
                    ConceptoPago = item.ConceptoPago.Nombre,
                    FechaPago = Convert.ToDateTime(item.FechaAlta).ToString("dd-MM-yyyy"),
                    Activo = item.Activo,
                    NoRecibo = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count == 0 ? (!string.IsNullOrEmpty(item.Convenio.NoTarjeta) ? Convert.ToInt32(item.Convenio.NoTarjeta) : (int?)null) :
                                item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count > 0 ? item.Recibo.Select(r => r.NoRecibo).FirstOrDefault() : item.Recibo.Select(r => r.NoRecibo).FirstOrDefault(),
                    TipoPago = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count == 0 ? "Tarjeta" : "Recibo",
                };
                periodosPagoViewModelList.Add(periodosPagoViewModel);
            }
            return View(periodosPagoViewModelList);
        }
        public ActionResult GastosComite()
        {
            GastosViewModel model = new GastosViewModel
            {
                SucursalId = Convert.ToInt32(Session["SucursalId"].ToString()),
                MultiComiteId = Convert.ToInt32(Session["MultiComiteId"].ToString()),
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };

            return View(model);
        }
        public ActionResult ConsultarGastos()
        {           
            return View();
        }
        public ActionResult EliminarGasto(int gastoId, DateTime fecha)
        {
            GastosDomain gastosDomain = new GastosDomain(_context);            
            List<GastosViewModel> gastosLista = new List<GastosViewModel>();           
            ArchivosGastoDomain archivosGastoDomain = new ArchivosGastoDomain(_context);

            var gasto = gastosDomain.ObtenerGasto(gastoId);
            var archivoGasto = gasto.ArchivoGasto;
            for(int item = 0; item <= gasto.ArchivoGasto.Count - 1; item++)
            {
                //Elimina Archivo
                var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Gastos/"), archivoGasto[item].Nombre);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                archivosGastoDomain.Eliminar(archivoGasto[item]);
            }            

            gastosDomain.Eliminar(gastoId);            

            var gastos = gastosDomain.ObtenerGastos(fecha);

            foreach (var item in gastos)
            {
                List<ArchivosGastoViewModel> listaArchivo = new List<ArchivosGastoViewModel>();

                var listaArchivos = archivosGastoDomain.ObtenerArchivosGastos(item.GastoId);

                foreach (var archivo in listaArchivos)
                {
                    ArchivosGastoViewModel archivoGartoViewModel = new ArchivosGastoViewModel()
                    {
                        Nombre = archivo.Nombre
                    };
                    listaArchivo.Add(archivoGartoViewModel);
                }

                GastosViewModel gastosViewModel = new GastosViewModel()
                {
                    GastoId = item.GastoId,
                    Concepto = item.Concepto,
                    Total = item.Total.ToString("C"),
                    ArchivosGasto = listaArchivo
                };

                gastosLista.Add(gastosViewModel);
            }

            return PartialView("_ResultadoConsultarGastos", gastosLista);

        }
        public ActionResult QuitarArchivo(string nombre)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            Archivo archivoClass = new Archivo();

            listaArchivo = (List<Archivo>)Session["ListaArchivos"];
            listaHttp = (List<ArchivoClass>)Session["ListaHttp"];

            listaArchivo.RemoveAll(x => x.Nombre == nombre);
            listaHttp.RemoveAll(x => x.nombre + "_" + x.FechaHora  == nombre);

            Session["ListaArchivos"] = listaArchivo;
            Session["ListaHttp"] = listaHttp;

            return PartialView("_ListaArchivos", listaArchivo);
        }
        public ActionResult QuitarArchivoPersona(string nombre, int personaId, int archivoPersonaId, int tipoArchivoId)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            Archivo archivoClass = new Archivo();
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            if (personaId == 0)
            {
                listaArchivo = (List<Archivo>)Session["ListaArchivos"];
                listaHttp = (List<ArchivoClass>)Session["ListaHttp"];

                listaArchivo.RemoveAll(x => x.Nombre == nombre);
                listaHttp.RemoveAll(x => x.nombre + "_" + x.FechaHora == nombre);

                Session["ListaArchivos"] = listaArchivo;
                Session["ListaHttp"] = listaHttp;
            }
            else
            {
                var archivoPersona = archivosPersonaDomain.ObtenerArchivoPersona(archivoPersonaId);
                //Elimina Archivo
                var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Persona/"), archivoPersona.UrlArchivo);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                archivosPersonaDomain.Eliminar(archivoPersonaId);
                var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);

                listaArchivo = archivos
                  .Select(up => new Archivo
                  {
                      PersonaId = up.PersonaId,
                      Nombre = up.Nombre,
                      ArchivoId = up.ArchivoPersonaId,
                      UrlArchivo = up.UrlArchivo,
                      TipoArchivoId = tipoArchivoId
                  })
                  .OrderBy(up => up.ArchivoId)
                  .ToList();

                Session["ListaArchivos"] = listaArchivo;
            }
            
            return PartialView("_ListaArchivosPersona", listaArchivo);
        }
        public ActionResult QuitarArchivoToma(int personaId, int archivoPersonaId, int tipoArchivoId)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            Archivo archivoClass = new Archivo();
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);
           
            var archivoPersona = archivosPersonaDomain.ObtenerArchivoPersona(archivoPersonaId);
            //Elimina Archivo
            var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Persona/"), archivoPersona.UrlArchivo);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            archivosPersonaDomain.Eliminar(archivoPersonaId);
            var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);

            listaArchivo = archivos
                .Select(up => new Archivo
                {
                    PersonaId = up.PersonaId,
                    Nombre = up.Nombre,
                    ArchivoId = up.ArchivoPersonaId,
                    UrlArchivo = up.UrlArchivo,
                    TipoArchivoId = tipoArchivoId
                })
                .OrderBy(up => up.ArchivoId)
                .ToList();

            Session["ListaArchivos"] = listaArchivo;

            return PartialView("_ListaArchivosToma", listaArchivo);
        }
        public ActionResult ConsultarArchivosPersona(string nombre, int personaId, int archivoPersonaId, int tipoArchivoId)
        {
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            List<Archivo> listaArchivo = new List<Archivo>();
            Archivo archivoClass = new Archivo();
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

           
                var archivoPersona = archivosPersonaDomain.ObtenerArchivoPersona(archivoPersonaId);
                //Elimina Archivo
                var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Persona/"), archivoPersona.UrlArchivo);
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                archivosPersonaDomain.Eliminar(archivoPersonaId);
                var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);

                listaArchivo = archivos
                  .Select(up => new Archivo
                  {
                      PersonaId = up.PersonaId,
                      Nombre = up.Nombre,
                      ArchivoId = up.ArchivoPersonaId,
                      UrlArchivo = up.UrlArchivo,
                      TipoArchivoId = tipoArchivoId
                  })
                  .OrderBy(up => up.ArchivoId)
                  .ToList();

                Session["ListaArchivos"] = listaArchivo;
           

            return PartialView("_ListaArchivosToma", listaArchivo);
        }
        public ActionResult ConsultarArchivosPersona(int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);
            List<Archivo> listaArchivo = new List<Archivo>();
            
            if (personaId == 0)
            {
                listaArchivo = (List<Archivo>)Session["ListaArchivos"];   
            }
            else
            {
                var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
                listaArchivo = archivos
              .Select(up => new Archivo
              {
                  PersonaId = up.PersonaId,
                  Nombre = up.Nombre,
                  ArchivoId = up.ArchivoPersonaId,
                  UrlArchivo = up.UrlArchivo,
                  TipoArchivoId = tipoArchivoId
              })
              .OrderBy(up => up.ArchivoId)
              .ToList();

                Session["ListaArchivos"] = listaArchivo;
            }

            return PartialView("_ListaArchivosPersona", listaArchivo);
        }
        public ActionResult ConsultarArchivosToma(int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);
            List<Archivo> listaArchivo = new List<Archivo>();

            
                var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
                listaArchivo = archivos
              .Select(up => new Archivo
              {
                  PersonaId = up.PersonaId,
                  Nombre = up.Nombre,
                  ArchivoId = up.ArchivoPersonaId,
                  UrlArchivo = up.UrlArchivo,
                  TipoArchivoId = tipoArchivoId
              })
              .OrderBy(up => up.ArchivoId)
              .ToList();

                Session["ListaArchivos"] = listaArchivo;
            

            return PartialView("_ListaArchivosToma", listaArchivo);
        }
        public ActionResult _ConsultarArchivosPersona(int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
            return PartialView("_ConsultarArchivosPersona", archivos);
        }
        public ActionResult _ConsultarArchivosToma(int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
            return PartialView("_ConsultarArchivosToma", archivos);
        }
        public ActionResult EliminarArchivoPersona(int archivoPersonaId, int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            var archivoPersona = archivosPersonaDomain.ObtenerArchivoPersona(archivoPersonaId);
            //Elimina
            archivosPersonaDomain.Eliminar(archivoPersonaId);

            //Elimina Archivo
            var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Persona/"), archivoPersona.UrlArchivo);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            //Consulta
            var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
            return PartialView("_ConsultarArchivosPersona", archivos);
        }
        public ActionResult EliminarArchivoToma(int archivoPersonaId, int personaId, int tipoArchivoId)
        {
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);

            var archivoPersona = archivosPersonaDomain.ObtenerArchivoPersona(archivoPersonaId);
            //Elimina
            archivosPersonaDomain.Eliminar(archivoPersonaId);

            //Elimina Archivo
            var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Persona/"), archivoPersona.UrlArchivo);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            //Consulta
            var archivos = archivosPersonaDomain.Obtener(personaId, tipoArchivoId);
            return PartialView("_ConsultarArchivosToma", archivos);
        }
        public ActionResult GuardarGasto(GastosViewModel model)
        {
            ArchivosGastoDomain archivosGastoDomain = new ArchivosGastoDomain(_context);
            GastosDomain gastosDomain = new GastosDomain(_context);
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            listaHttp = (List<ArchivoClass>)Session["ListaHttp"];

            Gasto gasto = new Gasto()
            {
                SucursalId = model.SucursalId,
                MultiComiteId = model.MultiComiteId,
                Concepto = AdsertiFunciones.FormatearTexto(model.Concepto),
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };

            gastosDomain.Gurdar(gasto);

            if (listaHttp != null)
            {
                foreach (var item in listaHttp)
                {
                    string archivo = item.nombre + "_" + item.FechaHora;

                    string rutaArchivo = @"/UploadFiles/Gastos/";

                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));                        
                    } // if (!Directory.Exists(ruta))

                    item.File.SaveAs(Server.MapPath(rutaArchivo + archivo));

                    ArchivoGasto archivoGasto = new ArchivoGasto()
                    {
                        Nombre = archivo,
                        GastoId = gasto.GastoId,
                        FechaAlta = DateTime.Now,
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                    };

                    archivosGastoDomain.Gurdar(archivoGasto);
                }
            }

            Session["ListaArchivos"] = null;
            Session["ListaHttp"] = null;
            ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
            return RedirectToAction("GastosComite", "Tomas");
        }
        public ActionResult GuardarPropietario(PropietariosPersonaFisicaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            var propietariosDomain = new PropietariosDomain(_context);
            var cambiosPropietarioDomain = new CambiosPropietarioDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var archivosPersonaDomain = new ArchivosPersonaDomain(_context);
            List<ArchivoClass> listaHttp = new List<ArchivoClass>();
            listaHttp = (List<ArchivoClass>)Session["ListaHttp"];


            //Si es cambio de propietario
            if (model.CambioPropietario)
            {
                var personaFisica = propietariosDomain.ObtenerPropietario(model.PropietarioId);
                var cambioPropietario = new CambioPropietario()
                {
                    PersonaId = personaFisica.Persona.PersonaFisica.PersonaId,
                    Nombre = AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.Nombre),
                    ApellidoPaterno = AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.ApellidoPaterno),
                    ApellidoMaterno = AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.ApellidoMaterno),
                    FechaNacimiento = !string.IsNullOrEmpty(personaFisica.Persona.PersonaFisica.FechaNacimiento.ToString()) ? Convert.ToDateTime(personaFisica.Persona.PersonaFisica.FechaNacimiento) : (DateTime?)null,
                    EstadoCivilId = personaFisica.Persona.PersonaFisica.EstadoCivilId,
                    Telefono = personaFisica.Persona.PersonaFisica.Telefono,
                    CorreoElectronico = personaFisica.Persona.PersonaFisica.CorreoElectronico,
                    Rfc = string.IsNullOrEmpty(personaFisica.Persona.PersonaFisica.Rfc) ? string.Empty : AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.Rfc),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaAlta = DateTime.Now
                };
                cambiosPropietarioDomain.Agregar(cambioPropietario);
                //Guarda Pago Cambio Propietario
                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.CambioPropietario,
                    TomaId = personaFisica.Toma.Select(t => t.TomaId).FirstOrDefault(),
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoCambioPropietario)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoCambioPropietario)),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    Activo = true
                };
                pagosDomain.Guardar(pago);
                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
                var tesorero = usuariosDomain.ObtenerUsuarioPorRol((int)RolesDomain.RolesEnum.Tesorero);
                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +
                                                         "CAJERO(A): {5}{0}",
                                                         Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         personaFisica.Toma.Select(t => t.Folio).FirstOrDefault(),
                                                         pago.Total.ToString("C"),
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pago.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = "CAMBIO PROPIETARIO ",
                    CantidadLetra = this.Convertir(pago.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    RenglonAdicional1 = "Nuevo: " + AdsertiFunciones.FormatearTexto(model.Nombre) + " " + AdsertiFunciones.FormatearTexto(model.ApellidoPaterno) + " " + AdsertiFunciones.FormatearTexto(model.ApellidoMaterno),
                    RenglonAdicional2 = "Anterior: " + cambioPropietario.Nombre + " " + cambioPropietario.ApellidoPaterno + " " + cambioPropietario.ApellidoMaterno,
                    //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
                };
                recibosDomain.Gurdar(recibo);
                
                //Descarga archivo
                var tomasVM = new TomasViewModel()
                {
                    TomaId = personaFisica.Toma.Select(t => t.TomaId).FirstOrDefault(),
                    Folio = personaFisica.Toma.Select(t => t.Folio).FirstOrDefault(),
                    Tesorero = tesorero != null ? "C. " + tesorero.Persona.Nombre + " " + tesorero.Persona.ApellidoPaterno + " " + tesorero.Persona.ApellidoMaterno : "(TESORERO DESHABILITADO)",
                    Propietario = "C. " + AdsertiFunciones.FormatearTexto(model.Nombre) + " " +
                                      AdsertiFunciones.FormatearTexto(model.ApellidoPaterno) + " " +
                                      AdsertiFunciones.FormatearTexto(model.ApellidoMaterno),
                    Calle = personaFisica.Toma.Select(d => d.Direccion) != null ? ((personaFisica.Toma.Select(d => d.Direccion.TipoCalleId).FirstOrDefault() > 0 ? personaFisica.Toma.Select(d => d.Direccion.TiposCalle.Nombre).FirstOrDefault() : string.Empty) + ' ' + (personaFisica.Toma.Select(d => d.Direccion.CalleId).FirstOrDefault() > 0 ? personaFisica.Toma.Select(d => d.Direccion.Calles.Nombre).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(personaFisica.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault()) ? " INT " + personaFisica.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(personaFisica.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault()) ? " EXT " + personaFisica.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault() : string.Empty)) : String.Empty,
                    Colonia = personaFisica.Toma.Select(d => d.Direccion) != null ? (personaFisica.Toma.Select(d => d.Direccion.ColoniaId).FirstOrDefault() > 0 ? personaFisica.Toma.Select(d => d.Direccion.Colonias.Nombre).FirstOrDefault() : string.Empty) : string.Empty,
                    
                };
                // generar archivo word
                var fechaLetra = this.ConvertirFecha(DateTime.Now.Day.ToString()).ToUpper() + " DÍAS DEL MES DE " +                            
                             DateTime.Now.ToString("MMMMMMMM").ToUpper() + " DE " +
                             DateTime.Now.Year;
                var cantidadLetra = this.ConvertirMinusculas(pago.Total.ToString()).ToLower() + " M.N";
                string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/ContratoCambioPropietario.docx");
                string rutaArchivo = @"/UploadFiles/ContratoCambioPropietario/";
                string nombreArchivo = "Contrato Cambio Propietario_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";

                using (DocX document = DocX.Load(rutaFormato))
                {
                    document.ReplaceText("{Tesorero}", tomasVM.Tesorero, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Propietario}", tomasVM.Propietario, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Direccion}", tomasVM.Calle + ", COL. " + tomasVM.Colonia + ", " + "SAN BUENAVENTURA", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Precio}", model.CostoCambioPropietario + " " + cantidadLetra, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Fecha}", fechaLetra, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    //Si no existe el directorio se crea
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                    } // if (!Directory.Exists(ruta))
                    document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
                }

                var urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;

                // fin archivo word
                if (!string.IsNullOrEmpty(urlArchivo))
                {
                    FileInfo archivo = new FileInfo(urlArchivo);

                    if (archivo.Exists)
                    {
                        Response.ClearContent();
                        Response.AppendCookie(new HttpCookie("fileDownloadToken", model.DownloadToken));
                        Response.AppendCookie(new HttpCookie("reciboId", recibo.ReciboId.ToString()));
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                        Response.AddHeader("Content-Length", archivo.Length.ToString());
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        Response.TransmitFile(archivo.FullName);
                        Response.End();
                    }
                }
                
            }//if (model.CambioPropietario)

            var propietario = new Propietario()
            {
                Persona = new Persona()
                {        
                    PersonaId = model.PersonaId,
                    PersonalidadJuridicaId = (int)PersonalidadesJuridicasDomain.PersonalidadesJuridicasEnum.PersonalidadJuridicaFisica,
                    MultiComiteId = Convert.ToInt32(Session["MultiComiteId"].ToString()),
                    SucursalId = Convert.ToInt32(Session["SucursalId"].ToString()),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaAlta = DateTime.Now,   
                    TipoPersonaId = (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica,
                    PersonaFisica = new PersonaFisica()
                    {              
                        PersonaId = model.PersonaId,
                        Nombre = AdsertiFunciones.FormatearTexto(model.Nombre),
                        ApellidoPaterno = AdsertiFunciones.FormatearTexto(model.ApellidoPaterno),
                        ApellidoMaterno = AdsertiFunciones.FormatearTexto(model.ApellidoMaterno),
                        FechaNacimiento = !string.IsNullOrEmpty(model.FechaNacimiento) ? Convert.ToDateTime(model.FechaNacimiento) : (DateTime?)null,
                        EstadoCivilId = model.EstadoCivilId,
                        Telefono = model.Telefono,
                        CorreoElectronico = model.CorreoElectronico,
                        Rfc = model.Rfc == null ? string.Empty : AdsertiFunciones.FormatearTexto(model.Rfc),
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                        FechaAlta =DateTime.Now
                    }
                },
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now,
                PropietarioId = model.PropietarioId > 0 ? Convert.ToInt32(model.PropietarioId) : 0,
                PersonaId = model.PersonaId 
            };           
            propietariosDomain.Guardar(propietario);

            //Agrega Archivos Persona            
            if (listaHttp != null)
            {
                foreach (var item in listaHttp)
                {
                    string archivo = item.nombre + "_" + item.FechaHora;

                    string rutaArchivo = @"/UploadFiles/Persona/";

                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                    } // if (!Directory.Exists(ruta))

                    item.File.SaveAs(Server.MapPath(rutaArchivo + archivo));

                    ArchivoPersona archivoGasto = new ArchivoPersona()
                    {
                        Nombre = item.nombre,
                        PersonaId = propietario.PersonaId,
                        UrlArchivo = archivo,
                        TipoArchivoId = (int)TiposArchivosDomain.TiposArchivosDomainEnum.TerceraEdad,
                        FechaAlta = DateTime.Now,
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                    };

                    archivosPersonaDomain.Guardar(archivoGasto);
                }
            }

            Session["ListaArchivos"] = null;
            Session["ListaHttp"] = null;

            if (model.CambioPropietario)
            {
                return null;
            }
            else
            {
                ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
                return RedirectToAction("Tabs", "Tomas", new { propietarioId = propietario.PropietarioId, cambioPropietario = false });
            }
            
        }
        public ActionResult GuardarPropietarioMoral(PropietarioPersonaMoralViewModel model)
        {
            
            var propietariosDomain = new PropietariosDomain(_context);
            var cambioPropietarioPersonaMoralDomain = new CambioPropietarioPersonaMoralDomain(_context);
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);

            //Si es cambio de propietario
            if (model.CambioPropietario)
            {
                var personaMoral = propietariosDomain.ObtenerPropietario(model.PropietarioId);
                var cambioPropietarioPersonaMoral = new CambioPropietarioPersonaMoral()
                {
                    PersonaId = personaMoral.Persona.PersonaMoral.PersonaId,
                    Nombre = AdsertiFunciones.FormatearTexto(personaMoral.Persona.PersonaMoral.Nombre),                   
                    CorreoElectronico = personaMoral.Persona.PersonaMoral.CorreoElectronico,
                    Rfc = string.IsNullOrEmpty(personaMoral.Persona.PersonaMoral.Rfc) ? string.Empty : AdsertiFunciones.FormatearTexto(personaMoral.Persona.PersonaMoral.Rfc),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaAlta = DateTime.Now
                };
                cambioPropietarioPersonaMoralDomain.Agregar(cambioPropietarioPersonaMoral);
                //Guarda Pago Cambio Propietario
                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.CambioPropietario,
                    TomaId = personaMoral.Toma.Select(t => t.TomaId).FirstOrDefault(),
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoCambioPropietario)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.CostoCambioPropietario)),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    Activo = true
                };
                pagosDomain.Guardar(pago);
                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
                var tesorero = usuariosDomain.ObtenerUsuarioPorRol((int)RolesDomain.RolesEnum.Tesorero);
                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +
                                                         "CAJERO(A): {5}{0}",
                                                         Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         personaMoral.Toma.Select(t => t.Folio).FirstOrDefault(),
                                                         pago.Total.ToString("C"),
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pago.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = "CAMBIO PROPIETARIO ",
                    CantidadLetra = this.Convertir(pago.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    RenglonAdicional1 = "Nuevo: " + AdsertiFunciones.FormatearTexto(model.Nombre),
                    RenglonAdicional2 = "Anterior: " + cambioPropietarioPersonaMoral.Nombre,
                    //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
                };
                recibosDomain.Gurdar(recibo);

                //Descarga archivo
                var tomasVM = new TomasViewModel()
                {
                    TomaId = personaMoral.Toma.Select(t => t.TomaId).FirstOrDefault(),
                    Folio = personaMoral.Toma.Select(t => t.Folio).FirstOrDefault(),
                    Tesorero = tesorero != null ? "C. " + tesorero.Persona.Nombre + " " + tesorero.Persona.ApellidoPaterno + " " + tesorero.Persona.ApellidoMaterno : "(TESORERO DESHABILITADO)",
                    Propietario = AdsertiFunciones.FormatearTexto(model.Nombre),
                    Calle = personaMoral.Toma.Select(d => d.Direccion) != null ? ((personaMoral.Toma.Select(d => d.Direccion.TipoCalleId).FirstOrDefault() > 0 ? personaMoral.Toma.Select(d => d.Direccion.TiposCalle.Nombre).FirstOrDefault() : string.Empty) + ' ' + (personaMoral.Toma.Select(d => d.Direccion.CalleId).FirstOrDefault() > 0 ? personaMoral.Toma.Select(d => d.Direccion.Calles.Nombre).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(personaMoral.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault()) ? " INT " + personaMoral.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(personaMoral.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault()) ? " EXT " + personaMoral.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault() : string.Empty)) : String.Empty,
                    Colonia = personaMoral.Toma.Select(d => d.Direccion) != null ? (personaMoral.Toma.Select(d => d.Direccion.ColoniaId).FirstOrDefault() > 0 ? personaMoral.Toma.Select(d => d.Direccion.Colonias.Nombre).FirstOrDefault() : string.Empty) : string.Empty,

                };
                // generar archivo word
                var fechaLetra = this.ConvertirFecha(DateTime.Now.Day.ToString()).ToUpper() + " DÍAS DEL MES DE " +
                             DateTime.Now.ToString("MMMMMMMM").ToUpper() + " DE " +
                             DateTime.Now.Year;
                var cantidadLetra = this.ConvertirMinusculas(pago.Total.ToString()).ToLower() + " M.N";
                string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/ContratoCambioPropietario.docx");
                string rutaArchivo = @"/UploadFiles/ContratoCambioPropietario/";
                string nombreArchivo = "Contrato Cambio Propietario_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";

                using (DocX document = DocX.Load(rutaFormato))
                {
                    document.ReplaceText("{Tesorero}", tomasVM.Tesorero, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Propietario}", tomasVM.Propietario, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Direccion}", tomasVM.Calle + ", COL. " + tomasVM.Colonia + ", " + "SAN BUENAVENTURA", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Precio}", model.CostoCambioPropietario + " " + cantidadLetra, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Fecha}", fechaLetra, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    //Si no existe el directorio se crea
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                    } // if (!Directory.Exists(ruta))
                    document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
                }

                var urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;

                // fin archivo word
                if (!string.IsNullOrEmpty(urlArchivo))
                {
                    FileInfo archivo = new FileInfo(urlArchivo);

                    if (archivo.Exists)
                    {
                        Response.ClearContent();
                        Response.AppendCookie(new HttpCookie("fileDownloadToken", model.DownloadTokenMoral));
                        Response.AppendCookie(new HttpCookie("reciboId", recibo.ReciboId.ToString()));
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                        Response.AddHeader("Content-Length", archivo.Length.ToString());
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        Response.TransmitFile(archivo.FullName);
                        Response.End();
                    }
                }

            }//if (model.CambioPropietario)

            var propietario = new Propietario()
            {
                Persona = new Persona()
                {
                    PersonaId = model.PersonaId,
                    PersonalidadJuridicaId = (int)PersonalidadesJuridicasDomain.PersonalidadesJuridicasEnum.PersonalidadJuridicaMoral,
                    MultiComiteId = Convert.ToInt32(Session["MultiComiteId"].ToString()),
                    SucursalId = Convert.ToInt32(Session["SucursalId"].ToString()),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaAlta = DateTime.Now,
                    TipoPersonaId = (int)TipoPersonaDomain.TipoPersonaEnum.PersonaMoral,
                    PersonaMoral = new PersonaMoral()
                    {
                        PersonaId = model.PersonaId,
                        Nombre = AdsertiFunciones.FormatearTexto(model.Nombre),                      
                        CorreoElectronico = model.CorreoElectronico,
                        Rfc = model.Rfc == null ? string.Empty : AdsertiFunciones.FormatearTexto(model.Rfc),
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                        FechaAlta = DateTime.Now
                    }
                },
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now,
                PropietarioId = model.PropietarioId > 0 ? Convert.ToInt32(model.PropietarioId) : 0,
                PersonaId = model.PersonaId
            };
            propietariosDomain.Guardar(propietario);
            if (model.CambioPropietario)
            {
                return null;
            }
            else
            {
                ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
                return RedirectToAction("Tabs", "Tomas", new { propietarioId = propietario.PropietarioId, cambioPropietario = false });
            }

        }
        public ActionResult GuardarToma(TomasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Tabs", "Tomas", new { propietarioId = model.PropietarioId });               
            }

            var tomasDomain = new TomasDomain(_context);

            if(tomasDomain.ValidarTomaExistente(Convert.ToInt32(model.Folio), model.TomaId))
            {
                ShowToastMessage("Alerta", "El folio ya se encuentra en existencia", ToastMessage.ToastType.Warning);
                return RedirectToAction("Tabs", "Tomas", new { propietarioId = model.PropietarioId });
                
            }

            var toma = new Toma()
            {
                TomaId = model.TomaId,
                CategoriaId = model.CategoriaId,
                PropietarioId = model.PropietarioId,
                Folio = Convert.ToInt32(model.Folio),
                Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now,               
                LiquidacionTomaId = (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaNueva,
                Activa = model.Activa,
                DireccionId = model.DireccionId,
                Pasiva = model.Pasiva                
            };

            tomasDomain.Guardar(toma);

            ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
            return RedirectToAction("Tabs", "Tomas", new { propietarioId = toma.PropietarioId });

        }
        public ActionResult GuardarDireccion(DireccionesViewModel model)
        {
            var usuariosDomain = new UsuariosDomain(_context);
            var propietariosDomain = new PropietariosDomain(_context);
            var direccionesDomain = new DireccionesDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            var direccion = new Direccion()
            {
                DireccionId = model.DireccionId,
                CalleId = model.CalleId,
                NumInt = model.NumInt,
                NumExt = model.NumExt,
                ColoniaId = model.ColoniaId,
                TipoCalleId = model.TipoCalleId,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now
            };

            direccionesDomain.Guardar(direccion);

            if (model.DireccionId == 0)
            {
                var toma = new Toma()
                {
                    TomaId = model.TomaId,
                    DireccionId = direccion.DireccionId,
                    UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    FechaCambio = DateTime.Now,
                    Activa = true
                };
                tomasDomain.EditarDireccionToma(toma);
            }

            if (model.DireccionId == 0)
            {
                //Descarga contrato toma Nueva
                var personaFisica = propietariosDomain.ObtenerPropietario(model.PropietarioId);
                var presidente = usuariosDomain.ObtenerUsuarioPorRol((int)RolesDomain.RolesEnum.Presidente);

                var tomasVM = new TomasViewModel()
                {
                    TomaId = personaFisica.Toma.Select(t => t.TomaId).FirstOrDefault(),
                    Folio = personaFisica.Toma.Select(t => t.Folio).FirstOrDefault(),
                    Presidente = presidente != null ? "C. " + presidente.Persona.Nombre + " " + presidente.Persona.ApellidoPaterno + " " + presidente.Persona.ApellidoMaterno : "(PRESIDENTE DESHABILITADO)",
                    Propietario = model.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? "C. " + AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.Nombre) + " " +
                                          AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.ApellidoPaterno) + " " +
                                          AdsertiFunciones.FormatearTexto(personaFisica.Persona.PersonaFisica.ApellidoMaterno) : personaFisica.Persona.PersonaMoral.Nombre,
                    Calle = personaFisica.Toma.Select(d => d.Direccion) != null ? ((personaFisica.Toma.Select(d => d.Direccion.TipoCalleId).FirstOrDefault() > 0 ? personaFisica.Toma.Select(d => d.Direccion.TiposCalle.Nombre).FirstOrDefault() : string.Empty) + ' ' + (personaFisica.Toma.Select(d => d.Direccion.CalleId).FirstOrDefault() > 0 ? personaFisica.Toma.Select(d => d.Direccion.Calles.Nombre).FirstOrDefault() : string.Empty) +
                                            (!string.IsNullOrEmpty(personaFisica.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault()) ? " INT " + personaFisica.Toma.Select(d => d.Direccion.NumInt).FirstOrDefault() : string.Empty) +
                                            (!string.IsNullOrEmpty(personaFisica.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault()) ? " EXT " + personaFisica.Toma.Select(d => d.Direccion.NumExt).FirstOrDefault() : string.Empty)) : String.Empty,
                    Colonia = personaFisica.Toma.Select(d => d.Direccion) != null ? (personaFisica.Toma.Select(d => d.Direccion.ColoniaId).FirstOrDefault() > 0 ? "COL. " + personaFisica.Toma.Select(d => d.Direccion.Colonias.Nombre).FirstOrDefault() : string.Empty) : string.Empty,
                    Precio = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Precio)).ToString("C")
                };
                var fechaLetra = this.ConvertirFecha(DateTime.Now.Day.ToString()).ToUpper() + " DÍAS DEL MES DE " +
                                DateTime.Now.ToString("MMMMMMMM").ToUpper() + " DE " +
                                DateTime.Now.Year;
                var cantidadLetra = this.ConvertirMinusculas(AdsertiFunciones.FormatearNumero(model.Precio)).ToLower() + " M.N";
                string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/ContratoTomaNueva.docx");
                string rutaArchivo = @"/UploadFiles/ContratoTomaNueva/";
                string nombreArchivo = "Contrato Toma Nueva_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";

                using (DocX document = DocX.Load(rutaFormato))
                {
                    document.ReplaceText("{Presidente}", tomasVM.Presidente, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Propietario}", tomasVM.Propietario, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Calle}", tomasVM.Calle, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Colonia}", tomasVM.Colonia, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Precio}", model.Precio + " (" + cantidadLetra + ")", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Fecha}", fechaLetra, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    //Si no existe el directorio se crea
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                    } // if (!Directory.Exists(ruta))
                    document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
                }

                var urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;

                // fin archivo word
                if (!string.IsNullOrEmpty(urlArchivo))
                {
                    FileInfo archivo = new FileInfo(urlArchivo);
                    if (archivo.Exists)
                    {
                        Response.ClearContent();
                        Response.AppendCookie(new HttpCookie("fileDownloadToken", model.DownloadTokenFileDireccion));
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                        Response.AddHeader("Content-Length", archivo.Length.ToString());
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        Response.TransmitFile(archivo.FullName);
                        Response.End();
                    }
                }
            }
            if (model.DireccionId > 0)
            {
                ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
            }
            
            return RedirectToAction("Tabs", "Tomas", new { propietarioId = model.PropietarioId });
        }
        public ActionResult GuardarConvenio(ConveniosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Tabs(model.PropietarioId);
            }

            var conveniosDomain = new ConveniosDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            var convenio = new Convenio()
            {
                ConvenioId = model.ConvenioId,
                ConceptoConvenioId = (int)ConceptosConvenioDomain.ConceptosConvenioDomainEnum.TomaNueva,
                TomaId = model.TomaId,
                EstatusConvenioId = (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo,
                PeriodoPagoConvenioId = model.PeriodoPagoConvenioId,
                PeriodoInicio = Convert.ToDateTime(model.PeriodoInicio),
                PeriodoFin = Convert.ToDateTime(model.PeriodoFin),
                FechaInicio = Convert.ToDateTime(model.FechaInicio),
                FechaFin = Convert.ToDateTime(model.FechaFin),
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,                
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };

            conveniosDomain.Guardar(convenio);
            tomasDomain.EditarActiva(model.TomaId, true, Convert.ToInt32(Session["UsuarioId"].ToString()));

            ShowToastMessage("Éxito", "Guardado exitosamente", ToastMessage.ToastType.Success);
            return RedirectToAction("Index", "Home");

        }
        public ActionResult GuardarPagoTomaNueva(PagosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            var pagosDomain = new PagosDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);

            var pago = new Pago()
            {
                PagoId = model.PagoId,
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva,
                TomaId = model.TomaId,
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now,
                Activo = true
            };                     

            pagosDomain.Guardar(pago);

            var periodoPago = new PeriodoPago()
            {
                PeriodoPagoId = model.PeriodoPagoId != null ? Convert.ToInt32(model.PeriodoPagoId) : 0,
                TomaId = model.TomaId,
                PagoId = pago.PagoId,
                MesAnoInicio = Convert.ToDateTime(DateTime.Now.ToString("yyyy MM")),
                MesAnoFin = Convert.ToDateTime(DateTime.Now.ToString("yyyy MM")),
                UltimoPeriodoPago = DateTime.Now.ToString("MMMMMMMMM yyyy").ToUpper(),
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now
            };            

            periodosPagoDomain.Guardar(periodoPago);
            tomasDomain.EditarActiva(Convert.ToInt32(pago.TomaId), true, Convert.ToInt32(Session["UsuarioId"].ToString()));

            return RedirectToAction("Index","Home");

        }
        public ActionResult GuardarPagoConvenio(PagosViewModel model)
        {           
            var pagosDomain = new PagosDomain(_context);
            var conveniosDomain = new ConveniosDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var notificacionesDomain = new NotificacionesDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);

            bool finalizarConvenio = (Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)) + model.TotalPagosConvenio) >= model.TotalConvenio;

            var notificacion = notificacionesDomain.ObtenerNotificacion(model.TomaId);           

            if (finalizarConvenio)
            {
                var convenio = conveniosDomain.ObtenerConvenio(Convert.ToInt32(model.ConvenioId));
                conveniosDomain.CambiarEstatusConvenio(Convert.ToInt32(model.ConvenioId),Convert.ToInt32(EstatusConvenioDomain.EstatusConvenioEnum.Concluido), Convert.ToInt32(Session["UsuarioId"].ToString()));

                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = model.TomaId,                   
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),                   
                    FechaAlta = Convert.ToDateTime(model.FechaAltaFormato),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    ConvenioId = convenio.ConvenioId,
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.FolioTarjeta,
                    Activo = true
                };

                pagosDomain.Guardar(pago);

                // finaliza notificacion
                if (pago.NotificacionId != null)
                {
                    notificacionesDomain.DesactivarNotificacion(Convert.ToInt32(pago.NotificacionId), pago.UsuarioAltaId);
                }

                var periodoPago = new PeriodoPago()
                {
                    TomaId = model.TomaId,
                    PagoId = pago.PagoId,
                    MesAnoInicio = convenio.PeriodoInicio,
                    MesAnoFin = convenio.PeriodoFin,
                    UltimoPeriodoPago = Convert.ToDateTime(convenio.PeriodoFin).ToString("MMM-yyyy").ToUpper(),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                periodosPagoDomain.Guardar(periodoPago);               
                return RedirectToAction("Index", "Home");
            }
            else
            {               
                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = model.TomaId,
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),                    
                    FechaAlta = Convert.ToDateTime(model.FechaAltaFormato),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    ConvenioId = model.ConvenioId,
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.FolioTarjeta,      
                    Activo = true
                };

                pagosDomain.Guardar(pago);              

                return RedirectToAction("Index", "Home");
            }
               
        }
        public ActionResult GuardarPagoConvenioRecibo(PagosViewModel model)
        {
            var pagosDomain = new PagosDomain(_context);
            var conveniosDomain = new ConveniosDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var notificacionesDomain = new NotificacionesDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            bool finalizarConvenio = (Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)) + model.TotalPagosConvenio) >= model.TotalConvenio;

            var notificacion = notificacionesDomain.ObtenerNotificacion(model.TomaId);

            if (finalizarConvenio)
            {
                var convenio = conveniosDomain.ObtenerConvenio(Convert.ToInt32(model.ConvenioId));
                conveniosDomain.CambiarEstatusConvenio(Convert.ToInt32(model.ConvenioId), Convert.ToInt32(EstatusConvenioDomain.EstatusConvenioEnum.Concluido), Convert.ToInt32(Session["UsuarioId"].ToString()));
                var toma = tomasDomain.ObtenerToma(model.TomaId);
               
                var pago = new Pago()
                {
                    ConceptoPagoId = toma.LiquidacionTomaId == (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaNueva ? (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva : (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = model.TomaId,                    
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),                    
                    FechaAlta = Convert.ToDateTime(model.FechaAltaFormato),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    ConvenioId = convenio.ConvenioId,
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.FolioTarjeta,
                    Activo = true
                };

                pagosDomain.Guardar(pago);
                if (toma.LiquidacionTomaId == (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaNueva)
                {
                    tomasDomain.EditarConceptoPago(model.TomaId, (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaExistente, Convert.ToInt32(Session["UsuarioId"].ToString()));
                }
                // finaliza notificacion
                if (pago.NotificacionId != null)
                {
                    notificacionesDomain.DesactivarNotificacion(Convert.ToInt32(pago.NotificacionId), pago.UsuarioAltaId);
                }

                var periodoPago = new PeriodoPago()
                {
                    TomaId = model.TomaId,
                    PagoId = pago.PagoId,
                    MesAnoInicio = convenio.PeriodoInicio,
                    MesAnoFin = convenio.PeriodoFin,
                    UltimoPeriodoPago = Convert.ToDateTime(convenio.PeriodoFin).ToString("MMM-yyyy").ToUpper(),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                periodosPagoDomain.Guardar(periodoPago);

                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));

                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +                                                        
                                                         "CAJERO(A): {5}{0}",
                                           Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         convenio.Toma.Folio,
                                                         convenio.Total.ToString("C"),                                                        
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pago.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                    CantidadLetra = this.Convertir(pago.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                recibosDomain.Gurdar(recibo);

                Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);
            }
            else
            {
                var convenio = conveniosDomain.ObtenerConvenio(Convert.ToInt32(model.ConvenioId));               

                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = model.TomaId,
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),                    
                    FechaAlta = Convert.ToDateTime(model.FechaAltaFormato),
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    ConvenioId = model.ConvenioId,
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.FolioTarjeta,
                    Activo = true
                };

                pagosDomain.Guardar(pago);

                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));

                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +                                                         
                                                         "CAJERO(A): {5}{0}",
                                                         Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         convenio.Toma.Folio,
                                                         convenio.Total.ToString("C"),
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pago.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                    CantidadLetra = this.Convertir(pago.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                recibosDomain.Gurdar(recibo);

                Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);
               
            }
            return HttpNotFound();
        }
        public ActionResult GuardarPagoSuministroAgua(PagosViewModel model)
        {
            var pagosDomain = new PagosDomain(_context);
            var notificacionesDomain = new NotificacionesDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var descuentosPagosDomain = new DescuentosPagosDomain(_context);

            // Cuando es Toma Nueva solo guarda pago y recibo
            if (model.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
            {
                var pagotomaNueva = new Pago()
                {
                    ConceptoPagoId = model.ConceptoPagoId,
                    TomaId = model.TomaId,
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PrecioToma)),
                    Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    NoRecibo = model.FolioTarjeta,
                    Activo = true,
                    CostoToma = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PrecioToma))
                };
                pagosDomain.Guardar(pagotomaNueva);
                var periodoPagoTomaNueva = new PeriodoPago()
                {
                    TomaId = model.TomaId,
                    PagoId = pagotomaNueva.PagoId,
                    MesAnoInicio = DateTime.Now,
                    MesAnoFin = DateTime.Now,
                    UltimoPeriodoPago = DateTime.Now.ToString("MMMMMMMMM yyyy").ToUpper(),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                periodosPagoDomain.Guardar(periodoPagoTomaNueva);

                tomasDomain.EditarConceptoPago(model.TomaId, (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaExistente, Convert.ToInt32(Session["UsuarioId"].ToString()));

                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
                var toma = tomasDomain.ObtenerToma(model.TomaId);

                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +
                                                         "CAJERO(A): {5}{0}",
                                                         Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         toma.Folio,
                                                         pagotomaNueva.Total.ToString("C"),
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pagotomaNueva.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                    CantidadLetra = this.Convertir(pagotomaNueva.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                    RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                    RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
                };

                recibosDomain.Gurdar(recibo);

                Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);

                return View();
            }
            else
            {

                var notificacion = notificacionesDomain.ObtenerNotificacion(model.TomaId);

                int mes = 12 - DateTime.Now.Month;
                var periodoFin = String.Format("{0:yyyy-MM}", DateTime.Now.AddMonths(mes));

                // Si esta notificada finaliza notificacion si esta totalmente liquidada
                if (notificacion != null)
                {
                    if (Convert.ToDateTime(model.MesAnoFin) >= Convert.ToDateTime(periodoFin))
                    {
                        notificacionesDomain.DesactivarNotificacion(Convert.ToInt32(notificacion.NotificacionId), notificacion.UsuarioAltaId);
                    }
                }

                var pago = new Pago()
                {
                    ConceptoPagoId = model.ConceptoPagoId,
                    TomaId = model.TomaId,
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                    Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.FolioTarjeta,
                    Activo = true,
                    DescuentProntoPago = !string.IsNullOrEmpty(model.DescuentoProntoPago) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.DescuentoProntoPago)) : (decimal?)null,
                    CostoToma = !string.IsNullOrEmpty(model.PrecioToma) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PrecioToma)) : (decimal?)null,
                    DescuentoMadreSoltera = !string.IsNullOrEmpty(model.DescuentoMadreSoltera) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.DescuentoMadreSoltera)) : (decimal?)null,
                    DescuentoTerceraEdad = !string.IsNullOrEmpty(model.DescuentoTerceraEdad) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.DescuentoTerceraEdad)) : (decimal?)null
                };

                pagosDomain.Guardar(pago);

                var periodoPago = new PeriodoPago()
                {
                    TomaId = model.TomaId,
                    PagoId = pago.PagoId,
                    MesAnoInicio = Convert.ToDateTime(model.MesAnoInicio),
                    MesAnoFin = Convert.ToDateTime(model.MesAnoFin),
                    UltimoPeriodoPago = Convert.ToDateTime(model.MesAnoFin).ToString("MMMMMMMMM yyyy").ToUpper(),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
                };

                periodosPagoDomain.Guardar(periodoPago);

                if (Session["Abonos"] != null) pagosDomain.EditarAbonosActivos((List<Pago>)Session["Abonos"]);

                if (model.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
                {
                    tomasDomain.EditarConceptoPago(model.TomaId, (int)LiquidacionesTomaDomain.LiquidacionesTomaEnum.TomaExistente, Convert.ToInt32(Session["UsuarioId"].ToString()));
                }

                //Guarda descuento tercera edad
                if (model.DescuentoTerceraEdadId != null)
                {
                    var descuentoPago = new DescuentoPago()
                    {
                        DescuentoVariosId = Convert.ToInt32(model.DescuentoTerceraEdadId),
                        PagoId = pago.PagoId,
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                        FechaAlta = DateTime.Now
                    };
                    descuentosPagosDomain.Agregar(descuentoPago);
                }

                //Guarda descuento madre soltera 
                if (model.DescuentoMadreSolteraId != null)
                {
                    var descuentoPago = new DescuentoPago()
                    {
                        DescuentoVariosId = Convert.ToInt32(model.DescuentoMadreSolteraId),
                        PagoId = pago.PagoId,
                        UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                        FechaAlta = DateTime.Now
                    };
                    descuentosPagosDomain.Agregar(descuentoPago);
                }

                //Guarda recibo
                var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
                var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
                var toma = tomasDomain.ObtenerToma(model.TomaId);

                string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                         "FECHA: {2}{0}" +
                                                         "CLAVE DE CONTROL: {3}{0}" +
                                                         "CANTIDAD PAGADA: {4}{0}" +
                                                         "CAJERO(A): {5}{0}",
                                                         Environment.NewLine, noRecibo,
                                                         DateTime.Now.ToString("dd/MM/yyyy"),
                                                         toma.Folio,
                                                         pago.Total.ToString("C"),
                                                         usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
                //Genera codigoQR
                var QQRurl = this.GenerarCodigoQR(informacionRecibo);

                var recibo = new Recibo()
                {
                    PagoId = pago.PagoId,
                    Fecha = DateTime.Now,
                    CodigoQRurl = QQRurl,
                    NoRecibo = noRecibo,
                    Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                    Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                    CantidadLetra = this.Convertir(pago.Total.ToString()),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                    RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                    RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
                };

                recibosDomain.Gurdar(recibo);

                Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);

                return View();
            }
        }
        public ActionResult GuardarPagoAbono(PagosViewModel model)
        {
            var pagosDomain = new PagosDomain(_context);
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var totalAbonos = (List<Pago>)Session["Abonos"];

            var pago = new Pago()
            {
                ConceptoPagoId = model.ConceptoPagoId,
                TomaId = model.TomaId,
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                Abono = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Abono)) == 0 ?
                        Convert.ToDateTime(model.MesAnoInicio) == Convert.ToDateTime(model.MesAnoFin) ?
                        Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)) :
                        (decimal?)null : Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Abono)),
                Activo = (Convert.ToDateTime(model.MesAnoInicio) == Convert.ToDateTime(model.MesAnoFin)) || Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Abono)) > 0
            };

            pagosDomain.Guardar(pago);

            var periodoPago = new PeriodoPago()
            {
                TomaId = model.TomaId,
                PagoId = pago.PagoId,
                MesAnoInicio = Convert.ToDateTime(model.MesAnoInicio),
                MesAnoFin = Convert.ToDateTime(model.MesAnoFin),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                UltimoPeriodoPago = Convert.ToDateTime(model.MesAnoFin).ToString("MMMMMMMMM yyyy").ToUpper()
            };

            periodosPagoDomain.Guardar(periodoPago);

            if (Convert.ToDateTime(model.MesAnoInicio) != Convert.ToDateTime(model.MesAnoFin))
            {
                if (Session["Abonos"] != null) pagosDomain.EditarAbonosActivos((List<Pago>)Session["Abonos"]);
            }

            //return RedirectToAction("Index", "Home");
            Response.Redirect("~/Print/Recibo.aspx?tomaId=" + model.TomaId + "&totalAbono=" + totalAbonos.Sum(a => a.Abono));
            return View();
        }        
        public ActionResult GuardarConvenioSuministroAgua(ConveniosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Tabs(model.PropietarioId);
            }

            var conveniosDomain = new ConveniosDomain(_context);
            var personaSeguridadDomain = new PersonasSeguridadDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var periodosPagoConvenioDomain = new PeriodosPagoConvenioDomain(_context);
            string urlArchivo = string.Empty;

            var personaSeguridad = personaSeguridadDomain.ObtenerPersonaSeguridad(model.PersonaId);
            var toma = tomasDomain.ObtenerToma(model.TomaId);
            var periodoPagoConvenio = periodosPagoConvenioDomain.ObtenerPeriodoPagoConvenio(model.PeriodoPagoConvenioId);

            // generar archivo word
            //if (model.ConvenioId == 0) { 
                string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/Convenios.docx");
                string rutaArchivo = @"/UploadFiles/Convenios/";
                string nombreArchivo = DateTime.Now.ToString("_ddMMyyyyHHmmss") + ".docx";

                nombreArchivo = "Convenio" + nombreArchivo;

                using (DocX document = DocX.Load(rutaFormato))
                {                                
                    document.ReplaceText("{NombreRepresentante}", personaSeguridad.Nombre + " " + personaSeguridad.ApellidoPaterno + " " + personaSeguridad.ApellidoMaterno, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Cargo}", personaSeguridad.Cargo, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{NombreUsuario}", toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? "C. " + toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                     toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                     toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : toma.Propietario.Persona.PersonaMoral.Nombre
                                         , false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Direccion}", toma.Direccion.Calle + (!string.IsNullOrEmpty(toma.Direccion.NumExt) ?
                                         " NO EXT " + toma.Direccion.NumExt : string.Empty) + (!string.IsNullOrEmpty(toma.Direccion.NumInt) ?
                                         " NO INT " + toma.Direccion.NumInt : string.Empty), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{AdeudoAñoInicio}", Convert.ToDateTime(model.PeriodoInicio).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{AdeudoAñoFin}", Convert.ToDateTime(model.PeriodoFin).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Total}", Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)).ToString("C"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{DiaInicio}", Convert.ToDateTime(model.FechaInicio).ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{MesInicio}", Convert.ToDateTime(model.FechaInicio).ToString("MMMMMMMMMM", new CultureInfo("es-ES")).ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{AñoInicio}", Convert.ToDateTime(model.FechaInicio).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{DiaFin}", Convert.ToDateTime(model.FechaFin).ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{MesFin}", Convert.ToDateTime(model.FechaFin).ToString("MMMMMMMMMM", new CultureInfo("es-ES")).ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{AñoFin}", Convert.ToDateTime(model.FechaFin).ToString("yyyy"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{PrimerPago}", !string.IsNullOrEmpty(model.PimerPago) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PimerPago)).ToString("C") : "N/A", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{PagosDe}", !string.IsNullOrEmpty(model.PagosDe) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PagosDe)).ToString("C") : "N/A", false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{Periodos}", periodoPagoConvenio.Nombre, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{DiaHoy}", DateTime.Now.ToString("dd"), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{MesHoy}", DateTime.Now.ToString("MMMMMMMMMM", new CultureInfo("es-ES")).ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    document.ReplaceText("{AñoHoy}", DateTime.Now.ToString("yyyy").ToUpper(), false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    //Si no existe el directorio se crea
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                    } // if (!Directory.Exists(ruta))
                    document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
                }

                urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;
            //}
            // fin archivo word

            var convenio = new Convenio()
            {
                ConvenioId = model.ConvenioId,
                ConceptoConvenioId = (int)ConceptosConvenioDomain.ConceptosConvenioDomainEnum.TomaNueva,
                TomaId = model.TomaId,
                EstatusConvenioId = (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo,
                PeriodoPagoConvenioId = model.PeriodoPagoConvenioId,
                PeriodoInicio = Convert.ToDateTime(model.PeriodoInicio),
                PeriodoFin = Convert.ToDateTime(model.PeriodoFin),
                FechaInicio = Convert.ToDateTime(model.FechaInicio),
                FechaFin = Convert.ToDateTime(model.FechaFin),
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                PersonaId = model.PersonaId,  
                Pagos = !string.IsNullOrEmpty(model.PagosDe) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PagosDe)) : (decimal?)null,
                PimerPago = !string.IsNullOrEmpty(model.PimerPago) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PimerPago)) : (decimal?)null,
                RutaArchivo = urlArchivo,
                NoTarjeta = model.NoTarjeta               
            };

            if (model.ConvenioId == 0)
            {
                conveniosDomain.Guardar(convenio);
            }
            else
            {
                if (!string.IsNullOrEmpty(model.RutaArchivo)) { 
                    FileInfo archivo = new FileInfo(model.RutaArchivo);

                    var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Convenios/"), archivo.Name);
                    if (System.IO.File.Exists(file))
                        System.IO.File.Delete(file);
                }

                conveniosDomain.Editar(convenio);
            }            

            if (!string.IsNullOrEmpty(model.PimerPago) && model.ConvenioId == 0)
            {
                var notificacionesDomain = new NotificacionesDomain(_context);
                PagosDomain pagosDomain = new PagosDomain(_context);

                var notificacion = notificacionesDomain.ObtenerNotificacion(model.TomaId);

                var pago = new Pago()
                {
                    ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio,
                    TomaId = model.TomaId,
                    SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PimerPago)),
                    Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.PimerPago)),
                    FechaAlta = DateTime.Now,
                    UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                    ConvenioId = convenio.ConvenioId,
                    NotificacionId = notificacion != null ? notificacion.NotificacionId : (int?)null,
                    NoRecibo = model.NoTarjeta,
                    Activo = true
                };
                pagosDomain.Guardar(pago);
            }
           
            return RedirectToAction("RegistroConvenioSuministroAgua",new { tomaId = model.TomaId });           
        }
        public ActionResult GuardarPeriodoPago(PeriodosPagoViewModel model)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);

            var periodoPago = new PeriodoPago()
            {
                TomaId = model.TomaId,
                MesAnoInicio = Convert.ToDateTime(model.MesAnoInicio),
                MesAnoFin = Convert.ToDateTime(model.MesAnoInicio),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                UltimoPeriodoPago = Convert.ToDateTime(model.MesAnoInicio).ToString("MMMMMMMMM yyyy").ToUpper(),
            };

            periodosPagoDomain.Guardar(periodoPago);

            return RedirectToAction("Tabs", "Tomas", new { propietarioId = model.PropietarioId });
        }
        public ActionResult GuardarServicio(ServiciosViewModel model)
        {
            var serviciosDomain = new ServiciosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            string urlArchivo = string.Empty;          

            //Obtiene tokens fontaneros
            var tokens = usuariosDomain.ObtenerTokenFontaneros();

            // generar archivo word           
            string rutaFormato = System.Web.HttpContext.Current.Server.MapPath("~/Print/Servicios.docx");
            string rutaArchivo = @"/UploadFiles/Servicios/";
            string nombreArchivo = DateTime.Now.ToString("_ddMMyyyyHHmmss") + ".docx";

            nombreArchivo = "Servicio" + nombreArchivo;

            using (DocX document = DocX.Load(rutaFormato))
            {
                document.ReplaceText("{Nombre}", model.NombreReporto, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Direccion}", model.Direccion, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Colonia}", model.Colonia, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Telefono}", !string.IsNullOrEmpty(model.Telefono) ? model.Telefono : string.Empty, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{UbicacionServicio}", model.UbicacionServicio, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Otro}", !string.IsNullOrEmpty(model.Otro) ? model.Otro : string.Empty, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                document.ReplaceText("{Observaciones}", !string.IsNullOrEmpty(model.Observaciones) ? model.Observaciones : string.Empty, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //Si no existe el directorio se crea
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
                } // if (!Directory.Exists(ruta))
                document.SaveAs(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo);
            }

            urlArchivo = System.Web.HttpContext.Current.Server.MapPath(rutaArchivo) + nombreArchivo;
            //}
            // fin archivo word

            var servicio = new Servicio()
            {
                AsuntoDescripcionId = Convert.ToInt32(model.AsuntoDescripcionId),
                TomaId = !string.IsNullOrEmpty(model.TomaId) ? Convert.ToInt32(model.TomaId) : (int?)null,
                Propietario = model.Propietario,
                Nombre = AdsertiFunciones.FormatearTexto(model.NombreReporto),
                Direccion = AdsertiFunciones.FormatearTexto(model.Direccion),
                Colonia = AdsertiFunciones.FormatearTexto(model.Colonia),
                Telefono = model.Telefono,
                UbicacionServicio = AdsertiFunciones.FormatearTexto(model.UbicacionServicio),
                Otro = !string.IsNullOrEmpty(model.Otro) ? AdsertiFunciones.FormatearTexto(model.Otro) : string.Empty,
                Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                FechaAlta = DateTime.Now,
                UrlArchivo = urlArchivo,
                EstatusServicioId = (int)EstatusServicioDomain.EstatusServicioEnum.NoIniciado
            };

            if (string.IsNullOrEmpty(model.ServicioId))
            {
                serviciosDomain.Guardar(servicio);
                foreach (var item in tokens)
                {
                    string mensaje = model.UbicacionServicio;
                    EnviarNotificacionServicios(item, mensaje);
                }              
            }
            else
            {
                if (!string.IsNullOrEmpty(model.RutaArchivo))
                {
                    FileInfo archivo = new FileInfo(model.RutaArchivo);

                    var file = Path.Combine(HttpContext.Server.MapPath("/UploadFiles/Servicios/"), archivo.Name);
                    if (System.IO.File.Exists(file))
                        System.IO.File.Delete(file);
                }
                servicio.ServicioId = Convert.ToInt32(model.ServicioId);
                servicio.UsuarioCambioId = Convert.ToInt32(Session["UsuarioId"].ToString());
                serviciosDomain.Editar(servicio);
            }
            return RedirectToAction("CargarServicio", new { servicioId = servicio.ServicioId });

        }
        [ValidateAntiForgeryToken]
        public ActionResult GuardarReconexionToma(ReconexionTomaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ReconexionToma", model);
            }

            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            //Guarda Pago
            var pago = new Pago()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.ReconexionToma,
                TomaId = model.TomaId,
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Costo)),
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Costo)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                Activo = true
            };
            pagosDomain.Guardar(pago);
            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
            var toma = tomasDomain.ObtenerToma(model.TomaId);
            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     "CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {4}{0}" +
                                                     "CAJERO(A): {5}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     DateTime.Now.ToString("dd/MM/yyyy"),
                                                     toma.Folio,
                                                     pago.Total.ToString("C"),
                                                     usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
            //Genera codigoQR
            var QQRurl = this.GenerarCodigoQR(informacionRecibo);

            var recibo = new Recibo()
            {
                PagoId = pago.PagoId,
                Fecha = DateTime.Now,
                CodigoQRurl = QQRurl,
                NoRecibo = noRecibo,
                //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                Adicional = "RECONEXIÓN DE TOMA",
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                RenglonAdicional1 = AdsertiFunciones.FormatearTexto(model.Observaciones),
                //RenglonAdicional2 = "Anterior propietario: " + cambioPropietario.Nombre + " " + cambioPropietario.ApellidoPaterno + " " + cambioPropietario.ApellidoMaterno,
                //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
            };
            recibosDomain.Gurdar(recibo);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);

            return null;
        }
        public ActionResult GuardarRecibosExtra(RecibosViewModel model)
        {
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            var pago = new Pago()
            {
                ConceptoPagoId = (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Otro,
                TomaId = model.TomaId,
                SubTotal = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.SubTotal)),
                Descuento = !string.IsNullOrEmpty(model.Descuento) ? Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Descuento)) : (decimal?)null,
                Total = Convert.ToDecimal(AdsertiFunciones.FormatearNumero(model.Total)),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                Activo = true
            };
            pagosDomain.Guardar(pago);

            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
            var toma = tomasDomain.ObtenerToma(model.TomaId);

            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     "CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {4}{0}" +
                                                     "CAJERO(A): {5}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     DateTime.Now.ToString("dd/MM/yyyy"),
                                                     toma.Folio,
                                                     pago.Total.ToString("C"),
                                                     usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
            //Genera codigoQR
            var QQRurl = this.GenerarCodigoQR(informacionRecibo);

            var recibo = new Recibo()
            {
                PagoId = pago.PagoId,
                Fecha = DateTime.Now,
                CodigoQRurl = QQRurl,
                NoRecibo = noRecibo,
                Adicional = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                Concepto = AdsertiFunciones.FormatearTexto(model.Concepto)
            };

            recibosDomain.Gurdar(recibo);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);

            return View();
        }
        public ActionResult ImprimirCorte(string fecha, string downloadToken)
        {
            DateTime fecha1 = Convert.ToDateTime(fecha);
            var corte = this.ObtenerCorteViewModel(fecha1);

            Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
            return new ViewAsPdf(corte)
            {
                FileName = string.Format("Corte_" + fecha1.ToString("dd-MM-yyyy") + ".pdf")                
            };           
        }
        public ActionResult ImprimirEstadoCuenta(int tomaId, string downloadToken)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var tarifasDomain = new TarifasDomain(_context);                          
            List<Tarifa> tarifas = new List<Tarifa>();

            var estadoCuenta = periodosPagoDomain.ObtenerEstadoCuenta(tomaId);
            int categoriaId = estadoCuenta.Select(e => e.Toma.Categoria.CategoriaId).FirstOrDefault();

            var ultimoPeriodo = periodosPagoDomain.ObtenerUltimoPeriodoPago(tomaId);
            if (ultimoPeriodo != null)
            {
                if (ultimoPeriodo.MesAnoFin != null)
                {
                    var mesAnoFin = Convert.ToDateTime(ultimoPeriodo.MesAnoFin);
                    var diferencia = 12 - mesAnoFin.Month;
                    if (diferencia > 0)
                    {
                        var tarifa = tarifasDomain.ObtenerTarifa(categoriaId, mesAnoFin.Year);

                        // valida que exista tarifa para ese año
                        if (tarifa == null)
                        {
                            ShowToastMessage("Error", "No existe tarifa para ese año, revisa las tarifas para esa categoría.", ToastMessage.ToastType.Error);
                            return RedirectToAction("Index","Home");
                        }

                        var costoPorMes = tarifa.CostoTarifa / 12;
                        var totalRestoMes = costoPorMes * diferencia;
                        foreach (var item in estadoCuenta.Select(e => e.Toma.Categoria.Tarifa.Where(t => t.MesAno >= mesAnoFin.Year && t.MesAno <= DateTime.Now.Year)).FirstOrDefault())
                        {
                            if (item.MesAno == mesAnoFin.Year)
                            {
                                item.CostoTarifa = totalRestoMes;
                            }
                            tarifas.Add(item);
                        }
                    }
                    else
                    {
                        foreach (var item in estadoCuenta.Select(e => e.Toma.Categoria.Tarifa.Where(t => t.MesAno > mesAnoFin.Year && t.MesAno <= DateTime.Now.Year)).FirstOrDefault())
                        {                            
                            tarifas.Add(item);
                        }
                    }                  
                }
            }               

            var estadoCuentaVM = new EstadoCuentaViewModel()
            {
                Categoria = estadoCuenta.Select(e => e.Toma.Categoria.Nombre).FirstOrDefault(),
                NombrePropietario = estadoCuenta.Select(e => e.Toma.Propietario.Persona.TipoPersonaId).FirstOrDefault() == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? estadoCuenta.Select(e => e.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                             e.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                             e.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno).FirstOrDefault() : estadoCuenta.Select(e => e.Toma.Propietario.Persona.PersonaMoral.Nombre).FirstOrDefault(),
                Direccion = (estadoCuenta.Select(t => t.Toma.Direccion.TipoCalleId).FirstOrDefault() > 0 ? estadoCuenta.Select(t => t.Toma.Direccion.TiposCalle.Nombre).FirstOrDefault() : string.Empty) + " " + (estadoCuenta.Select(t => t.Toma.Direccion.CalleId).FirstOrDefault() > 0 ? estadoCuenta.Select(t => t.Toma.Direccion.Calles.Nombre).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(estadoCuenta.Select(t => t.Toma.Direccion.NumInt).FirstOrDefault()) ? " Int." + estadoCuenta.Select(t => t.Toma.Direccion.NumInt).FirstOrDefault() : string.Empty) +
                                        (!string.IsNullOrEmpty(estadoCuenta.Select(t => t.Toma.Direccion.NumExt).FirstOrDefault()) ? " Ext." + estadoCuenta.Select(t => t.Toma.Direccion.NumExt).FirstOrDefault() : string.Empty) + ", " +
                                        (estadoCuenta.Select(t => t.Toma.Direccion.ColoniaId).FirstOrDefault() > 0 ? estadoCuenta.Select(t => t.Toma.Direccion.Colonias.Nombre).FirstOrDefault() : string.Empty),               
                Tarifas = tarifas,
                Folio = estadoCuenta.Select(t => t.Toma.Folio).FirstOrDefault().ToString()
            };
            Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
            return new ViewAsPdf(estadoCuentaVM)
            {            
                FileName = string.Format("Estado Cuenta_" + estadoCuenta.Select(e => e.Toma.Folio).FirstOrDefault() + ".pdf")
            };
        }
        public ActionResult ImprimirConvenio(ConveniosViewModel model)
        {            

            if (!string.IsNullOrEmpty(model.RutaArchivo))
            {
                FileInfo archivo = new FileInfo(model.RutaArchivo);

                if (archivo.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                    Response.AddHeader("Content-Length", archivo.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.TransmitFile(archivo.FullName);
                    Response.End();
                }
            }
            return null;
        }
        public ActionResult EliminarConvenio(int convenioId)
        {
            var conveniosDomain = new ConveniosDomain(_context);

            conveniosDomain.EliminarConvenio(convenioId, Convert.ToInt32(Session["UsuarioId"].ToString()));

            return RedirectToAction("Index", "Home");
        }
        public ActionResult ImprimirServicio(ServiciosViewModel model)
        {

            if (!string.IsNullOrEmpty(model.RutaArchivo))
            {
                FileInfo archivo = new FileInfo(model.RutaArchivo);

                if (archivo.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
                    Response.AddHeader("Content-Length", archivo.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.TransmitFile(archivo.FullName);
                    Response.End();
                }
            }
            return null;
        }
        public ActionResult DescargarPadron(string downloadToken)
        {
            TomasDomain tomasDomain = new TomasDomain(_context);
            PadronExcel padronExcel = new PadronExcel();
            List<PadronExcel> padronExcelList = new List<PadronExcel>();
            decimal? newtotal = null;

            List<Toma> tomas = tomasDomain.ObtenerTomas();

            foreach (var item in tomas)
            {
                newtotal = null;
                if (item.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault() != null)
                {
                    var tarifasDomain = new TarifasDomain(_context);

                    int mes = 12 - DateTime.Now.Month;

                    var periodoInicio = Convert.ToDateTime(item.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).AddMonths(1);
                    var periodoFin = DateTime.Now.AddMonths(mes);

                    var tarifas = tarifasDomain.ObtenerTarifasPeriodo(periodoInicio, periodoFin, item.CategoriaId);

                    decimal total = 0;

                    while (periodoInicio <= periodoFin)
                    {
                        var costoTarifa = tarifas.Where(t => t.MesAno == periodoInicio.Year).FirstOrDefault();
                        if (costoTarifa != null)
                        {
                            var mensualidad = (costoTarifa.CostoTarifa / 12);

                            total += mensualidad;
                        }
                        periodoInicio = periodoInicio.AddMonths(1);
                    }

                    newtotal = Math.Round(total);

                }

                padronExcel = new PadronExcel()
                {
                    Folio = item.Folio,
                    Propietario = item.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? item.Propietario.Persona.PersonaFisica.Nombre + " " +
                                  item.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                  item.Propietario.Persona.PersonaFisica.ApellidoMaterno : item.Propietario.Persona.PersonaMoral.Nombre,
                    Calle = item.Direccion != null ? (item.Direccion.TipoCalleId > 0 ? item.Direccion.TiposCalle.Nombre : string.Empty) + " " + (item.Direccion.CalleId > 0 ? item.Direccion.Calles.Nombre : string.Empty) : string.Empty,
                    NumInt = item.Direccion != null ? item.Direccion.NumInt : string.Empty,
                    NumExt = item.Direccion != null ? item.Direccion.NumExt : string.Empty,
                    Colonia = item.Direccion != null ? (item.Direccion.ColoniaId > 0 ? item.Direccion.Colonias.Nombre : string.Empty) : string.Empty,

                    Tarifa = item.Categoria.Nombre,
                    Periodo = !string.IsNullOrEmpty(item.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault().ToString()) ? Convert.ToDateTime(item.PeriodoPago.Select(p => p.MesAnoFin).LastOrDefault()).ToString("MMM-yyyy") : item.PeriodoPago.Select(p => p.UltimoPeriodoPago).LastOrDefault(),
                    Adeudo = newtotal != null ? Convert.ToDecimal(newtotal).ToString("C") : string.Empty
                };

                if (item.Convenio.Count > 0)
                {
                    if (item.Convenio.Select(c => c.EstatusConvenioId).LastOrDefault() == (int)EstatusConvenioDomain.EstatusConvenioEnum.Activo)
                    {
                        PagosDomain pagosDomain = new PagosDomain(_context);
                        var pagos = pagosDomain.ObtenerPagosConvenio(item.Convenio.Select(c => c.ConvenioId).LastOrDefault());

                        padronExcel.Convenio = item.Convenio.Select(c => c.ConceptoConvenio.Nombre).LastOrDefault();
                        padronExcel.FechaInicio = item.Convenio.Select(c => c.FechaInicio).LastOrDefault().ToString("dd-MM-yyyy");
                        padronExcel.FechaFin = item.Convenio.Select(c => c.FechaFin).LastOrDefault().ToString("dd-MM-yyyy");
                        padronExcel.Total = item.Convenio.Select(c => c.Total).LastOrDefault().ToString("C");
                        padronExcel.PorPagar = (item.Convenio.Select(c => c.Total).LastOrDefault() - pagos.Sum(p => p.Total)).ToString("C");
                    }
                }

                padronExcelList.Add(padronExcel);
            }
            
            padronExcelList = padronExcelList.Where(t => t.Folio > 0).OrderBy(t => t.Folio).ToList();                     

            DataTable dt = new DataTable("Tomas");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("NÚMERO"),
                                            new DataColumn("NOMBRE"),
                                            new DataColumn("CALLE"),
                                            new DataColumn("#INT"),
                                            new DataColumn("#EXT"),
                                            new DataColumn("COLONIA"),
                                            new DataColumn("TARIFA"),
                                            new DataColumn("PERIODO"),
                                            new DataColumn("ADEUDO"),
                                            new DataColumn("CONVENIO"),
                                            new DataColumn("FECHA INICIO"),
                                            new DataColumn("FECHA FIN"),
                                            new DataColumn("TOTAL"),
                                            new DataColumn("POR PAGAR")});            

            foreach (var item in padronExcelList)
            {
                dt.Rows.Add(item.Folio,
                    item.Propietario,
                    item.Calle,
                    item.NumInt,
                    item.NumExt,
                    item.Colonia,
                    item.Tarifa,
                    item.Periodo,
                    item.Adeudo,
                    item.Convenio,
                    item.FechaInicio,
                    item.FechaFin,
                    item.Total,
                    item.PorPagar);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);               
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Padron.xlsx");
                }
            }
        }
        public ActionResult RegistroServicio(int? tomaId)
        {
            var asuntosDomain = new AsuntosDomain(_context);
            var tomasDomain = new TomasDomain(_context);
            var serviciosViewModel = new ServiciosViewModel();
            
            if (tomaId != null)
            {
                var toma = tomasDomain.ObtenerToma(Convert.ToInt32(tomaId));
                serviciosViewModel = new ServiciosViewModel()
                {
                    Asuntos = asuntosDomain.ObtenerAsuntos(),
                    AsuntosDescripcion = new List<AsuntoDescripcion>(),
                    Propietario = true,
                    TomaId = tomaId.ToString(),
                    NombreReporto = toma.Propietario.Persona.PersonaFisica.Nombre + " " + toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                    toma.Propietario.Persona.PersonaFisica.ApellidoMaterno,
                    Direccion = toma.Direccion.Calle + (!string.IsNullOrEmpty(toma.Direccion.NumInt) ? " INT." + toma.Direccion.NumInt : string.Empty)
                                + (!string.IsNullOrEmpty(toma.Direccion.NumExt) ? " EXT." + toma.Direccion.NumExt : string.Empty),
                    Colonia = toma.Direccion.Colonia,
                    Telefono = toma.Propietario.Persona.PersonaFisica.Telefono,
                    UbicacionServicio = toma.Direccion.Calle + (!string.IsNullOrEmpty(toma.Direccion.NumInt) ? " INT." + toma.Direccion.NumInt : string.Empty)
                                + (!string.IsNullOrEmpty(toma.Direccion.NumExt) ? " EXT." + toma.Direccion.NumExt : string.Empty),
                    Imprimir = false
                };
            }
            serviciosViewModel.Asuntos = asuntosDomain.ObtenerAsuntos();
            return View(serviciosViewModel);
        }
        public JsonResult CargarDescripcionAsunto(int asuntoId)
        {
            var asuntosDescripcionDomain = new AsuntosDescripcionDomain(_context);

            var asuntosDescripcion = asuntosDescripcionDomain.ObtenerAsuntosDescripcion(asuntoId);

            return Json(new { result = asuntosDescripcion }, JsonRequestBehavior.AllowGet);
        }       
        public ActionResult CargarServicio(int servicioId)
        {
            var serviciosDomain = new ServiciosDomain(_context);
            var asuntosDomain = new AsuntosDomain(_context);
            var asuntosDescripcionDomain = new AsuntosDescripcionDomain(_context);

            var servicio = serviciosDomain.ObtenerServicio(servicioId);

            var serviciosViewModel = new ServiciosViewModel()
            {
                ServicioId = servicioId.ToString(),
                Asuntos = asuntosDomain.ObtenerAsuntos(),
                AsuntosDescripcion = asuntosDescripcionDomain.ObtenerAsuntosDescripcion(servicio.AsuntoDescripcion.Asunto.AsuntoId),
                NombreReporto = servicio.Nombre,
                Direccion = servicio.Direccion,
                Colonia = servicio.Colonia,
                Telefono = servicio.Telefono,
                UbicacionServicio = servicio.UbicacionServicio,
                AsuntoId = servicio.AsuntoDescripcion.Asunto.AsuntoId.ToString(),
                AsuntoDescripcionId = servicio.AsuntoDescripcionId.ToString(),
                Otro = servicio.Otro,
                Observaciones = servicio.Observaciones,
                Imprimir = true,
                RutaArchivo = servicio.UrlArchivo
            };

            return View("RegistroServicio", serviciosViewModel);

        }
        public ActionResult ConsultarServicios()
        {
            try
            {

                var serviciosDomain = new ServiciosDomain(_context);
                var serviciosViewModel = new ServiciosViewModel();
                var evidenciaServiciosVM = new EvidenciaServiciosViewModel();
                var evidenciaServiciosVMList = new List<EvidenciaServiciosViewModel>();

                var servicios = serviciosDomain.ObtenerServicios();

                var noIniciado = servicios.Where(s => s.EstatusServicioId == (int)EstatusServicioDomain.EstatusServicioEnum.NoIniciado).OrderByDescending(s => s.FechaAlta).ToList();
                var enProceso = servicios.Where(s => s.EstatusServicioId == (int)EstatusServicioDomain.EstatusServicioEnum.EnProceso).OrderByDescending(s => s.FechaCambio).ToList();
                var concluido = servicios.Where(s => s.EstatusServicioId == (int)EstatusServicioDomain.EstatusServicioEnum.Concluido).OrderByDescending(s => s.FechaCambio).ToList();

                foreach (var item in servicios)
                {
                    evidenciaServiciosVM = new EvidenciaServiciosViewModel()
                    {
                        EvidenciaServicioId = item.EvidenciaServicio.Select(e => e.EvidenciaServicioId).FirstOrDefault(),
                        ServicioId = item.EvidenciaServicio.Select(e => e.ServicioId).FirstOrDefault(),
                        UrlImagen = item.EvidenciaServicio.Select(e => e.UrlImagen).FirstOrDefault(),
                        Observaciones = item.EvidenciaServicio.Select(e => e.Observaciones).FirstOrDefault()
                    };
                    evidenciaServiciosVMList.Add(evidenciaServiciosVM);
                }

                serviciosViewModel.NoIniciado = noIniciado;
                serviciosViewModel.EnProceso = enProceso;
                serviciosViewModel.Concluido = concluido;
                serviciosViewModel.EvidenciaServicios = evidenciaServiciosVMList;

                return View(serviciosViewModel);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Recibos(int? reciboId = null)
        {
            var reciboVM = this.ObtenerRecibos(null, null, DateTime.Now, reciboId);
            return View(reciboVM);
        }       
        public ActionResult BuscarRecibos(RecibosViewModel modelo)
        {
            var reciboVM = new RecibosViewModel();
            if (modelo.NoReciboFiltro != null ||
                modelo.FolioFiltro != null ||
                modelo.FechaFiltro != null)
            {
                reciboVM = this.ObtenerRecibos(modelo.NoReciboFiltro, modelo.FolioFiltro, modelo.FechaFiltro);
            }
            else
            {
                reciboVM = this.ObtenerRecibos(null, null, DateTime.Now);
            }

            return View("Recibos", reciboVM);
            
        }        
        public ActionResult CancelarRecibo(int reciboId, int tomaId)
        {
            var recibosDomain = new RecibosDomain(_context);
            int usuarioId = Convert.ToInt32(Session["UsuarioId"].ToString());

            var pagos = recibosDomain.ObtenerRecibos(tomaId);
            var pago = pagos.Where(p => p.Recibo.Select(r => r.ReciboId == reciboId).FirstOrDefault()).FirstOrDefault();

            if (pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.TomaNueva)
            {
                if (pagos.Count > 1)
                {
                    ShowToastMessage("Alerta", "La toma ya tiene pagos registrados, por lo tanto no se puede cancelar", ToastMessage.ToastType.Warning);
                }
                else
                {
                    recibosDomain.CancelarReciboTomaNueva(reciboId, usuarioId);
                    ShowToastMessage("SAAP", "Recibo cancelado exitosamente", ToastMessage.ToastType.Success);
                }
            }else if(pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua)
            {
                var pagosConsecuentes = pagos.Where(p => (p.PagoId > pago.PagoId) && (p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua ||
                                                                                        p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio)).ToList();
                if (pagosConsecuentes.Count > 0)
                {
                    ShowToastMessage("Alerta", "La toma ya tiene pagos por suministro de agua posteriores, por lo tanto no se puede cancelar", ToastMessage.ToastType.Warning);
                }
                else
                {
                    recibosDomain.CancelarReciboSuministroAgua(reciboId, usuarioId);
                    ShowToastMessage("SAAP", "Recibo cancelado exitosamente", ToastMessage.ToastType.Success);
                }                
            }else if(pago.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio)
            {
                var pagosConsecuentes = pagos.Where(p => (p.PagoId > pago.PagoId) && (p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio ||
                                                                                        p.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.SuministroAgua)).ToList();
                if (pagosConsecuentes.Count > 0)
                {
                    ShowToastMessage("Alerta", "La toma ya tiene pagos por convenio posteriores, por lo tanto no se puede cancelar", ToastMessage.ToastType.Warning);
                }
                else
                {
                    recibosDomain.CancelarReciboPagoConvenio(reciboId, usuarioId);
                    ShowToastMessage("SAAP", "Recibo cancelado exitosamente", ToastMessage.ToastType.Success);
                }
            }
            else
            {
                recibosDomain.CancelarRecibo(reciboId, usuarioId);
                ShowToastMessage("SAAP", "Recibo cancelado exitosamente", ToastMessage.ToastType.Success);
            }
            var recibo = recibosDomain.ObtenerRecibo(reciboId);
            var recibosListVM = this.ObtenerRecibos(null, null, DateTime.Now, null);
            return View("Recibos", recibosListVM);
        }       
        public ActionResult Limpiar()
        {
            var reciboVM = new RecibosViewModel(){
                Recibos = new List<Recibo>()
            };
            return View("Recibos", reciboVM);
        }
        public ActionResult ImprimirReciboConvenio(int tomaId, int convenioId)
        {
            var conveniosDomain = new ConveniosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);
            var tomasDomain = new TomasDomain(_context);

            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));
            var toma = tomasDomain.ObtenerToma(tomaId);
            var convenio = conveniosDomain.ObtenerPagosConvenio(convenioId);


            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     "CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {4}{0}" +
                                                     "CAJERO(A): {5}{0}",
                Environment.NewLine, noRecibo,
                DateTime.Now.ToString("dd/MM/yyyy"),
                toma.Folio,
                convenio.Sum(c => c.Total).ToString("C"),
                //pago.Total.ToString("C"),
                usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
            //Genera codigoQR
            var QQRurl = this.GenerarCodigoQR(informacionRecibo);

            var recibo = new Recibo()
            {
                //PagoId = pago.PagoId,
                ConvenioId = convenioId,
                Fecha = DateTime.Now,
                CodigoQRurl = QQRurl,
                NoRecibo = noRecibo,
                //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                //Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                CantidadLetra = this.Convertir(convenio.Sum(c => c.Total).ToString()),
                FechaAlta = DateTime.Now,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString()),
                //RenglonAdicional1 = !string.IsNullOrEmpty(model.RenglonAdicional1) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional1) : string.Empty,
                //RenglonAdicional2 = !string.IsNullOrEmpty(model.RenglonAdicional2) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional2) : string.Empty,
                //RenglonAdicional3 = !string.IsNullOrEmpty(model.RenglonAdicional3) ? AdsertiFunciones.FormatearTexto(model.RenglonAdicional3) : string.Empty,
            };

            recibosDomain.Gurdar(recibo);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId + "&convenioId=" + convenioId);

            return View();
        }
        public ActionResult Convenios()
        {
            var conveniosDomain = new ConveniosDomain(_context);
            var conveniosAdministracionVM = new ConveniosAdministracionViewModel();

            var convenios = conveniosDomain.ObtenerConvenios();

            conveniosAdministracionVM.Convenios = convenios.ToList();

            return View(conveniosAdministracionVM);
        }
        public ActionResult GenerarReciboPagadoConvenio(int pagoId)
        {
            var pagosDomain = new PagosDomain(_context);
            var recibosDomain = new RecibosDomain(_context);
            var usuariosDomain = new UsuariosDomain(_context);

            var pago = pagosDomain.ObtenerPago(pagoId);

            //Guarda recibo
            var noRecibo = recibosDomain.ObtenerNoRecibo() + 1;
            var usuarioPersona = usuariosDomain.ObtenerUsuarioPersona(Convert.ToInt32(Session["UsuarioId"].ToString()));

            string informacionRecibo = string.Format("RECIBO DE PAGO NO: {1}{0}" +
                                                     "FECHA: {2}{0}" +
                                                     "CLAVE DE CONTROL: {3}{0}" +
                                                     "CANTIDAD PAGADA: {4}{0}" +                                                   
                                                     "CAJERO(A): {5}{0}",
                                                     Environment.NewLine, noRecibo,
                                                     pago.FechaAlta.ToString("dd/MM/yyyy"),
                                                     pago.Toma.Folio,
                                                     pago.Total.ToString("C"),
                                                     usuarioPersona.Persona.Nombre + " " + usuarioPersona.Persona.ApellidoPaterno + " " + usuarioPersona.Persona.ApellidoMaterno);
            //Genera codigoQR
            var QQRurl = this.GenerarCodigoQR(informacionRecibo);

            var recibo = new Recibo()
            {
                PagoId = pago.PagoId,
                Fecha = pago.FechaAlta,
                CodigoQRurl = QQRurl,
                NoRecibo = noRecibo,
                //Observaciones = !string.IsNullOrEmpty(model.Observaciones) ? AdsertiFunciones.FormatearTexto(model.Observaciones) : string.Empty,
                //Adicional = !string.IsNullOrEmpty(model.Adicional) ? AdsertiFunciones.FormatearTexto(model.Adicional) : string.Empty,
                CantidadLetra = this.Convertir(pago.Total.ToString()),
                FechaAlta = pago.FechaAlta,
                UsuarioAltaId = Convert.ToInt32(Session["UsuarioId"].ToString())
            };

            recibosDomain.Gurdar(recibo);

            Response.Redirect("~/Print/ReciboSistema.aspx?reciboId=" + recibo.ReciboId);

            return HttpNotFound();
        }
        public ActionResult ReconexionToma(int tomaId)
        {
            var reconexionTomaViewModel = new ReconexionTomaViewModel()
            {
                TomaId = tomaId,
                Accion = Accion.Agregar
            };
            return View(reconexionTomaViewModel);
        }
        public JsonResult ValidarFolio(int tomaId, int folio, string downloadToken)
        {
            var tomasDomain = new TomasDomain(_context);
            var existeFolio = tomasDomain.ValidarTomaExistente(folio, tomaId);
            Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
            return Json(new { existe = existeFolio }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidarRfcPersonaFisica(string rfc)
        {
            bool rfcValido = AdsertiValidaciones.EsRfcPersonaFisica(rfc);
            return Json(new { valido = rfcValido }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidarRfcPersonaMoral(string rfc)
        {
            bool rfcValido = AdsertiValidaciones.EsRfcPersonaMoral(rfc);
            return Json(new { valido = rfcValido }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RecibosExtraFiltros()
        {
            var recibosViewModel = new RecibosViewModel()
            {
                Accion = ComiteAgua.Accion.Consultar,
            };

            return View(recibosViewModel);
        }
        public ActionResult _RecibosExtraConsulta(int? folio, string propietario, string calle)
        {
            ConstanciasDomain constanciasDomain = new ConstanciasDomain(_context);
            var tomasPreList = constanciasDomain.ObtenerTomas(folio, propietario, calle);

            var tomas = tomasPreList
                .Select(t => new TomasViewModel
                {
                    TomaId = t.TomaId,
                    Folio = t.Folio,
                    Propietario = t.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? t.Propietario.Persona.PersonaFisica.Nombre + " " +
                                  t.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                  t.Propietario.Persona.PersonaFisica.ApellidoMaterno : t.Propietario.Persona.PersonaMoral.Nombre,
                    Calle = t.Direccion != null ? ((t.Direccion.TipoCalleId > 0 ? t.Direccion.TiposCalle.Nombre : string.Empty) + ' ' + (t.Direccion.CalleId > 0 ? t.Direccion.Calles.Nombre : string.Empty) +
                                    (!string.IsNullOrEmpty(t.Direccion.NumInt) ? " INT " + t.Direccion.NumInt : string.Empty) +
                                    (!string.IsNullOrEmpty(t.Direccion.NumExt) ? " EXT " + t.Direccion.NumExt : string.Empty)) : String.Empty,
                    Colonia = t.Direccion != null ? (t.Direccion.ColoniaId > 0 ? t.Direccion.Colonias.Nombre : string.Empty) : string.Empty,
                    UltimoPeriodoPago = t.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault() != null ?
                                        Convert.ToDateTime(t.PeriodoPago.Select(p => p.MesAnoInicio).LastOrDefault()).ToString("MMM-yyyy", new CultureInfo("es-ES")) : t.PeriodoPago.Select(p => p.UltimoPeriodoPago).LastOrDefault(),
                    ReciboImpreso = t.Constancia.Count > 0 ? t.Constancia.Select(c => c.ReciboImpreso).LastOrDefault() : true
                })
                .OrderBy(t => t.Folio)
                .ToList();

            return PartialView(tomas);
        }
        public ActionResult RecibosExtraAdministracion(int tomaId)
        {
            var recibosViewModel = new RecibosViewModel()
            {
                Accion = Accion.Agregar,
            };
            return View("RecibosExtraAdministracion", recibosViewModel);
        }
        
        #endregion

        #region * Métodos creados por Comité Agua *

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        } // protected override void Dispose(bool disposing)               
        public string ConvertirFecha(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }//public string ConvertirFecha(string num)
        public string EnviarNotificacionServicios(string deviceId, string message)
        {
            string SERVER_API_KEY = "AIzaSyBNwF00EO9NtjQi5s42JoEk0Hs3jYPS10I";
            var SENDER_ID = "comiteaguaapp";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            var data = new
            {
                to = deviceId,
                notification = new
                {
                    body = message,
                    title = "Comité de Agua",
                    icon = "myicon",
                    sound = "mysound"
                }
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);

            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();


            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
        public string GenerarCodigoQR(string informacionPago)
        {            

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(informacionPago);
            System.Drawing.Image QR = (System.Drawing.Image)img;

            MemoryStream ms = new MemoryStream();

            QR.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = ms.ToArray();
            //CodigoImg.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(imageBytes);
            //CodigoImg.Height = 50;
            //CodigoImg.Width = 50;

            string rutaArchivo = @"/UploadFiles/CodigosQR/";            
            string nombreArchivo = DateTime.Now.ToString("_ddMMyyyyHHmmss") + ".png";

            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rutaArchivo));
            } // if (!Directory.Exists(ruta))

            img.Save(Server.MapPath(rutaArchivo) + nombreArchivo, ImageFormat.Png);           
           

            string codigoQRurl = nombreArchivo;

            return codigoQRurl;
        }
        public CorteViewModel ObtenerCorteViewModel(DateTime fecha)
        {
            GastosDomain gastosDomain = new GastosDomain(_context);
            PagosDomain pagosDomain = new PagosDomain(_context);
            List<PagosViewModel> listaPagos = new List<PagosViewModel>();
            List<GastosViewModel> listaGastos = new List<GastosViewModel>();

            var gastos = gastosDomain.ObtenerGastos(fecha);
            var ingresos = pagosDomain.ObtenerPagos(fecha);

            foreach (var item in gastos)
            {
                GastosViewModel gastosViewModel = new GastosViewModel()
                {                   
                    GastoId = item.GastoId,
                    Concepto = item.Concepto,
                    Total = item.Total.ToString("C"),
                };

                listaGastos.Add(gastosViewModel);
            }

            foreach (var item in ingresos)
            {
                PagosViewModel pagosViewModel = new PagosViewModel()
                {                 
                    NoRecibo = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count == 0 ? (!string.IsNullOrEmpty(item.Convenio.NoTarjeta) ? Convert.ToInt32(item.Convenio.NoTarjeta) : (int?)null) :
                                item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count > 0 ? item.Recibo.Select(r => r.NoRecibo).FirstOrDefault() : item.Recibo.Select(r => r.NoRecibo).FirstOrDefault(),
                    PagoId = item.PagoId,
                    ConceptoPago = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Renta ? 
                                                            item.Renta.TipoRenta.Nombre : 
                                                           item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Constancia ? 
                                                            item.Constancia.TiposConstancia.Nombre :
                                                           item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Otro ?
                                                           item.Recibo.Select(r => r.Concepto).FirstOrDefault():
                                                            item.ConceptoPago.Nombre,
                    Total = item.Total.ToString("C"),
                    TipoPago = item.ConceptoPagoId == (int)ConceptosPagoDomain.ConceptosPagoDomainEnum.Convenio && item.Recibo.Count == 0 ? "Tarjeta" : "Recibo",
                    Activo = item.Activo
                };

                listaPagos.Add(pagosViewModel);
            }

            //var totalIngresos = ingresos.Sum(x => x.Total);
            var totalIngresos = ingresos.Where(x => x.Activo).Sum(x => x.Total);
            var totalGastos = gastos.Sum(x => x.Total);
            var total = totalIngresos - totalGastos;

            CorteViewModel corteViewModel = new CorteViewModel()
            {
                Gastos = listaGastos,
                Pagos = listaPagos,
                TotalIngresos = totalIngresos.ToString("C"),
                TotalGastos = totalGastos.ToString("C"),
                Total = total.ToString("C"),
                FechaConsulta = fecha
            };

            return corteViewModel;
        }
        protected string Convertir(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res + " PESOS 00/100 M.N";
        }
        protected string ConvertirMinusculas(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " con " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res + " pesos 00/100";
        }
        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UN";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
        public RecibosViewModel ObtenerRecibos(int? noRecibo, int? folio, DateTime? fecha, int? reciboId = null)
        {
            var recibosDomain = new RecibosDomain(_context);
            var recibos = recibosDomain.ObtenerRecibos(noRecibo, folio, fecha, reciboId).ToList();

            var reciboVM = new RecibosViewModel()
            {
                NoReciboFiltro = noRecibo,
                FolioFiltro = folio,
                FechaFiltro = fecha,
                ReciboId = reciboId,
                Recibos = recibos,
                UrlOrigen = Url.Action("Recibos", "Tomas")                
            };

            return reciboVM;
        }

        #endregion

    } // public class TomasController : Controller

} // namespace ComiteAgua.Controllers.Tomas

public class Archivo
{
    public int ArchivoId { get; set; }
    public string Nombre { get; set; }
    public string UrlArchivo { get; set; }
    public int PersonaId { get; set; }
    public int TipoArchivoId { get; set; }
}

public class ArchivoClass
{
    public HttpPostedFileBase File { get; set; }
    public string FechaHora { get; set; }
    public string nombre { get; set; }
}

public class PadronExcel
{
    [XmlAttribute("NÚMERO")]
    public int Folio { get; set; }
    [XmlAttribute("NOMBRE")]
    public string Propietario { get; set; }
    [XmlAttribute("CALLE")]
    public string Calle { get; set; }
    [XmlAttribute("#INT")]
    public string NumInt { get; set; }
    [XmlAttribute("#EXT")]
    public string NumExt { get; set; }
    [XmlAttribute("COLONIA")]
    public string Colonia { get; set; }    
    [XmlAttribute("TARIFA")]
    public string Tarifa { get; set; }
    [XmlAttribute("PERIODO")]
    public string Periodo { get; set; }
    [XmlAttribute("ADEUDO")]
    public string Adeudo { get; set; }

    // Convenio
    [XmlAttribute("CONVENIO")]
    public string Convenio { get; set; }
    [XmlAttribute("FECHA INICIO")]
    public string FechaInicio {get;set;}
    [XmlAttribute("FECHA FIN")]
    public string FechaFin { get; set; }
    [XmlAttribute("TOTAL")]
    public string Total { get; set; }
    [XmlAttribute("POR PAGAR")]
    public string PorPagar { get; set; }

}


