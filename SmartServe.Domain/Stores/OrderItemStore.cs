using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;
using SmartServe.Resources.Provider;

namespace SmartServe.Domain.Stores
{
	public class OrderItemStore : BaseStore<OrderItemEntity>, IOrderItemStore
	{
		public OrderItemStore(SmartServeDbContext db, ITenantProvider tenantProvider) : base(db,tenantProvider) { }

		public async Task<List<OrderItemEntity>> GetOrderItemsAsync(int orderId)
		{
			return await Set
				.Where(i => i.OrderId == orderId)
				.Include(i => i.Variant)
						.ThenInclude(v => v.Product)
				.ToListAsync();
		}
		public async Task AddOrderItemsAsync(List<OrderItemEntity> items)
		{
			AddRange(items);
			await SaveAsync();
		}

		public async Task RemoveOrderItemsAsync(List<OrderItemEntity> items)
		{
			RemoveRange(items);
			await SaveAsync();
		}
		public async Task UpdateOrderItemsAsync(int orderId, List<OrderItemEntity> orderItems)
		{
			var existingItems = await GetOrderItemsAsync(orderId);

			RemoveRange(existingItems);

			await Set.AddRangeAsync(orderItems);

			await SaveAsync();
		}

	}

}
