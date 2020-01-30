using PaymentGatewayDemo.APIClients;
using PaymentGatewayDemo.APIClients.Interfaces;
using PaymentGatewayDemo.Logging;
using PaymentGatewayDemo.Repositories;
using PaymentGatewayDemo.Repositories.Interfaces;
using PaymentGatewayDemo.Services;
using PaymentGatewayDemo.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PaymentGatewayDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ILogger, Logger>(Lifestyle.Scoped);

            container.Register<IPaymentService, PaymentService>(Lifestyle.Scoped);
            container.Register<IBankAPIClient, BankAPIClient>(Lifestyle.Scoped);

            container.Register<IPaymentRepository, PaymentRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
