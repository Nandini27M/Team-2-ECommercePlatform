using System.ComponentModel.DataAnnotations;

namespace CartService.DTOs;

public class AddCartItemRequest
{
    [Required(ErrorMessage = "User Id is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Product Id is required.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10.")]
    public int Quantity { get; set; }
}