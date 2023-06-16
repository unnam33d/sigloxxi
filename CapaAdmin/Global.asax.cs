using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CapaAdmin
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

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Response.Clear();

        //    HttpException httpException = exception as HttpException;

        //    if (httpException != null)
        //    {
        //        // Obtén el código de error HTTP
        //        int httpStatusCode = httpException.GetHttpCode();

        //        // Realiza acciones específicas para ciertos códigos de error si lo deseas
        //        // Por ejemplo, puedes redirigir a páginas de error personalizadas según el código de error

        //        // ...

        //        // Registra la excepción en el registro de errores
        //        // Puedes usar un sistema de registro como log4net o simplemente imprimir el mensaje de error en la consola
        //        Console.WriteLine(exception.Message);
        //    }

        //    Server.ClearError();
        //}

    }
}
