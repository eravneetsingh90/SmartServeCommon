using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<Order>
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public async Task<Order> CreateOrderAsync(
			int? tableId,
			string orderType,
			int statusId)
		{
			var order = new Order
			{
				TableId = tableId,
				OrderType = orderType,
				StatusId = statusId,
				OrderNumber = $"ORD-{DateTime.Now:yyyyMMddHHmmss}",
				//IsActive = true,
				CreatedAt = DateTime.Now,
				TotalAmount = 0
			};

			await AddAsync(order);
			return order;
		}

		public async Task CloseOrderAsync(int orderId, decimal totalAmount)
		{
			var order = await GetByIdAsync(orderId);
			if (order == null) return;

			order.TotalAmount = totalAmount;
			//order.IsActive = false;
			order.ClosedAt = DateTime.Now;

			await UpdateAsync(order);
		}
	}

}
