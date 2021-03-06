﻿#r "System.Configuration"
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
    if (req.Method == HttpMethod.Post)
    {
        MongoConfig.RegisterMappings();

        var customer = await req.Content.ReadAsAsync<CustomerModel>();

        if (customer != null)
        {
            var customerInfo = AutoMapper.Mapper.Map<CustomerModel, CustomerInfo>(customer);

            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnectionString"]);
            var customersCollection =
                mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"])
                    .GetCollection<RoboBank.Customer.Domain.Customer>("customers");

            var customerApplicationService =
                new CustomerApplicationService(new MongoCustomerRepository(customersCollection),
                    new Application.Adapters.Mapper());
            await customerApplicationService.UpdateAsync(customerInfo);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }

    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Method not supported");
}