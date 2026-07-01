using MassTransit;
using NotificationService.Contracts;
using NotificationService.DTOs;
using NotificationService.Interfaces;

namespace NotificationService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(
        INotificationService notificationService,
        ILogger<OrderCreatedConsumer> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("OrderCreatedEvent received. OrderId: {OrderId}", message.OrderId);

        var notificationRequest = new NotificationRequestDto
        {
            Recipient = "customer@example.com",
            Subject = "Order Placed Successfully",
            Message = $"Your order {message.OrderId} has been placed successfully. Amount: {message.Amount}",
            NotificationType = "Email"
        };

        await _notificationService.SendNotificationAsync(notificationRequest);
    }
}