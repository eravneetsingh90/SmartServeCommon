using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IBillingService
	{
		Task<OrderDto> GetOrderAsync(int orderId);
		Task<int> CreateOrderAsync(OrderDto request);
		Task UpdateOrderAsync(OrderDto dto);
		Task CreateOrderItemsAsync(List<OrderItemDto> orderItems);
		Task UpdateOrderItemsAsync(int orderId, List<OrderItemDto> items);
	}
}
