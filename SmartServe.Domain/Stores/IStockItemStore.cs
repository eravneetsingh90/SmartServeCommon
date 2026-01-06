using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockItemStore : IBaseStore<StockItem>
	{
		Task<List<StockItem>> GetStockItemsAsync();
		Task<List<StockItem>> GetStockItemAsync(string itemType);
		Task<StockItem?> GetStockItemAsync(string itemType, int referenceId);
		Task UpdateStockItemAsync(StockItem stockItem);
		Task AddStockItemAsync(StockItem stockItem);
		Task<List<CurrentStockDto>> GetCurrentStockAsync();
		Task<decimal> GetCurrentStockQuantityAsync(int stockItemId);
	}
}
