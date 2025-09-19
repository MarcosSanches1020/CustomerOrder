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
        public async Task<IActionResult> AddItem(int customerId, [FromBody] AddCartItemDto body)
        {
            try
            {
                var itemsCart = new CartItem { ProductId = body.productId, Quantity = body.quantity };
                var cart = await _cartService.AddItemAsync(customerId, itemsCart);
                return Ok(new { message = "Item added to cart!", cartId = cart.Id });
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var cart = await _cartService.GetCartAsync(customerId);

            if (cart == null)
                return NotFound("Cart not found.");

            var response = new
            {
                customer = cart.Customer,
                items = cart.Items.Select(items => new
                {
                    productId = items.ProductId,
                    name = items.Product.NameProduct,
                    price = items.Product.PriceProduct,
                    quantity = items.Quantity,
                    total = items.Quantity * items.Product.PriceProduct
                }),
                totalCart = cart.Items.Sum(items => items.Quantity * items.Product.PriceProduct)
            };

            return Ok(response);
        }

        [HttpPut("{customerId}/items/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(int customerId, int productId, [FromBody] UpdateCartItemQuantityDto body)
        {
            try
            {
                var cart = await _cartService.UpdateItemQuantityAsync(customerId, productId, body.quantity);
                return Ok(new { message = "Updated quantity", cartId = cart.Id });
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{customerId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(int customerId, int productId)
        {
            try
            {
                var cart = await _cartService.RemoveItemAsync(customerId, productId);
                return Ok(new { message = "Item removed", cartId = cart.Id });
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> ClearCart(int customerId)
        {
            await _cartService.ClearCartAsync(customerId);
            return Ok(new { message = "Clean cart" });
        }
    }
}
