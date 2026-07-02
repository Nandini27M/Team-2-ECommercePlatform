using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Interfaces;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(CreateOrderRequest request)
    {
        var result = await _orderService.CreateOrderAsync(request);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var order = await _orderService.GetOrderAsync(orderId);

        if (order == null)
        {
            return NotFound(new
            {
                Success = false,
                Message = "Order not found."
            });
        }

        return Ok(order);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetOrderHistory(int userId)
    {
        var orders = await _orderService.GetOrderHistoryAsync(userId);

        return Ok(orders);
    }
}