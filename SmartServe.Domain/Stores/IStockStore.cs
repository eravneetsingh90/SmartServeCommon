using SmartServe.Domain.Models;
using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore : IBaseStore<Stock>
	{
		Task<List<Stock>> GetStockAsync();
		Task<List<Stock>> GetStockAsync(string itemType);
		Task<Stock?> GetStockAsync(string itemType, int referenceId);
		Task UpdateStockAsync(Stock stockItem);
		Task AddStockAsync(Stock stockItem);
		Task<List<CurrentStockDto>> GetCurrentStockAsync();
		Task<decimal> GetCurrentStockQuantityAsync(int id);
	}
}
