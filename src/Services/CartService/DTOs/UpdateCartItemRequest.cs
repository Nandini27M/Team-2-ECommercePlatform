using System.ComponentModel.DataAnnotations;

namespace CartService.DTOs;

public class UpdateCartItemRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}