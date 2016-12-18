using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using RestSharp;
using RoboBank.Customer.Domain;
using RoboBank.Customer.Service.IntegrationTests.Models;
using TechTalk.SpecFlow;

namespace RoboBank.Customer.Service.IntegrationTests
{
    [Binding]
    public class Steps
    {
        private readonly IRestClient _restClient;
        private readonly IMongoCollection<Domain.Customer> _customersCollection;

        public Steps(IRestClient restClient, IMongoCollection<Domain.Customer> customersCollection)
        {
            _restClient = restClient;
            _customersCollection = customersCollection;
        }

        [AfterScenario]
        public void Teardown()
        {
            _customersCollection.DeleteMany(new BsonDocument());
        }

        [Given(@"There is one customer in the system with id = '(.*)'")]
        public void GivenThereIsOneCustomerInTheSystemWithId(string p0)
        {
            var customer = new Domain.Customer { Id = p0 };
            _customersCollection.InsertOne(customer);
        }

        [When(@"I request the customer with id = '(.*)'")]
        public void WhenIRequestTheCustomerWithId(string p0)
        {
            var request = new RestRequest($"customers/{p0}", Method.GET);
            var response = _restClient.Execute<LiteEntityModel>(request);

            ScenarioContext.Current.Add("StatusCode", response.StatusCode);
            ScenarioContext.Current.Add("result", response.StatusCode == HttpStatusCode.OK ? response.Data : null);
        }

        [Then(@"The result should include the customer with id = '(.*)'")]
        public void ThenTheResultShouldIncludeTheCustomerWithId(string p0)
        {
            var result = ScenarioContext.Current["result"] as LiteEntityModel;
            Assert.IsNotNull(result);
            Assert.AreEqual(p0, result.Id);
        }

        [Then(@"The status code should be (.*)")]
        public void ThenTheStatusCodeShouldBe(int p0)
        {
            var statusCode = (HttpStatusCode)ScenarioContext.Current["StatusCode"];
            Assert.AreEqual((HttpStatusCode)p0, statusCode);
        }

        [Then(@"The result should be empty")]
        public void ThenTheResultShouldBeEmpty()
        {
            var result = ScenarioContext.Current["result"] as LiteEntityModel;
            Assert.IsTrue(result == null || string.IsNullOrEmpty(result.Id));
        }

        [Given(@"There is one customer in the system with external id ='(.*)' and id = '(.*)'")]
        public void GivenThereIsOneCustomerInTheSystemWithExternalIdAndId(string p0, string p1)
        {
            var customer = new Domain.Customer { ExternalId = p0, Id = p1};
            _customersCollection.InsertOne(customer);
        }

        [When(@"I request the customer with external id = '(.*)'")]
        public void WhenIRequestTheCustomerWithExternalId(string p0)
        {
            var request = new RestRequest($"customers/externalId/{p0}", Method.GET);
            var response = _restClient.Execute<LiteEntityModel>(request);

            ScenarioContext.Current.Add("StatusCode", response.StatusCode);
            ScenarioContext.Current.Add("result", response.StatusCode == HttpStatusCode.OK ? response.Data : null);
        }

        [When(@"I send a customer update with id = '(.*)' and external id = '(.*)'")]
        public void WhenISendACustomerUpdateWithIdAndExternalId(string p0, string p1)
        {
            var request = new RestRequest("customers", Method.POST);
            request.AddJsonBody(new { Id = p0, ExternalId = p1, Person = "Legal" });

            var response = _restClient.Execute(request);
            ScenarioContext.Current.Add("StatusCode", response.StatusCode);
        }


        [Then(@"The customer with id = '(.*)' should be updated with external id = '(.*)'")]
        public void ThenTheCustomerWithIdShouldBeUpdatedWithExternalId(string p0, string p1)
        {
            var customer = _customersCollection.Find(cst => cst.Id == p0).FirstOrDefault();

            Assert.IsNotNull(customer);
            Assert.AreEqual(p1, customer.ExternalId);
        }

        [When(@"I send a customer update with external id = '(.*)'")]
        public void WhenISendACustomerUpdateWithExternalId(string p0)
        {
            var request = new RestRequest("customers", Method.POST);
            request.AddJsonBody(new { ExternalId = p0, Person = "Legal" });

            var response = _restClient.Execute(request);
            ScenarioContext.Current.Add("StatusCode", response.StatusCode);
        }

        [Then(@"Customer with id = '(.*)' should not be created")]
        public void ThenCustomerWithIdShouldNotBeCreated(string p0)
        {
            var customer = _customersCollection.Find(cst => cst.Id == p0).FirstOrDefault();
            Assert.IsNull(customer);
        }

        [Given(@"There is one customer in the system with profile Something = '(.*)' and SometingElse = '(.*)' and AllowsSearch = '(.*)' and id = '(.*)'")]
        public void GivenThereIsOneCustomerInTheSystemWithProfileSomethingAndSometingElseAndAllowsSearchAndId(string p0, string p1, string p2, string p3)
        {
            var customer = new Domain.Customer { Profile = new Profile(), AllowsSearch = bool.Parse(p2), Id = p3};
            customer.Profile.Properties.Add("Something", p0);
            customer.Profile.Properties.Add("SomethingElse", p1);

            _customersCollection.InsertOne(customer);
        }

        [When(@"I search for Something = '(.*)' and SometingElse = '(.*)'")]
        public void WhenISearchForSomethingAndSometingElse(string p0, string p1)
        {
            var request = new RestRequest($"customers/search?Something={p0}&SomethingElse={p1}", Method.GET);
            var response = _restClient.Execute<List<LiteEntityModel>>(request);

            ScenarioContext.Current.Add("StatusCode", response.StatusCode);
            ScenarioContext.Current.Add("result", response.StatusCode == HttpStatusCode.OK ? response.Data : null);
        }

        [Then(@"The list result should include the customer with id = '(.*)'")]
        public void ThenTheListResultShouldIncludeTheCustomerWithId(string p0)
        {
            var result = ScenarioContext.Current["result"] as List<LiteEntityModel>;
            Assert.IsNotNull(result);
            var customer = result.FirstOrDefault();
            Assert.IsNotNull(customer);

            Assert.AreEqual(p0, customer.Id);
        }

        [Then(@"The list result should be empty")]
        public void ThenTheListResultShouldBeEmpty()
        {
            var result = ScenarioContext.Current["result"] as List<LiteEntityModel>;
            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Any());
        }

    }
}
