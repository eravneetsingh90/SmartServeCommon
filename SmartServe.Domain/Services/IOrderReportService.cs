
using SmartServe.Domain.Models;

namespace SmartServe.Domain.Services
{
	public interface IOrderReportService
	{
		Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
		Task<OrderReportResult> GetOrdersAsync(
		DateTime fromUtc,
		DateTime toUtc);
	}
}
