using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using RoboBank.Customer.Application;
using RoboBank.Customer.Application.DTOs;
using RoboBank.Customer.ServiceFabric.Service.Models;

namespace RoboBank.Customer.ServiceFabric.Service.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerApplicationService _customerApplicationService;

        public CustomerController(CustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }

        [Route("customers")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAsync(CustomerModel customerModel)
        {
            var customerInfo = Mapper.Map<CustomerModel, CustomerInfo>(customerModel);
            await _customerApplicationService.UpdateAsync(customerInfo);

            return Ok();
        }


        [Route("customers/externalId/{externalId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByExternalIdAsync(string externalId)
        {
            var customerInfo = await _customerApplicationService.GetByExternalIdAsync(externalId);

            if (customerInfo != null)
            {
                return Ok(Mapper.Map<CustomerInfo, CustomerModel>(customerInfo));
            }

            return NotFound();
        }

        [Route("customers/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(string id)
        {
            var customerInfo = await _customerApplicationService.GetByIdAsync(id);

            if (customerInfo != null)
            {
                return Ok(Mapper.Map<CustomerInfo, CustomerModel>(customerInfo));
            }

            return NotFound();
        }

        [Route("customers/search")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchAsync()
        {
            var customerInfos = await _customerApplicationService.SearchAsync(Request.GetQueryNameValuePairs());
            return Ok(Mapper.Map<List<CustomerInfo>, List<CustomerModel>>(customerInfos.ToList()));
        }
    }
}
