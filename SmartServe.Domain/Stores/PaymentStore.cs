using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class PaymentStore : BaseStore<Payment>, IPaymentStore
	{
		public PaymentStore(SmartServeDbContext db) : base(db)
		{
		}
		public async Task AddRangeAsync(IEnumerable<Payment> payments)
		{
			await _db.Payments.AddRangeAsync(payments);
			await _db.SaveChangesAsync();
		}
	}
}
