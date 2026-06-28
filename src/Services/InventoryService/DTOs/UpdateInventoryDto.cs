using System.ComponentModel.DataAnnotations;

namespace InventoryService.DTOs
{
    public class UpdateInventoryDto
    {
        [Required]
        public int StockQuantity { get; set; }
    }
}