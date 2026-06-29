using Microsoft.EntityFrameworkCore;
using ShippingService.Models;
namespace SShippingService.Data;

public class ShippingDbContext : DbContext
{
    public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Shipping> Shippings { get; set; }
}
