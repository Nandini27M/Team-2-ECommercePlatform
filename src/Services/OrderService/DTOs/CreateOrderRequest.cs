using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class CreateOrderRequest
{
    [Required]
    public int UserId { get; set; }
}