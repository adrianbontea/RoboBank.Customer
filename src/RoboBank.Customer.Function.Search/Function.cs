using System.Collections.Generic;
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

namespace RoboBank.Customer.Function.Search
{
    public static class Function
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
        {
            if (req.Method == HttpMethod.Get)
            {
                MongoConfig.RegisterMappings();
                var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnectionString"]);
                var customersCollection =
                    mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"])
                        .GetCollection<RoboBank.Customer.Domain.Customer>("customers");

                var customerApplicationService =
                    new CustomerApplicationService(new MongoCustomerRepository(customersCollection),
                        new Application.Adapters.Mapper());

                var customerInfos = await customerApplicationService.SearchAsync(req.GetQueryNameValuePairs());

                return req.CreateResponse(HttpStatusCode.OK,
                    AutoMapper.Mapper.Map<List<CustomerInfo>, List<CustomerModel>>(customerInfos.ToList()));
            }

            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Method not supported");
        }
    }
}
