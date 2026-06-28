namespace InventoryService.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public int StockQuantity { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}