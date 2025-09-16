using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Models;
using CustomerOrders.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddCustomer([FromBody] Customer newCustomer)
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

                await _customerService.AddCustomer(newCustomer);
                
                return Created("Custumer successfully created", newCustomer);
            }
            catch (Exception ex )
            {

                return StatusCode(400, ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAll()
        {
            var customers = await _customerService.GetCustumers();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customerUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedCustomer = await _customerService.UpdateCustomer(id, customerUpdate);

                if (updatedCustomer == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(updatedCustomer);
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