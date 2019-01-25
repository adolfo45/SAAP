using ComiteAgua.Classes;
using ComiteAgua.Classes.Security;
using ComiteAgua.Domain;
using ComiteAgua.Filters.Security;
using ComiteAgua.Models;
using ComiteAgua.ViewModels.Tomas;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ComiteAgua.Controllers
{
    [Authenticate]
    public class HomeController : MessageControllerBase
    {

        #region * Constructor generado por Comité Agua *

        public HomeController()
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

        // GET: Home
        [Route("home")]
        public ActionResult Index(bool bandera = false)
        {                          

            if (bandera)
                ShowToastMessage("SAAP", "Bienvenido a SAAP...", ToastMessage.ToastType.Info);

            return View();          
        }
        public JsonResult Listar(AnexGRID agrid)
        {
            var propietariosDomain = new PropietariosDomain(_context);

            return Json(propietariosDomain.Listar(agrid), JsonRequestBehavior.AllowGet);
        }        
       
        #endregion

        #region * Métodos creados por Comité Agua *

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        } // protected override void Dispose(bool disposing)

        #endregion

    } // public class HomeController : Controller

} // namespace ComiteAgua.Controllers