using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IOrderItemStore : IBaseStore<OrderItemEntity>
	{
		Task<List<OrderItemEntity>> GetOrderItemsAsync(int orderId);
		Task AddOrderItemsAsync(List<OrderItemEntity> items);
		Task UpdateOrderItemsAsync(int orderId, List<OrderItemEntity> orderItems);
	}
}
