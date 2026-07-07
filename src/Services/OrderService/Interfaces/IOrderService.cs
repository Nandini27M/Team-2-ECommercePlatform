using OrderService.DTOs;

namespace OrderService.Interfaces;

public interface IOrderService
{
    Task<ApiResponse> CreateOrderAsync(CreateOrderRequest request);

    Task<OrderResponse?> GetOrderAsync(int orderId);

    Task<List<OrderResponse>> GetOrderHistoryAsync(int userId);
}