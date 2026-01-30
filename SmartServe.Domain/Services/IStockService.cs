using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IStockService
	{
		Task<List<Stock>> GetStockAsync();
		Task ActivateStockItemAsync(Stock stock);
		Task<BaseResponse> AddStockAsync(List<AddStock> rows);
		Task AdjustStockAsync(int stockItemId,decimal quantity,string reason);
		Task ApplyOrderStockAsync(int orderId);

	}

}
