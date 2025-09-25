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
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
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
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return Ok(result.Datas);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSeller(int id, [FromBody] SellersUpdateDto sellersUpdateDto)
        {
            var result = await _sellersService.updateSellerId(id, sellersUpdateDto);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return Ok(result.Datas);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var result = await _sellersService.DeleteSellerId(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return Ok(result.Message);
        }
    }
}
