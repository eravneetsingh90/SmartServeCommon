
using SmartServe.Domain.Models;

namespace SmartServe.Domain.Services
{
	public interface IOrderReportService
	{
		Task<OrderReportResult> GetOrdersAsync(
		DateTime fromUtc,
		DateTime toUtc);
	}
}
