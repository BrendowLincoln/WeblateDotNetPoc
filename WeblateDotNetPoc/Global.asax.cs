using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using LightInject;

namespace WeblateDotNetPoc
{
    public class Global : HttpApplication
    {
        public static readonly ServiceContainer Container = new ServiceContainer();
        
        void Application_Start(object sender, EventArgs e)
        {
            // Código que é executado na inicialização do aplicativo
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            
        }

        
        private static void ConfigureLightInject()
        {
            var container = new ServiceContainer();

            container.Register<ITranslationMediator, TranslationMediator>(new PerContainerLifetime());
            container.EnableWebApi(GlobalConfiguration.Configuration);
            container.EnablePerWebRequestScope();

            // 🔥 Esta linha aqui é essencial para ExpressionBuilder
            ServiceLocator.SetContainer(container);
        }


    }
}