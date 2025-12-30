using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class RestaurantTableStore : BaseStore<RestaurantTable>, IRestaurantTableStore
	{
		public RestaurantTableStore(SmartServeDbContext db) : base(db)
		{
		}

		public async Task CreateTableAsync(string displayName)
		{
			var table = new RestaurantTable
			{
				DisplayName = displayName,
				IsActive = true,
				CreatedAt = DateTime.Now
			};

			await AddAsync(table);
		}

		public async Task SoftDeleteTableAsync(int tableId)
		{
			var table = await GetByIdAsync(tableId);
			if (table == null) return;

			table.IsActive = false;
			await UpdateAsync(table);
		}

		public async Task<List<GetTableView>> GetTablesForViewAsync()
		{
			var query =
				from table in _db.RestaurantTables
				where table.IsActive==true

				// ✅ Open order = ClosedAt IS NULL
				let activeOrder = _db.Orders
					.Where(o =>
						o.TableId == table.TableId &&
						o.ClosedAt == null &&
						o.OrderType == "DINE_IN")
					.OrderByDescending(o => o.CreatedAt)
					.Select(o => new
					{
						o.OrderId,
						o.TotalAmount,
						o.StatusId
					})
					.FirstOrDefault()

				// ✅ Resolve table status only if order exists
				let tableStatus = activeOrder != null
					? _db.TableStatuses.FirstOrDefault(s => s.StatusId == activeOrder.StatusId)
					: null

				select new GetTableView
				{
					TableId = table.TableId,
					DisplayName = table.DisplayName ?? string.Empty,

					OrderId = activeOrder != null ? activeOrder.OrderId : null,
					Amount = activeOrder != null ? activeOrder.TotalAmount ?? 0 : 0,

					StatusCode = tableStatus.StatusCode ?? "BLANK",
					StatusName = tableStatus.StatusName ?? "Blank Table",
					ColorHex = tableStatus.ColorHex ?? "#E0E0E0"
				};

			return await query
				.OrderBy(t => t.DisplayName)
				.ToListAsync();
		}


	}

}
