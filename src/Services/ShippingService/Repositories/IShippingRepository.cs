using ShippingService.Models;
using ShippingService.Models;

namespace SmartBank.ShippingService.Repositories;

public interface IShippingRepository
{
    Task<Shipping> CreateShippingAsync(Shipping shipping);
    Task<List<Shipping>> GetAllShippingsAsync();
    Task<Shipping?> GetShippingByIdAsync(int shippingId);
    Task<Shipping?> GetShippingByOrderIdAsync(Guid orderId);
    Task UpdateShippingAsync(Shipping shipping);
}