using CartService.DTOs;
using CartService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add")]
public async Task<IActionResult> AddToCart(AddCartItemRequest request)
{
    var response = await _cartService.AddCartItemAsync(
        request.UserId,
        request);

    if (!response.Success)
        return BadRequest(response);

    return Ok(response);
}

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var cart = await _cartService.GetCartItemsAsync(userId);

        return Ok(cart);
    }

    [HttpPut("{cartId}")]
    public async Task<IActionResult> UpdateCart(
        int cartId,
        UpdateCartItemRequest request)
    {
        var response = await _cartService.UpdateCartItemAsync(cartId, request);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{userId}/{productId}")]
    public async Task<IActionResult> RemoveCartItem(
        int userId,
        int productId)
    {
        var response = await _cartService.RemoveCartItemAsync(userId, productId);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}