using SmartServe.Domain.Models;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Services
{
	public interface IBillingService
	{
		Task<int> UpdateOrderAsync(Order order);
		Task<int> SaveOrderAsync(BillingSaveRequest request);
		Task<Order?> GetOrderAsync(int orderId);
	}
}
