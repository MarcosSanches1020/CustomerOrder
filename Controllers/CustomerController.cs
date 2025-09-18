using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Models;
using CustomerOrders.API.Services;
using Microsoft.AspNetCore.Mvc;
using CustomerOrders.API.DTOs.Customer;
using CustomerOrders.API.Mappings;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _customerService.CustomerVerify(newCustomer.Cpf))
                {
                    throw new Exception("There is already a customer registered with this CPF");
                }

                var entity = newCustomer.ToEntity();
                var saved = await _customerService.AddCustomer(entity);

                return Created("Custumer successfully created", saved.ToResponseDto());
            }
            catch (Exception ex )
            {

                return StatusCode(400, ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomersAll()
        {
            var customers = await _customerService.GetCustumers();
            var result = customers.ConvertAll(c => c.ToResponseDto());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(customer.ToResponseDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existing = await _customerService.GetCustomerById(id);

                if (existing == null)
                {
                    return NotFound("Customer not found in database");
                }

                customerUpdate.ApplyToEntity(existing);

                var updatedCustomer = await _customerService.UpdateCustomer(id, existing);

                if (updatedCustomer == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(updatedCustomer.ToResponseDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var deleted = await _customerService.DeleteCustomer(id);

                if (!deleted)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok("Customer successfully deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}