using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
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

		public async Task RemoveOrderItemsAsync(List<OrderItem> items)
		{
			_db.OrderItems.RemoveRange(items);
			await _db.SaveChangesAsync();
		}
		public async Task UpdateOrderItemsAsync(int orderId, List<OrderItem> orderItems)
		{
			var existingItems = await GetOrderItemsAsync(orderId);

			_db.OrderItems.RemoveRange(existingItems);

			await _db.OrderItems.AddRangeAsync(orderItems);

			await _db.SaveChangesAsync();
		}

	}

}
