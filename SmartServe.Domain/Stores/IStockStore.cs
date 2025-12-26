using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IStockStore
	{
		Task<int> GetStockAsync(int variantId);
		Task UpdateStockAsync(int variantId, int newQty);

		Task AddStockTransactionAsync(StockTransaction txn);
	}

}
