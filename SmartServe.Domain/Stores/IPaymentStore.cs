using SmartServe.Domain.Stores.SmartServe.Domain.Stores;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public interface IPaymentStore : IBaseStore<PaymentEntity>
	{
		Task<List<PaymentEntity>> GetByDateFilterAsync(DateTime fromUtc, DateTime toUtc);
	}
}
