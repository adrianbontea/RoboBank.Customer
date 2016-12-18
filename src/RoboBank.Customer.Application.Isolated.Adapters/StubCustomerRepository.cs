using RoboBank.Customer.Application.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoboBank.Customer.Domain;

namespace RoboBank.Customer.Application.Isolated.Adapters
{
    public class StubCustomerRepository : ICustomerRepository
    {
        public Task<Domain.Customer> GetByExternalIdAsync(string externalId)
        {
            return Task.FromResult(new Domain.Customer
            {
                Id = "abc",
                ExternalId = externalId,
                AllowsSearch = true,
                Person = PersonType.Natural,
                Profile = new Profile()
            });
        }

        public Task<Domain.Customer> GetByIdAsync(string customerId)
        {
            return Task.FromResult(new Domain.Customer
            {
                Id = customerId,
                ExternalId = "xyz",
                AllowsSearch = true,
                Person = PersonType.Natural,
                Profile = new Profile()
            });
        }

        public Task<IEnumerable<Domain.Customer>> SearchAsync(IEnumerable<KeyValuePair<string, string>> criteria, int limit = 10)
        {
            return Task.FromResult<IEnumerable<Domain.Customer>>(new List<Domain.Customer>
            {
                new Domain.Customer
                {
                    Id = "search1",
                    ExternalId = "xyz",
                    AllowsSearch = true,
                    Person = PersonType.Natural,
                    Profile = new Profile()
                }
            });
        }

        public Task UpdateAsync(Domain.Customer customer)
        {
            return Task.CompletedTask;
        }
    }
}
