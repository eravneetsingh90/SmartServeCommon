using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class OrderItemStore : BaseStore<OrderItem>
	{
		public OrderItemStore(SmartServeDbContext db) : base(db) { }

		public async Task AddItemAsync(
			int orderId,
			int productId,
			int quantity,
			decimal priceSnapshot)
		{
			var item = new OrderItem
			{
				OrderId = orderId,
				//ProductId = productId,
				Quantity = quantity,
				PriceSnapshot = priceSnapshot
			};

			await AddAsync(item);
		}

		public async Task<List<OrderItem>> GetItemsByOrderAsync(int orderId)
		{
			return await _db.OrderItems
				.Where(i => i.OrderId == orderId)
				.ToListAsync();
		}
	}

}
