using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDB.Driver;
using RoboBank.Customer.Application;
using RoboBank.Customer.Application.Adapters;
using RoboBank.Customer.Application.DTOs;
using RoboBank.Customer.Function.Update;
using RoboBank.Customer.Models;

namespace RoboBank.Customer.Function.GetByExternalId
{
    public static class Function
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
        {
            if (req.Method == HttpMethod.Get)
            {
                string externalId = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => q.Key == "externalId")
                    .Value;

                if (!string.IsNullOrEmpty(externalId))
                {
                    MongoConfig.RegisterMappings();
                    var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnectionString"]);
                    var customersCollection =
                        mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"])
                            .GetCollection<RoboBank.Customer.Domain.Customer>("customers");

                    var customerApplicationService =
                        new CustomerApplicationService(new MongoCustomerRepository(customersCollection),
                            new Application.Adapters.Mapper());

                    var customerInfo = await customerApplicationService.GetByExternalIdAsync(externalId);

                    if (customerInfo != null)
                    {
                        return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<CustomerInfo, CustomerModel>(customerInfo));
                    }
                }

                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Method not supported");
        }
    }
}
