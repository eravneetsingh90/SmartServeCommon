using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<Order>, IOrderStore
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public async Task<Order?> GetOrderAsync(int orderId)
		{
			return await _db.Orders
				.AsNoTracking()
				.Include(o => o.OrderItems)
					.ThenInclude(oi => oi.Variant)
						.ThenInclude(v => v.Product)
				.FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task ClearOrderItemsAsync(int orderId)
		{
			var items = _db.OrderItems.Where(x => x.OrderId == orderId);
			_db.OrderItems.RemoveRange(items);
			await _db.SaveChangesAsync();
		}
	}
}
