using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IOrderStore
	{
		Task<Order> CreateOrderAsync(Order order);
		Task<Order?> GetOrderAsync(int orderId);
		Task UpdateOrderAsync(Order order);

		Task ClearOrderItemsAsync(int orderId);
		Task AddOrderItemsAsync(IEnumerable<OrderItem> items);
	}

}
