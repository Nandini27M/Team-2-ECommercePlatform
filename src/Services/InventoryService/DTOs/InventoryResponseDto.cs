namespace InventoryService.DTOs
{
    public class InventoryResponseDto
    {
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public int StockQuantity { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}