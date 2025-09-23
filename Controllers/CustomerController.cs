using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerOrders.API.DTOs.Customer;
using CustomerOrders.API.Services;

namespace CustomerOrders.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateDto newCustomer)
        {
            var result = await _customerService.AddCustomerAsync(newCustomer);
            return Created("Customer successfully created", result.Data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomersAll()
        {
            var result = await _customerService.GetCustomersAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
        {
            var result = await _customerService.GetCustomerById(id);
            return Ok(result.Data);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdate)
        { 
            var result = await _customerService.UpdateCustomerId(id, customerUpdate);
            return Ok(result.Data);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomeID(id);
            return Ok(result.Message);
        }
    }
}