using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class PaymentStore : BaseStore<PaymentEntity>, IPaymentStore
	{
		public PaymentStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db, tenantProvider)
		{

		}

		public async Task AddPaymentsAsync(List<PaymentEntity> items)
		{
			AddRange(items);
			await SaveAsync();
		}

		public async Task<List<PaymentEntity>> GetByDateFilterAsync(DateTime fromUtc, DateTime toUtc)
		{
			var result = await Set
				.AsNoTracking()
				.Where(o => o.CreatedAt >= fromUtc && o.CreatedAt < toUtc)
				.OrderByDescending(o => o.CreatedAt)
				.ToListAsync();
			return result;
		}
	}
}
