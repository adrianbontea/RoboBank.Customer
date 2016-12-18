using System;
using Microsoft.Practices.Unity;
using RoboBank.Customer.Application.Ports;
using RoboBank.Customer.Application.Isolated.Adapters;

namespace RoboBank.Customer.Service
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
            container.RegisterType<ICustomerRepository, StubCustomerRepository>();
            container.RegisterType<IMapper, Mapper>();
        }
    }
}