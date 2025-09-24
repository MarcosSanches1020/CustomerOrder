using System.Threading.Tasks;
using CustomerOrders.API.DTOs.Sellers;
using CustomerOrders.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly SellersService _sellersService;

        public SellersController(SellersService sellersService)
        {
            _sellersService = sellersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller([FromBody] SellersCreateDto newSeller)
        {
            var result = await _sellersService.createNewSellerAsync(newSeller);
            return Created("Seller successfully created", result.Datas);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSellers()
        {
            var result = await _sellersService.GetCustomersAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellersResponseDto>> GetSeller(int id)
        {
            var result = await _sellersService.GetSellerId(id);
            return Ok(result.Datas);   
        }

    }
}
