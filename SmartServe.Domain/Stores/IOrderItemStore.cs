using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IOrderItemStore : IBaseStore<OrderItem>
	{
		Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
		Task AddOrderItemsAsync(List<OrderItem> items);
	}
}
