namespace OrderService.Events;

public record PaymentCompletedEvent
(
    Guid OrderId,
    bool Success
);