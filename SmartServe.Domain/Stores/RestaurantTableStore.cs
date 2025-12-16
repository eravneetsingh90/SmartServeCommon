using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Entities;

namespace SmartServe.Domain.Stores
{
	public class RestaurantTableStore : BaseStore<RestaurantTable>
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
				where table.IsActive == true

				join order in _db.Orders
					.Where(o =>
						o.IsActive ==true &&
						o.ClosedAt == null &&
						o.OrderType == "DINE_IN")
					on table.TableId equals order.TableId into orderGroup

				from activeOrder in orderGroup.DefaultIfEmpty()

				join status in _db.TableStatuses
					on activeOrder.StatusId equals status.StatusId
					into statusGroup

				from tableStatus in statusGroup.DefaultIfEmpty()

				select new GetTableView
				{
					TableId = table.TableId,
					DisplayName = table.DisplayName??string.Empty,

					OrderId = activeOrder != null ? activeOrder.OrderId : null,
					Amount = activeOrder != null ? Convert.ToDecimal(activeOrder.TotalAmount) : 0,
					StatusCode = tableStatus != null ? tableStatus.StatusCode : "BLANK",
					StatusName = tableStatus != null ? tableStatus.StatusName : "Blank Table",
					ColorHex = tableStatus != null ? tableStatus.ColorHex : "#E0E0E0"
				};

			return await query
				.OrderBy(t => t.DisplayName)
				.ToListAsync();
		}

	}

}
