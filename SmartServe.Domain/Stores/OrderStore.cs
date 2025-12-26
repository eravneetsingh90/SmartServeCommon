using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<Order>, IOrderStore
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public Task<Order> CreateOrderAsync(Order order)
		{
			throw new NotImplementedException();
		}

		public Task<Order?> GetOrderAsync(int orderId)
		{
			throw new NotImplementedException();
		}

		public Task UpdateOrderAsync(Order order)
		{
			throw new NotImplementedException();
		}

		public async Task ClearOrderItemsAsync(int orderId)
		{
			var items = _db.OrderItems.Where(x => x.OrderId == orderId);
			_db.OrderItems.RemoveRange(items);
		}

		public Task AddOrderItemsAsync(IEnumerable<OrderItem> items)
		{
			throw new NotImplementedException();
		}
	}

}
