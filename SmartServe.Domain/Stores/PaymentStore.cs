using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class PaymentStore : BaseStore<Payment>, IPaymentStore
	{
		public PaymentStore(SmartServeDbContext db) : base(db)
		{
		}
		
	}
}
