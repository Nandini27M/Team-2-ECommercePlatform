using System.Security.Claims;
using CartService.DTOs;
using CartService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized(new ApiResponse
            {
                Success = false,
                Message = "Invalid user."
            });
        }

        int userId = int.Parse(userIdClaim);

        var response = await _cartService.AddCartItemAsync(userId, request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized(new ApiResponse
            {
                Success = false,
                Message = "Invalid user."
            });
        }

        int userId = int.Parse(userIdClaim);

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

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveCartItem(int productId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized(new ApiResponse
            {
                Success = false,
                Message = "Invalid user."
            });
        }

        int userId = int.Parse(userIdClaim);

        var response = await _cartService.RemoveCartItemAsync(userId, productId);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}