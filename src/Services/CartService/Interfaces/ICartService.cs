using CartService.DTOs;

namespace CartService.Interfaces;

public interface ICartService
{
    Task<ApiResponse> AddCartItemAsync(
        int userId,
        AddCartItemRequest request);

    Task<List<CartResponse>> GetCartItemsAsync(int userId);

    Task<ApiResponse> UpdateCartItemAsync(
        int cartId,
        UpdateCartItemRequest request);

    Task<ApiResponse> RemoveCartItemAsync(
        int userId,
        int productId);
}