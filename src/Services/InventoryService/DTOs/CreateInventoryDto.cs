using System.ComponentModel.DataAnnotations;

namespace InventoryService.DTOs
{
    public class CreateInventoryDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int StockQuantity { get; set; }
    }
}