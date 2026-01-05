using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore : IBaseStore<StockItem>
	{
		Task<List<StockItem>> GetStockItemsAsync();
		Task<StockItem?> GetStockItemAsync(string itemType, int referenceId);
		Task UpdateStockItemAsync(StockItem stockItem);
		Task AddStockItemAsync(StockItem stockItem);
		Task AddTransactionAsync(StockTransaction transaction);

		Task<List<CurrentStockDto>> GetCurrentStockAsync();
	}
}
