using PaymentGatewayDemo;
using Swashbuckle.Application;
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PaymentGatewayDemo
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration.EnableSwagger(c => c.SingleApiVersion("v1", "PaymentGatewayDemo")).EnableSwaggerUi();
        }
    }
}