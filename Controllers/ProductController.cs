using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.DTOs.Product;
using CustomerOrders.API.Mappings;
using CustomerOrders.API.Models;
using CustomerOrders.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto newProduct)
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

                var entity = newProduct.ToEntityProduct();
                var saved = await _productService.AddProduct(entity);

                return Created("Custumer successfully created", saved.ToResponseProductDto() );
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
            var result = products.ConvertAll(p => p.ToResponseProductDto());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProducts(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Customer not found in database");
                }

                return Ok(product.ToResponseProductDto());
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existing = await _productService.GetProductById(id);

                if (existing == null)
                {
                    return NotFound("Customer not found in database");
                }

                productUpdate.ApplyToEntity(existing);

                var updateProduct = await _productService.UpdateProduct(id, existing);

                if (updateProduct == null)
                {
                    return NotFound("Product not found in database");
                }

                return Ok(updateProduct.ToResponseProductDto());
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
