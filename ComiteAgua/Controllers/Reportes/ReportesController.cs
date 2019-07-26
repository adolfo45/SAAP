using AdsertiVS2017ClassLibrary;
using ComiteAgua.Domain;
using ComiteAgua.Filters.Security;
using ComiteAgua.Models;
using ComiteAgua.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.Controllers
{
    [Authenticate]
    public class ReportesController : MessageControllerBase
    {
        #region * Constructor generado por Comité Agua *
        public ReportesController()
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
        public ActionResult _ConsultarDeudores(DateTime? periodoInicio, DateTime? periodoFin, int? calleId)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var deudores = new List<DeudoresViewModel>();
            decimal? newtotal = null;

            var periodoPagoList = periodosPagoDomain.ObtenerDeudores(periodoInicio, periodoFin, calleId);

            foreach (var t in periodoPagoList)
            {
                newtotal = null;
                if (t.MesAnoFin != null)
                {
                    var tarifasDomain = new TarifasDomain(_context);

                    int mes = 12 - DateTime.Now.Month;

                    var periodoInicioVar = Convert.ToDateTime(t.MesAnoFin).AddMonths(1);
                    var periodoFinVar = DateTime.Now.AddMonths(mes);

                    var tarifas = tarifasDomain.ObtenerTarifasPeriodo(periodoInicioVar, periodoFinVar, t.Toma.CategoriaId);

                    decimal total = 0;

                    while (periodoInicioVar <= periodoFinVar)
                    {
                        var costoTarifa = tarifas.Where(ta => ta.MesAno == periodoInicioVar.Year).FirstOrDefault();
                        if (costoTarifa != null)
                        {
                            var mensualidad = (costoTarifa.CostoTarifa / 12);

                            total += mensualidad;
                        }
                        periodoInicioVar = periodoInicioVar.AddMonths(1);
                    }
                    newtotal = Math.Round(total);
                }

                var deudor = new DeudoresViewModel()
                {
                    Categoria = t.Toma.Categoria.Abreviatura,
                    NombrePropietario = t.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? t.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                                         t.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                                         t.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : t.Toma.Propietario.Persona.PersonaMoral.Nombre,
                    Direccion = (t.Toma.Direccion.TipoCalleId > 0 ? t.Toma.Direccion.TiposCalle.Nombre : string.Empty) + " " + (t.Toma.Direccion.CalleId > 0 ? t.Toma.Direccion.Calles.Nombre : string.Empty) +
                                                (!string.IsNullOrEmpty(t.Toma.Direccion.NumInt) ? " Int." + t.Toma.Direccion.NumInt : string.Empty) +
                                                (!string.IsNullOrEmpty(t.Toma.Direccion.NumExt) ? " Ext." + t.Toma.Direccion.NumExt : string.Empty) + ", " +
                                                (t.Toma.Direccion.ColoniaId > 0 ? t.Toma.Direccion.Colonias.Nombre : string.Empty),
                    Folio = t.Toma.Folio.ToString(),
                    Periodo = t.MesAnoFin != null ? Convert.ToDateTime(t.MesAnoFin).ToString("MMM yyyy") : t.UltimoPeriodoPago,
                    Adeudo = newtotal != null ? Convert.ToDecimal(newtotal).ToString("C") : string.Empty
                };
                deudores.Add(deudor);
            }

            return PartialView("_ConsultarDeudores", deudores);
        }//public ActionResult _ConsultarDeudores(DateTime? periodoInicio, DateTime? periodoFin, int? calleId)
        public ActionResult Deudores()
        {
            var direccionesDomain = new DireccionesDomain(_context);
            var calles = direccionesDomain.ObtenerCalles()
                .OrderBy(c => c.Nombre)
                .ToList();
            ViewBag.Calles = calles;
            return View();
        }//public ActionResult Deudores()
        public ActionResult DescargarExcelDeudores(DateTime? periodoInicio, DateTime? periodoFin, int? calleId, string downloadToken)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            decimal? newtotal = null;

            string archivoTemplate = HttpContext.Server.MapPath("~/UploadFiles/Archivos/Templates/DeudoresTemplate.xlsx");
            string archivoNew = HttpContext.Server.MapPath("~/UploadFiles/Archivos/Reportes/Deudores/Deudores.xlsx");

            string ruta = HttpContext.Server.MapPath("~/UploadFiles/Archivos/Reportes/Deudores/");

            //Si no existe el directorio se crea
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            } // if (!Directory.Exists(ruta))

            System.IO.File.Copy(archivoTemplate, archivoNew, true);

            var periodoPagoList = periodosPagoDomain.ObtenerDeudores(periodoInicio, periodoFin, calleId);

            //Actualiza celda
            string fechaInicioVal = periodoInicio != null ? Convert.ToDateTime(periodoInicio).ToString("dd/MM/yyyy") : string.Empty;
            string fechaFinVal = periodoFin != null ? Convert.ToDateTime(periodoFin).ToString("dd/MM/yyyy") : string.Empty;

            string texto = !string.IsNullOrEmpty(fechaInicioVal) && !string.IsNullOrEmpty(fechaFinVal) ?
                            fechaInicioVal + "-" + fechaFinVal : !string.IsNullOrEmpty(fechaInicioVal) ?
                            fechaInicioVal : fechaFinVal;

            UpdateCell(archivoNew, texto, 6, "E");
            //Fin actualiza celda

            using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(archivoNew, true))
            {
                Sheet sheet = myWorkbook.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                WorksheetPart wsPart = (WorksheetPart)(myWorkbook.WorkbookPart.GetPartById(sheet.Id));

                var renglones = wsPart.Worksheet.Descendants<Row>().ToList();

                Worksheet worksheet = (myWorkbook.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                SheetData sheetData = worksheet.GetFirstChild<SheetData>();

                uint rowIndex = 9;
                int contador = 1;
                foreach (var item in periodoPagoList)
                {
                    newtotal = null;
                    if (item.MesAnoFin != null)
                    {
                        var tarifasDomain = new TarifasDomain(_context);

                        int mes = 12 - DateTime.Now.Month;

                        var periodoInicioVar = Convert.ToDateTime(item.MesAnoFin).AddMonths(1);
                        var periodoFinVar = DateTime.Now.AddMonths(mes);

                        var tarifas = tarifasDomain.ObtenerTarifasPeriodo(periodoInicioVar, periodoFinVar, item.Toma.CategoriaId);

                        decimal total = 0;

                        while (periodoInicioVar <= periodoFinVar)
                        {
                            var costoTarifa = tarifas.Where(ta => ta.MesAno == periodoInicioVar.Year).FirstOrDefault();
                            if (costoTarifa != null)
                            {
                                var mensualidad = (costoTarifa.CostoTarifa / 12);

                                total += mensualidad;
                            }
                            periodoInicioVar = periodoInicioVar.AddMonths(1);
                        }
                        newtotal = Math.Round(total);
                    }

                    var reporteRow = (Row)renglones.FirstOrDefault();

                    worksheet = reporteRow.Ancestors<Worksheet>().First();
                    sheetData = worksheet.Descendants<SheetData>().First();

                    var newRow = (Row)reporteRow.Clone();

                    newRow.RowIndex = rowIndex;
                    foreach (var cell in newRow.Elements<Cell>())
                    {
                        string cellReference = cell.CellReference.Value;
                        cell.CellReference = new StringValue(cellReference.Replace(reporteRow.RowIndex.Value.ToString(), rowIndex.ToString()));

                        var txtVacio = string.Empty;
                        cell.CellValue = new CellValue(txtVacio.ToString());
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        var columna = cellReference.Replace(reporteRow.RowIndex.Value.ToString(), string.Empty);
                        if (columna == "A")
                        {
                            string txt = contador.ToString();
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                        }
                        if (columna == "B")
                        {
                            string txt = item.Toma.Folio.ToString();
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                        }
                        if (columna == "C")
                        {
                            string txt = item.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? item.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                                         item.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                                         item.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : item.Toma.Propietario.Persona.PersonaMoral.Nombre;
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }
                        if (columna == "D")
                        {
                            string txt = (item.Toma.Direccion.TipoCalleId > 0 ? item.Toma.Direccion.TiposCalle.Nombre : string.Empty) + " " + (item.Toma.Direccion.CalleId > 0 ? item.Toma.Direccion.Calles.Nombre : string.Empty) +
                                                (!string.IsNullOrEmpty(item.Toma.Direccion.NumInt) ? " Int." + item.Toma.Direccion.NumInt : string.Empty) +
                                                (!string.IsNullOrEmpty(item.Toma.Direccion.NumExt) ? " Ext." + item.Toma.Direccion.NumExt : string.Empty) + ", " +
                                                (item.Toma.Direccion.ColoniaId > 0 ? item.Toma.Direccion.Colonias.Nombre : string.Empty);
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }
                        if (columna == "E")
                        {
                            string txt = item.Toma.Categoria.Nombre;
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }
                        if (columna == "F")
                        {
                            string txt = item.MesAnoFin != null ? Convert.ToDateTime(item.MesAnoFin).ToString("MMM yyyy") : item.UltimoPeriodoPago;
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.String);
                        }
                        if (columna == "G")
                        {
                            string txt = newtotal.ToString();
                            cell.CellValue = new CellValue(txt);
                            cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                        }
                    }
                    rowIndex++;
                    contador++;
                    if (rowIndex == 1)
                    {
                        sheetData.InsertAt(newRow, 0);
                    }
                    else
                    {
                        var lastRow = sheetData.Elements<Row>().Last(r => r.RowIndex < rowIndex);

                        sheetData.InsertAfter(newRow, lastRow);
                    }
                }

                myWorkbook.WorkbookPart.Workbook.Save();
                myWorkbook.Close();
            }

            var f = new FileInfo(archivoNew);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));
            Response.AddHeader("Content-Disposition", "attachment; filename=" + f.Name);
            Response.AddHeader("Content-Length", f.Length.ToString());
            Response.ContentType = "application/vnd.xlsx";
            Response.Flush();
            Response.TransmitFile(archivoNew);
            Response.End();

            return View();
        }
        public ActionResult ImprimirReporteDeudores(DateTime? periodoInicio, DateTime? periodoFin, int? calleId, string downloadToken)
        {
            var periodosPagoDomain = new PeriodosPagoDomain(_context);
            var deudores = new List<DeudoresViewModel>();
            decimal? newtotal = null;

            var periodoPagoList = periodosPagoDomain.ObtenerDeudores(periodoInicio, periodoFin, calleId);

            ViewBag.Periodo = periodoInicio != null ? Convert.ToDateTime(periodoInicio).ToString("dd/MM/yyyy") +
                                                (periodoFin != null ? "-" + Convert.ToDateTime(periodoFin).ToString("dd/MM/yyyy") : string.Empty) :
                              periodoFin != null ? Convert.ToDateTime(periodoFin).ToString("dd/MM/yyyy") : string.Empty;

            foreach (var t in periodoPagoList)
            {
                newtotal = null;
                if (t.MesAnoFin != null)
                {
                    var tarifasDomain = new TarifasDomain(_context);

                    int mes = 12 - DateTime.Now.Month;

                    var periodoInicioVar = Convert.ToDateTime(t.MesAnoFin).AddMonths(1);
                    var periodoFinVar = DateTime.Now.AddMonths(mes);

                    var tarifas = tarifasDomain.ObtenerTarifasPeriodo(periodoInicioVar, periodoFinVar, t.Toma.CategoriaId);

                    decimal total = 0;

                    while (periodoInicioVar <= periodoFinVar)
                    {
                        var costoTarifa = tarifas.Where(ta => ta.MesAno == periodoInicioVar.Year).FirstOrDefault();
                        if (costoTarifa != null)
                        {
                            var mensualidad = (costoTarifa.CostoTarifa / 12);

                            total += mensualidad;
                        }
                        periodoInicioVar = periodoInicioVar.AddMonths(1);
                    }
                    newtotal = Math.Round(total);
                }

                var deudor = new DeudoresViewModel()
                {
                    Categoria = t.Toma.Categoria.Abreviatura,
                    NombrePropietario = t.Toma.Propietario.Persona.TipoPersonaId == (int)TipoPersonaDomain.TipoPersonaEnum.PersonaFisica ? t.Toma.Propietario.Persona.PersonaFisica.Nombre + " " +
                                                                         t.Toma.Propietario.Persona.PersonaFisica.ApellidoPaterno + " " +
                                                                         t.Toma.Propietario.Persona.PersonaFisica.ApellidoMaterno : t.Toma.Propietario.Persona.PersonaMoral.Nombre,
                    Direccion = (t.Toma.Direccion.TipoCalleId > 0 ? t.Toma.Direccion.TiposCalle.Nombre : string.Empty) + " " + (t.Toma.Direccion.CalleId > 0 ? t.Toma.Direccion.Calles.Nombre : string.Empty) +
                                                (!string.IsNullOrEmpty(t.Toma.Direccion.NumInt) ? " Int." + t.Toma.Direccion.NumInt : string.Empty) +
                                                (!string.IsNullOrEmpty(t.Toma.Direccion.NumExt) ? " Ext." + t.Toma.Direccion.NumExt : string.Empty) + ", " +
                                                (t.Toma.Direccion.ColoniaId > 0 ? t.Toma.Direccion.Colonias.Nombre : string.Empty),
                    Folio = t.Toma.Folio.ToString(),
                    Periodo = t.MesAnoFin != null ? Convert.ToDateTime(t.MesAnoFin).ToString("MMM yyyy") : t.UltimoPeriodoPago,
                    Adeudo = newtotal != null ? Convert.ToDecimal(newtotal).ToString("C") : string.Empty
                };
                deudores.Add(deudor);
            }

            Response.AppendCookie(new HttpCookie("fileDownloadToken", downloadToken));

            return new ViewAsPdf(deudores)
            {
                FileName = string.Format("Deudores_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf"),
                PageSize = Rotativa.Options.Size.A4,
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 8"
            };
        }
        #endregion

        #region * Métodos creados por Comité Agua *
        public static void UpdateCell(string docName, string text, uint rowIndex, string columnName)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet =
                     SpreadsheetDocument.Open(docName, true))
            {
                WorksheetPart worksheetPart =
                      GetWorksheetPartByName(spreadSheet, "Deudores");

                if (worksheetPart != null)
                {
                    Cell cell = GetCell(worksheetPart.Worksheet,
                                             columnName, rowIndex);

                    cell.CellValue = new CellValue(text);
                    cell.DataType = new EnumValue<CellValues>(CellValues.String);

                    // Save the worksheet.
                    worksheetPart.Worksheet.Save();
                }
            }

        }
        private static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets =
               document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
               Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.

                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
                 document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;

        }
        private static Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => string.Compare
                   (c.CellReference.Value, columnName +
                   rowIndex, true) == 0).First();
        }
        private static Row GetRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
              Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }
        #endregion
    }//public class ReportesController : Controller
}//namespace ComiteAgua.Controllers.Reportes