using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IBillingService
	{
		Task UpdateOrderAsync(OrderDto dto);
		Task<int> CreateOrderAsync(OrderDto request);
		Task CreateOrderItemsAsync(List<OrderItemDto> orderItems);
		Task<OrderDto> GetOrderAsync(int orderId);
	}
}
