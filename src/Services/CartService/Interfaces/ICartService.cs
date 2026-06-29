using CartService.DTOs;

namespace CartService.Interfaces;

public interface ICartService
{
    Task<ApiResponse> AddCartItemAsync(AddCartItemRequest request);

    Task<List<CartResponse>> GetCartItemsAsync(int userId);

    Task<ApiResponse> UpdateCartItemAsync(int cartId, UpdateCartItemRequest request);

    Task<ApiResponse> RemoveCartItemAsync(int cartId);
}