using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using RoboBank.Customer.Application.Adapters;
using RoboBank.Customer.Application.Ports;

namespace RoboBank.Customer.ServiceFabric.Service
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbConnectionString"]);
            var customersCollection = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbDatabase"]).GetCollection<Domain.Customer>("customers");

            container.RegisterInstance(customersCollection);
            container.RegisterType<ICustomerRepository, MongoCustomerRepository>();
            container.RegisterType<IMapper, Mapper>();
        }
    }
}