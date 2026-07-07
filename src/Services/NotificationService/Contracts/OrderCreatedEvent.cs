namespace NotificationService.Contracts;

public record OrderCreatedEvent
(
    Guid OrderId,
    Guid UserId,
    decimal Amount
);