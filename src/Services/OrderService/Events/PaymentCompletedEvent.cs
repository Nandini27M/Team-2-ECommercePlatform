namespace OrderService.Events;

public record PaymentCompletedEvent
(
    int OrderId,
    bool Success
);