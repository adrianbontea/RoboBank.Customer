using System.Configuration;
using Microsoft.ApplicationInsights.Extensibility;

namespace RoboBank.Customer.ServiceFabric.Service
{
    public static class ApplicationInsightsConfig
    {
        public static void Configure()
        {
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["ApplicationInsightsKey"];
        }
    }
}