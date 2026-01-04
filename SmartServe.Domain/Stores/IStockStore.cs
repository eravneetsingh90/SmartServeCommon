using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore 
	{
		Task<List<StockItem>> GetStockItemsAsync();

		Task<StockItem?> GetStockItemAsync(string itemType, int referenceId);

		Task AddTransactionAsync(StockTransaction transaction);

		Task<List<CurrentStockDto>> GetCurrentStockAsync();
	}
}
