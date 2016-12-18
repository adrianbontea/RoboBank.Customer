using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboBank.Customer.Application.Ports
{
    public interface ICustomerRepository
    {
        Task UpdateAsync(Domain.Customer customer);

        Task<Domain.Customer> GetByIdAsync(string customerId);

        Task<Domain.Customer> GetByExternalIdAsync(string externalId);

        Task<IEnumerable<Domain.Customer>> SearchAsync(IEnumerable<KeyValuePair<string, string>> criteria, int limit = 10);
    }
}
