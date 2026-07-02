namespace OrderService.DTOs;

public class OrderResponse
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }
}