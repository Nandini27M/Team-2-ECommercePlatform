using Microsoft.AspNetCore.Mvc;
using InventoryService.DTOs;
using InventoryService.Models;
using InventoryService.Services;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);

            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(CreateInventoryDto dto)
        {
            var inventory = new Inventory
            {
                ProductId = dto.ProductId,
                StockQuantity = dto.StockQuantity,
                LastUpdated = DateTime.Now
            };

            var result = await _inventoryService.CreateInventoryAsync(inventory);

            return CreatedAtAction(nameof(GetInventoryById),
                new { id = result.InventoryId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, UpdateInventoryDto dto)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);

            if (inventory == null)
                return NotFound();

            inventory.StockQuantity = dto.StockQuantity;
            inventory.LastUpdated = DateTime.Now;

            await _inventoryService.UpdateInventoryAsync(inventory);

            return Ok("Inventory Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);

            if (inventory == null)
                return NotFound();

            await _inventoryService.DeleteInventoryAsync(id);

            return Ok("Inventory Deleted Successfully");
        }
    }
}