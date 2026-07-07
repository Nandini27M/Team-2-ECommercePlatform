namespace OrderService.DTOs;

public class CartResponse
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}