namespace SmartServe.Domain.Services
{
	public interface IStockService
	{
		Task ApplyOrderStockAsync(int orderId);

		Task AddStockAsync(
			string itemType,
			int referenceId,
			decimal quantity,
			string reason);

		Task AdjustStockAsync(
			int stockItemId,
			decimal quantity,
			string reason);

		Task EnsureStockItemAsync(
			string itemType,
			int referenceId,
			string unit);

	}

}
