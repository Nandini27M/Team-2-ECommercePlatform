using System.ComponentModel.DataAnnotations;

namespace CartService.DTOs;

public class UpdateCartItemRequest
{
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10.")]
    public int Quantity { get; set; }
}