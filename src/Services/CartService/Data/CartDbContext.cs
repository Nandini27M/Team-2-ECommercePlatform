using CartService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options)
        : base(options)
    {
    }

    public DbSet<CartItem> CartItems => Set<CartItem>();
}