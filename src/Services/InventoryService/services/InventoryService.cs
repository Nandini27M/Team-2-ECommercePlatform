using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;
            return await _repository.AddAsync(inventory);
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;
            await _repository.UpdateAsync(inventory);
        }

        public async Task DeleteInventoryAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}