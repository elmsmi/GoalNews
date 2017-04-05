using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoalNews
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        // Maneja las excepciones en cada metodo de cada controlador
        // Emula [HandleError] para cada metodod
        // No maneja los errores HTTP
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        //// Metodo para detectar errores
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Response.Clear();

        //    HttpException httpException = exception as HttpException;

        //    int error = httpException != null ? httpException.GetHttpCode() : 0;

        //    Server.ClearError();
        //    Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
        //}
    }
}
