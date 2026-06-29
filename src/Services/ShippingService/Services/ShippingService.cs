using ShippingService.DTOs;
using ShippingService.Models;
using SmartBank.ShippingService.Repositories;

namespace ShippingService.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _repository;
        private readonly ILogger<ShippingService> _logger;

        public ShippingService(
            IShippingRepository repository,
            ILogger<ShippingService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Shipping> CreateShippingAsync(CreateShippingRequest request)
        {
            var existingShipping = await _repository.GetShippingByOrderIdAsync(request.OrderId);

            if (existingShipping != null)
            {
                _logger.LogWarning("Shipping already exists for OrderId: {OrderId}", request.OrderId);
                return existingShipping;
            }

            var shipping = new Shipping
            {
                OrderId = request.OrderId,
                TransactionId = request.TransactionId,
                CustomerEmail = request.CustomerEmail,
                ShippingAddress = request.ShippingAddress,
                TrackingNumber = GenerateTrackingNumber(),
                Carrier = "ECom Express",
                ShippingStatus = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            var result = await _repository.CreateShippingAsync(shipping);

            _logger.LogInformation("Shipping created for OrderId: {OrderId}", request.OrderId);

            return result;
        }

        public async Task<List<Shipping>> GetAllShippingsAsync()
        {
            return await _repository.GetAllShippingsAsync();
        }

        public async Task<Shipping?> GetShippingByIdAsync(int shippingId)
        {
            return await _repository.GetShippingByIdAsync(shippingId);
        }

        public async Task<Shipping?> GetShippingByOrderIdAsync(Guid orderId)
        {
            return await _repository.GetShippingByOrderIdAsync(orderId);
        }

        public async Task<Shipping> UpdateShippingStatusAsync(int shippingId, UpdateShippingStatusRequest request)
        {
            var shipping = await _repository.GetShippingByIdAsync(shippingId);

            if (shipping == null)
            {
                throw new Exception("Shipping record not found");
            }

            shipping.ShippingStatus = request.ShippingStatus;

            if (request.ShippingStatus == "Shipped")
            {
                shipping.ShippedAt = DateTime.UtcNow;
            }

            if (request.ShippingStatus == "Delivered")
            {
                shipping.DeliveredAt = DateTime.UtcNow;
            }

            await _repository.UpdateShippingAsync(shipping);

            _logger.LogInformation(
                "Shipping status updated. ShippingId: {ShippingId}, Status: {Status}",
                shippingId,
                request.ShippingStatus);

            return shipping;
        }

        private static string GenerateTrackingNumber()
        {
            return "TRK-" + DateTime.UtcNow.Ticks;
        }
    }
}
