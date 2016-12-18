using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;

namespace RoboBank.Customer.ServiceFabric.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
        }
    }
}
