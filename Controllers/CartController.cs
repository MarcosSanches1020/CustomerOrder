using System.Linq;
using CustomerOrders.API.Models;
using Microsoft.AspNetCore.Mvc;
using CustomerOrders.API.Services;
using System.Threading.Tasks;

namespace CustomerOrders.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public record AddCartItemDto(int productId, int quantity);
        public record UpdateCartItemQuantityDto(int quantity);


        [HttpPost("{customerId}/items")]
        public async Task<IActionResult> AddItemInit(int customerId, [FromBody] AddCartItemDto body)
        {
            var result = await _cartService.AddItem(customerId, body.productId, body.quantity);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartInit(int customerId)
        {
            var result = await _cartService.GetCart(customerId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{customerId}/items/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(int customerId, int productId, [FromBody] UpdateCartItemQuantityDto body)
        {
            var result = await _cartService.UpdateItemQuantity(customerId, productId, body.quantity);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{customerId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemInit(int customerId, int productId)
        {
            var result = await _cartService.RemoveItem(customerId, productId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> ClearCartInit(int customerId)
        {
            var result = await _cartService.ClearCart(customerId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, result);
        }
    }
}