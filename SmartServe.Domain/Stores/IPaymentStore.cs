using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IPaymentStore : IBaseStore<Payment>
	{
		Task AddAsync(Payment payment);
		Task AddRangeAsync(IEnumerable<Payment> payments);
	}
}
