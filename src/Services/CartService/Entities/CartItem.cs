using System.ComponentModel.DataAnnotations;

namespace CartService.Entities;

public class CartItem
{
    [Key]
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}