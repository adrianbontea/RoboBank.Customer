using BoDi;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using RestSharp;
using TechTalk.SpecFlow;

namespace RoboBank.Customer.Service.IntegrationTests
{
    [Binding]
    public class PartiesSetup
    {
        private readonly IObjectContainer _objectContainer;

        public PartiesSetup (IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void Setup()
        {
            var unityContainer = UnityConfig.GetConfiguredContainer();
            _objectContainer.RegisterInstanceAs(unityContainer.Resolve<IMongoCollection<Domain.Customer>>());
            _objectContainer.RegisterTypeAs<RoboRestClient, IRestClient>();
            MongoConfig.RegisterMappings();
        }
    }
}
