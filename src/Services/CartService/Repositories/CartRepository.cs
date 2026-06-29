using CartService.Data;
using CartService.Entities;
using CartService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CartService.Repositories;

public class CartRepository : ICartRepository
{
    private readonly CartDbContext _context;

    public CartRepository(CartDbContext context)
    {
        _context = context;
    }

    public async Task<CartItem?> GetCartItemAsync(int userId, int productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                c.ProductId == productId);
    }

    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        return await _context.CartItems
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<CartItem?> GetByIdAsync(int cartId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(c => c.CartId == cartId);
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
    }

    public void UpdateCartItem(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
    }

    public void RemoveCartItem(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}