using System.Collections.Generic;
using System.Threading.Tasks;
using RoboBank.Customer.Application.DTOs;
using RoboBank.Customer.Application.Ports;

namespace RoboBank.Customer.Application
{
    public class CustomerApplicationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerApplicationService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task UpdateAsync(CustomerInfo customerInfo)
        {
            var customer = _mapper.Map<CustomerInfo, Domain.Customer>(customerInfo);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<CustomerInfo> GetByExternalIdAsync(string externalId)
        {
            var customer = await _customerRepository.GetByExternalIdAsync(externalId);
            return _mapper.Map<Domain.Customer, CustomerInfo>(customer);
        }

        public async Task<CustomerInfo> GetByIdAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<Domain.Customer, CustomerInfo>(customer);
        }

        public async Task<IEnumerable<CustomerInfo>> SearchAsync(IEnumerable<KeyValuePair<string, string>> criteria)
        {
            var customers = await _customerRepository.SearchAsync(criteria);
            return _mapper.Map<IEnumerable<Domain.Customer>, IEnumerable<CustomerInfo>>(customers);
        }
    }
}
