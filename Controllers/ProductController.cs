using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.DTOs.Product;
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
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto newProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.AddProductAsync(newProduct);
            if (!result.Success)
                return StatusCode(result.StatusCode, result.Message);

            return Created("Product successfully created", result.Data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsAll()
        {
            var result = await _productService.GetProductsAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var result = await _productService.GetProductById(id);
            if (!result.Success)
                return StatusCode(result.StatusCode, result.Message);
            return Ok(result.Data);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateProductAsync(id, productUpdate);
            if (!result.Success)
                return StatusCode(result.StatusCode, result.Message);
            return Ok(result.Data);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductById(id);
            if (!result.Success)
                return StatusCode(result.StatusCode, result.Message);
            return Ok(result.Message);
        }
    }
}
