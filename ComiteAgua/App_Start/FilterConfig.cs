using ComiteAgua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComiteAgua.App_Start
{
    public class FilterConfig
    {


        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MessagesActionFilter());

        } // public static void RegisterGlobalFilters(GlobalFilterCollection filters)


    } // public class FilterConfig
} // namespace ComiteAgua.App_Start