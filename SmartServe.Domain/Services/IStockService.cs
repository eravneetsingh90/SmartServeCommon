namespace SmartServe.Domain.Services
{
	public interface IStockService
	{
		Task ConsumeForOrderAsync(int orderId);
	}
}
