using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using System.Collections.Generic;

namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<Order>, IOrderStore
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public async Task<Order?> GetOrderAsync(int orderId)
		{
			return await Set
				.AsNoTracking()
				.Include(o => o.OrderItems)
					.ThenInclude(oi => oi.Variant)
						.ThenInclude(v => v.Product)
				.FirstOrDefaultAsync(o => o.OrderId == orderId);
		}
		public virtual void Update(Order order)
		{
			var tracked = Db.Orders.Local.FirstOrDefault(x => x.OrderId == order.OrderId);

			if (tracked == null)
			{
				tracked = new Order
				{
					OrderId = order.OrderId
				};

				Attach(tracked);
			}
			Db.Entry(tracked).CurrentValues.SetValues(order);
		}
	}
}
