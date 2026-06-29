using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingService.DTOs;
using ShippingService.Services;

namespace ShippingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipping(CreateShippingRequest request)
        {
            var result = await _shippingService.CreateShippingAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippings()
        {
            var result = await _shippingService.GetAllShippingsAsync();
            return Ok(result);
        }

        [HttpGet("{shippingId:int}")]
        public async Task<IActionResult> GetShippingById(int shippingId)
        {
            var result = await _shippingService.GetShippingByIdAsync(shippingId);

            if (result == null)
                return NotFound("Shipping record not found");

            return Ok(result);
        }

        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetShippingByOrderId(Guid orderId)
        {
            var result = await _shippingService.GetShippingByOrderIdAsync(orderId);

            if (result == null)
                return NotFound("Shipping record not found for this order");

            return Ok(result);
        }

        [HttpPut("{shippingId:int}/status")]
        public async Task<IActionResult> UpdateShippingStatus(
            int shippingId,
            UpdateShippingStatusRequest request)
        {
            var result = await _shippingService.UpdateShippingStatusAsync(shippingId, request);
            return Ok(result);
        }
    }
}
