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

    public async Task<ApiResponse> AddCartItemAsync(
        int userId,
        AddCartItemRequest request)
    {
        if (request.Quantity > 10)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Maximum quantity allowed is 10."
            };
        }

        var existingItem = await _repository.GetCartItemAsync(
            userId,
            request.ProductId);

        if (existingItem != null)
        {
            if (existingItem.Quantity + request.Quantity > 10)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Maximum quantity allowed is 10."
                };
            }

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
            UserId = userId,
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

    public async Task<ApiResponse> RemoveCartItemAsync(
        int userId,
        int productId)
    {
        var cartItem = await _repository.GetCartItemAsync(
            userId,
            productId);

        if (cartItem == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Product not found in cart."
            };
        }

        _repository.RemoveCartItem(cartItem);

        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Product removed from cart successfully."
        };
    }
}