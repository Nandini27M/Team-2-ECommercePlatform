using MassTransit;
using OrderService.Events;

namespace OrderService.Consumers;

public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        Console.WriteLine(
            $"Payment received for Order {context.Message.OrderId}");

        // TODO:
        // Update order status to Paid

        await Task.CompletedTask;
    }
}