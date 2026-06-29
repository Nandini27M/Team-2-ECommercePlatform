using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities;

public class Order
{
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required]
    public int UserId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}