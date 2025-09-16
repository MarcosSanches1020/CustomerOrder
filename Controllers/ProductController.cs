using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Models;
using CustomerOrders.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Products newProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _productService.ProductsVerify(newProduct.NameProduct))
                {
                    throw new Exception("There is already a customer registered with this CPF");
                }

                await _productService.AddProduct(newProduct);

                return Created("Custumer successfully created", newProduct);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductssAll()
        {
            var products = await _productService.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Products productsUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = await _productService.UpdateProduct(id, productsUpdate);

                if (updatedProduct == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deleted = await _productService.DeleteProduct(id);

                if (!deleted)
                {
                    return NotFound("Product not found in database");
                }

                return Ok("Product successfully deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
