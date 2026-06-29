using OrderService.Entities;

namespace OrderService.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);

    Task<Order?> GetOrderByIdAsync(Guid orderId);

    Task<List<Order>> GetOrdersByUserIdAsync(int userId);

    Task SaveChangesAsync();
}