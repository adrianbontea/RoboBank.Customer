using System.Configuration;
using RestSharp;

namespace RoboBank.Customer.Service.IntegrationTests
{
    public class RoboRestClient : RestClient
    {
        public RoboRestClient(): base(ConfigurationManager.AppSettings["ServiceUri"])
        {
        }
    }
}
