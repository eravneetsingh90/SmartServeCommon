using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class OrderItemStore : BaseStore<OrderItem>,IOrderItemStore
	{
		public OrderItemStore(SmartServeDbContext db) : base(db) { }

		public async Task<List<OrderItem>> GetOrderItemsAsync(int orderId)
		{
			return await _db.OrderItems
				.Where(i => i.OrderId == orderId)
				.ToListAsync();
		}
		public async Task AddOrderItemsAsync(List<OrderItem> items)
		{
			_db.OrderItems.AddRange(items);
			await _db.SaveChangesAsync();
		}

	}

}
