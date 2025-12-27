using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<Order>, IOrderStore
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public async Task<Order> CreateOrderAsync(Order order)
		{
			_db.Orders.Add(order);
			await _db.SaveChangesAsync();
			return order;
		}

		public async Task<Order?> GetOrderAsync(int orderId)
		{
			return await _db.Orders
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task UpdateOrderAsync(Order order)
		{
			_db.Orders.Update(order);
			await _db.SaveChangesAsync();
		}

		public async Task AddOrderItemsAsync(IEnumerable<OrderItem> items)
		{
			_db.OrderItems.AddRange(items);
			await _db.SaveChangesAsync();
		}

		public async Task ClearOrderItemsAsync(int orderId)
		{
			var items = _db.OrderItems.Where(x => x.OrderId == orderId);
			_db.OrderItems.RemoveRange(items);
			await _db.SaveChangesAsync();
		}
	}
}
