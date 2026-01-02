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
			return await Set
				.Where(i => i.OrderId == orderId)
				.ToListAsync();
		}
		public async Task AddOrderItemsAsync(List<OrderItem> items)
		{
			AddRange(items);
			await SaveAsync();
		}

		public async Task RemoveOrderItemsAsync(List<OrderItem> items)
		{
			RemoveRange(items);
			await SaveAsync();
		}
		public async Task UpdateOrderItemsAsync(int orderId, List<OrderItem> orderItems)
		{
			var existingItems = await GetOrderItemsAsync(orderId);

			RemoveRange(existingItems);

			await Set.AddRangeAsync(orderItems);

			await SaveAsync();
		}

	}

}
