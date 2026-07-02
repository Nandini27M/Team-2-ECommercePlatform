namespace OrderService.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}