using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore : IBaseStore<StockEntity>
	{
		Task<List<StockEntity>> GetAllStockAsync();
		Task<List<StockEntity>> GetStockAsync();
		Task<List<StockEntity>> GetStockAsync(string itemType);
		Task<StockEntity?> GetStockAsync(string itemType, int referenceId);
		Task UpdateStockAsync(StockEntity stockItem);
		Task AddStockAsync(StockEntity stockItem);
		Task<List<CurrentStock>> GetCurrentStockAsync();
		Task<decimal> GetCurrentStockQuantityAsync(int id);
	}
}
