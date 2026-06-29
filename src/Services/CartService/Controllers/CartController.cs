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

    // Add Item To Cart
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddCartItemRequest request)
    {
        var response = await _cartService.AddCartItemAsync(request);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // Get User Cart
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var cart = await _cartService.GetCartItemsAsync(userId);

        return Ok(cart);
    }

    // Update Quantity
    [HttpPut("{cartId}")]
    public async Task<IActionResult> UpdateCart(
        int cartId,
        UpdateCartItemRequest request)
    {
        var response = await _cartService.UpdateCartItemAsync(cartId, request);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    // Remove Item
    [HttpDelete("{cartId}")]
    public async Task<IActionResult> RemoveCartItem(int cartId)
    {
        var response = await _cartService.RemoveCartItemAsync(cartId);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}