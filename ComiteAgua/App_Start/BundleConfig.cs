using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ComiteAgua.App_Start
{
    public class BundleConfig
    {
        #region * Constructor generado por Comité Agua *

        #endregion

        #region * Enumeraciones declaradas por Comité Agua *

        #endregion

        #region * Variables declaradas por Comité Agua *

        #endregion

        #region * Propiedades declaradas por Comité Agua * 

        #endregion

        #region * Acciones generados por Comité Agua *

        #endregion

        #region * Métodos creados por Comité Agua *

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(               
                "~/Content/bootstrap.min.css",
                "~/Content/toastr.css",
                "~/Content/dataTables.bootstrap4.min.css",
                "~/Content/ionicons.min.css"                
                ));

            bundles.Add(new StyleBundle("~/Site/css").Include(
              "~/Content/Site.css"
              ));

            bundles.Add(new StyleBundle("~/Authentication/css").Include(
             "~/Content/Authenticate.css"
             ));

            bundles.Add(new StyleBundle("~/AnexGrid/css").Include(
               "~/Content/AnexGrid.css"               
               ));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
               "~/Scripts/jquery.blockUI.js",
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js",
               "~/Scripts/toastr.js",
               "~/Scripts/jquery.dataTables.min.js",
               "~/Scripts/dataTables.bootstrap4.min.js",
               "~/Scripts/popper.js",               
               "~/Scripts/autoNumeric/autoNumeric-min.js",
               "~/Scripts/bootbox.min.js"                   
           ));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
              "~/Scripts/jquery_3.3.1.js"
          ));          

        } // public static void RegisterBundles(BundleCollection bundles)

        #endregion
    }
}