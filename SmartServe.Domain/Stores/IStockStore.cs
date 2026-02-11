using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore : IBaseStore<StockEntity>
	{
		Task<List<StockEntity>> GetAllStockAsync();
		Task<List<StockEntity>> GetStockAsync();
		Task<StockEntity?> GetStockAsync(int referenceId);
		Task<StockEntity?> GetStockAsync(string itemType, int referenceId);
		Task<List<CurrentStock>> GetCurrentStockAsync();
	}
}
