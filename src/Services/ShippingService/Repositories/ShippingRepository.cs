using ShippingService.Models;
using Microsoft.EntityFrameworkCore;
using SmartBank.ShippingService.Repositories;
using SShippingService.Data;

namespace ECommerce.ShippingService.Repositories;

public class ShippingRepository : IShippingRepository
{
    private readonly ShippingDbContext _context;

    public ShippingRepository(ShippingDbContext context)
    {
        _context = context;
    }

    public async Task<Shipping> CreateShippingAsync(Shipping shipping)
    {
        await _context.Shippings.AddAsync(shipping);
        await _context.SaveChangesAsync();
        return shipping;
    }

    public async Task<List<Shipping>> GetAllShippingsAsync()
    {
        return await _context.Shippings.ToListAsync();
    }

    public async Task<Shipping?> GetShippingByIdAsync(int shippingId)
    {
        return await _context.Shippings.FindAsync(shippingId);
    }

    public async Task<Shipping?> GetShippingByOrderIdAsync(Guid orderId)
    {
        return await _context.Shippings
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }

    public async Task UpdateShippingAsync(Shipping shipping)
    {
        _context.Shippings.Update(shipping);
        await _context.SaveChangesAsync();
    }
}