using Microsoft.EntityFrameworkCore;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace SmartServe.Domain.Stores
{
	public class OrderStore : BaseStore<OrderEntity>, IOrderStore
	{
		public OrderStore(SmartServeDbContext db) : base(db) { }

		public async Task<OrderEntity?> GetOrderAsync(int orderId)
		{
			return await Set
				.AsNoTracking()
				.Include(o => o.OrderItems)
					.ThenInclude(oi => oi.Variant)
						.ThenInclude(v => v.Product)
				.FirstOrDefaultAsync(o => o.Id == orderId);
		}
		public async Task<OrderEntity?> GetByOrderNumberAsync(string orderNumber)
		{
			return await Set.AsNoTracking()
				.FirstOrDefaultAsync(e => e.OrderNumber == orderNumber);
		}
		public async Task<List<OrderEntity>> GetByDateFilterAsync(DateTime fromUtc,DateTime toUtc)
		{
			var result = await Set
				.AsNoTracking()
				.Where(o => o.CreatedAt >= fromUtc && o.CreatedAt < toUtc)
				.Include(o=>o.Status)
				.OrderByDescending(o=> o.CreatedAt)
				.ToListAsync();
			return result;
		}
		public virtual void Update(OrderEntity order)
		{
			var tracked = Db.Orders.Local.FirstOrDefault(x => x.Id == order.Id);

			if (tracked == null)
			{
				tracked = new OrderEntity
				{
					Id = order.Id
				};

				Attach(tracked);
			}
			Db.Entry(tracked).CurrentValues.SetValues(order);
		}
	}
}
