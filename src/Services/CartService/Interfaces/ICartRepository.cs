using CartService.Entities;

namespace CartService.Interfaces;

public interface ICartRepository
{
    Task<CartItem?> GetCartItemAsync(int userId, int productId);

    Task<List<CartItem>> GetCartItemsAsync(int userId);

    Task<CartItem?> GetByIdAsync(int cartId);

    Task AddCartItemAsync(CartItem cartItem);

    void UpdateCartItem(CartItem cartItem);

    void RemoveCartItem(CartItem cartItem);

    Task SaveChangesAsync();
}