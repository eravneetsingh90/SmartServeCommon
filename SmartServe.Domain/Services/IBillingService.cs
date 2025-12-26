using SmartServe.Domain.Models;

namespace SmartServe.Domain.Services
{
	public interface IBillingService
	{
		Task<int> SaveOrderAsync(BillingSaveRequest request);
	}
}
