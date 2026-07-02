using MassTransit;
using OrderService.Entities;
using OrderService.Events;
using OrderService.Interfaces;

namespace OrderService.Publishers;

public class OrderPublisher : IOrderPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishOrderCreatedAsync(Order order)
    {
        foreach (var item in order.OrderItems)
        {
            await _publishEndpoint.Publish(new OrderCreatedEvent
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }
    }
}