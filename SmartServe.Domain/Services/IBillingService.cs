using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IBillingService
	{
		Task<Order> GetOrderAsync(int orderId);
		Task<int> CreateOrderAsync(Order request);
		Task UpdateOrderAsync(Order dto);
		Task CreateOrderItemsAsync(List<OrderItem> orderItems);
		Task UpdateOrderItemsAsync(int orderId, List<OrderItem> items);
		Task CloseOrderAsync(int orderId, Payment payment);
	}
}
