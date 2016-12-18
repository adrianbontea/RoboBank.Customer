#r "System.Configuration"
#r "RoboBank.Customer.Application.dll"
#r "RoboBank.Customer.Application.Adapters.dll"
#r "RoboBank.Customer.Domain.dll"
#r "RoboBank.Customer.Models.dll"

using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using RoboBank.Customer.Application;
using RoboBank.Customer.Application.Adapters;
using RoboBank.Customer.Application.DTOs;
using RoboBank.Customer.Models;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
{
    if (req.Method == HttpMethod.Get)
    {
        string id = req.GetQueryNameValuePairs()
            .FirstOrDefault(q => q.Key == "id")
            .Value;

        if (!string.IsNullOrEmpty(id))
        {
            MongoConfig.RegisterMappings();
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnectionString"]);
            var customersCollection =
                mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"])
                    .GetCollection<RoboBank.Customer.Domain.Customer>("customers");

            var customerApplicationService =
                new CustomerApplicationService(new MongoCustomerRepository(customersCollection),
                    new Application.Adapters.Mapper());

            var customerInfo = await customerApplicationService.GetByIdAsync(id);

            if (customerInfo != null)
            {
                return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<CustomerInfo, CustomerModel>(customerInfo));
            }
        }

        return req.CreateResponse(HttpStatusCode.NotFound);
    }

    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Method not supported");
}