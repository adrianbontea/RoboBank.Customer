using System.Web.Http;
using Owin;

namespace RoboBank.Customer.ServiceFabric.Service
{
    public static class Startup
    {
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            AutoMapperConfig.RegisterMappings();
            MongoConfig.RegisterMappings();
            HttpConfiguration config = new HttpConfiguration();
            SwaggerConfig.Register(config);
            WebApiConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}
