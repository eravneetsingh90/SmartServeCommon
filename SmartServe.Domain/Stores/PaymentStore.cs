using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class PaymentStore : BaseStore<PaymentEntity>, IPaymentStore
	{
		public PaymentStore(SmartServeDbContext db) : base(db)
		{
		}
		
	}
}
