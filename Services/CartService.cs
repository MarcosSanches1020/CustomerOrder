using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;
using CustomerOrders.API.DTOs.Cart;
using CustomerOrders.API.Mappings;

namespace CustomerOrders.API.Services
{
    public class ServiceCartResult<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

    public class CartService
    {
        private readonly AppDbContext _appDbContext;

        public CartService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ServiceCartResult<CartResponseDto>> AddItem(int customerId, int productId, int quantity)
        {
            try
            {
                var itemsCart = new CartItem { ProductId = productId, Quantity = quantity };
                var cart = await AddItemAsync(customerId, itemsCart);
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = cart.ToResponseDto(),
                    Message = "Item adicionado ao carrinho!"
                };
            }
            catch (System.Exception ex)
            {
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceCartResult<CartResponseDto>> GetCart(int customerId)
        {
            var cart = await GetCartAsync(customerId);
            if (cart == null)
            {
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Carrinho não encontrado"
                };
            }
            return new ServiceCartResult<CartResponseDto>
            {
                Success = true,
                StatusCode = 200,
                Data = cart.ToResponseDto()
            };
        }

        public async Task<ServiceCartResult<CartResponseDto>> UpdateItemQuantity(int customerId, int productId, int quantity)
        {
            try
            {
                var cart = await UpdateItemQuantityAsync(customerId, productId, quantity);
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = cart.ToResponseDto(),
                    Message = "Quantidade atualizada"
                };
            }
            catch (System.Exception ex)
            {
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceCartResult<CartResponseDto>> RemoveItem(int customerId, int productId)
        {
            try
            {
                var cart = await RemoveItemAsync(customerId, productId);
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = cart.ToResponseDto(),
                    Message = "Item removido"
                };
            }
            catch (System.Exception ex)
            {
                return new ServiceCartResult<CartResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceCartResult<object>> ClearCart(int customerId)
        {
            await ClearCartAsync(customerId);
            return new ServiceCartResult<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Carrinho limpo com sucesso"
            };
        }

        public async Task<Cart> AddItemAsync(int customerId, CartItem itemsCart)
        {
            var cart = await _appDbContext.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                cart = new Cart { CustomerId = customerId };
                _appDbContext.carts.Add(cart);
            }

            var product = await _appDbContext.products.FindAsync(itemsCart.ProductId);
            if (product == null)
            {
                throw new System.Exception("Produto não encontrado");
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == itemsCart.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += itemsCart.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = itemsCart.ProductId,
                    Quantity = itemsCart.Quantity
                });
            }

            await _appDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> GetCartAsync(int customerId)
        {
            var cart = await _appDbContext.carts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return cart;
        }

        public async Task<Cart> UpdateItemQuantityAsync(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return await RemoveItemAsync(customerId, productId);
            }

            var cart = await _appDbContext.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                throw new System.Exception("Carrinho não encontrado");
            }

            var product = await _appDbContext.products.FindAsync(productId);
            if (product == null)
            {
                throw new System.Exception("Produto não encontrado");
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem == null)
            {
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }
            else
            {
                existingItem.Quantity = quantity;
            }

            await _appDbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> RemoveItemAsync(int customerId, int productId)
        {
            var cart = await _appDbContext.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                throw new System.Exception("Carrinho não encontrado");
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem == null)
            {
                throw new System.Exception("Item não encontrado no carrinho");
            }

            cart.Items.Remove(existingItem);
            await _appDbContext.SaveChangesAsync();
            return cart;
        }

        public async Task ClearCartAsync(int customerId)
        {
            var cart = await _appDbContext.carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                return;
            }

            cart.Items.Clear();
            await _appDbContext.SaveChangesAsync();
        }
    }
}