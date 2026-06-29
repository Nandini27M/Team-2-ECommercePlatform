using ShippingService.DTOs;
using ShippingService.Models;

namespace ShippingService.Services
{
    public interface IShippingService
    {
        Task<Shipping> CreateShippingAsync(CreateShippingRequest request);
        Task<List<Shipping>> GetAllShippingsAsync();
        Task<Shipping?> GetShippingByIdAsync(int shippingId);
        Task<Shipping?> GetShippingByOrderIdAsync(Guid orderId);
        Task<Shipping> UpdateShippingStatusAsync(int shippingId, UpdateShippingStatusRequest request);
    }
}
