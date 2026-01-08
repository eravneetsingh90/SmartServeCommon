using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IStockService
	{
		Task<List<StockDto>> GetStockItemAsync(string itemType);
		Task CreateStockItemAsync(string itemType, int referenceId, string unit, decimal minStockLevel);
		Task ActivateStockItemAsync(string itemType,int referenceId);
		Task DeactivateStockItemAsync(string itemType, int referenceId);
		Task AddStockAsync(int stockItemId,decimal quantity,string reason,string referenceType = "MANUAL",int? referenceId = null);
		Task AdjustStockAsync(int stockItemId,decimal quantity,string reason);
		Task ApplyOrderStockAsync(int orderId);
		Task<List<IngredientDto>> GetIngredients();

	}

}
