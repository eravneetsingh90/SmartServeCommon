using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IOrderStore : IBaseStore<Order>
	{
		//Task<Order> CreateOrderAsync(Order order);
		Task<Order?> GetOrderAsync(int orderId);
		Task ClearOrderItemsAsync(int orderId);
		Task AddOrderItemsAsync(IEnumerable<OrderItem> items);
	}

}
