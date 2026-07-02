using OrderService.Entities;

namespace OrderService.Interfaces;

public interface IOrderPublisher
{
    Task PublishOrderCreatedAsync(Order order);
}