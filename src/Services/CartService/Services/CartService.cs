using CartService.DTOs;
using CartService.Entities;
using CartService.Interfaces;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse> AddCartItemAsync(AddCartItemRequest request)
    {
        var existingItem = await _repository.GetCartItemAsync(
            request.UserId,
            request.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;

            _repository.UpdateCartItem(existingItem);
            await _repository.SaveChangesAsync();

            return new ApiResponse
            {
                Success = true,
                Message = "Cart updated successfully."
            };
        }

        var cartItem = new CartItem
        {
            UserId = request.UserId,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        await _repository.AddCartItemAsync(cartItem);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Item added to cart successfully."
        };
    }

    public async Task<List<CartResponse>> GetCartItemsAsync(int userId)
    {
        var items = await _repository.GetCartItemsAsync(userId);

        return items.Select(c => new CartResponse
        {
            CartId = c.CartId,
            UserId = c.UserId,
            ProductId = c.ProductId,
            Quantity = c.Quantity,
            CreatedAt = c.CreatedAt
        }).ToList();
    }

    public async Task<ApiResponse> UpdateCartItemAsync(
        int cartId,
        UpdateCartItemRequest request)
    {
        var cartItem = await _repository.GetByIdAsync(cartId);

        if (cartItem == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Cart item not found."
            };
        }

        cartItem.Quantity = request.Quantity;

        _repository.UpdateCartItem(cartItem);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Cart updated successfully."
        };
    }

    public async Task<ApiResponse> RemoveCartItemAsync(int cartId)
    {
        var cartItem = await _repository.GetByIdAsync(cartId);

        if (cartItem == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Cart item not found."
            };
        }

        _repository.RemoveCartItem(cartItem);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Cart item removed successfully."
        };
    }
}