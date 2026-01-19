using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class RestaurantTableStore : BaseStore<RestaurantTableEntity>, IRestaurantTableStore
	{
		private readonly IMapper _mapper;
		public RestaurantTableStore(SmartServeDbContext db, IMapper mapper) : base(db)
		{
			_mapper = mapper;
		}

		public async Task<List<RestaurantTable>> GetActiveRestaurantTablesAsync()
		{
			var items = (await GetAllAsync())
				.Where(c => c.IsActive == true)
				.ToList();
			return _mapper.Map<List<RestaurantTable>>(items);
		}

		public async Task CreateTableAsync(string displayName)
		{
			var table = new RestaurantTableEntity
			{
				DisplayName = displayName,
				IsActive = true,
				CreatedAt = DateTime.Now
			};
			Add(table);
			await SaveAsync();
		}

		public async Task<List<GetTableView>> GetTablesForViewAsync()
		{
			var query =
				from table in Set
				where table.IsActive==true

				// ✅ Open order = ClosedAt IS NULL
				let activeOrder = Db.Orders
					.Where(o =>
						o.TableId == table.Id &&
						o.ClosedAt == null &&
						o.OrderType == "DINE_IN")
					.OrderByDescending(o => o.CreatedAt)
					.Select(o => new
					{
						o.Id,
						o.TotalAmount,
						o.StatusId
					})
					.FirstOrDefault()

				// ✅ Resolve table status only if order exists
				let tableStatus = activeOrder != null
					? Db.TableStatuses.FirstOrDefault(s => s.Id == activeOrder.StatusId)
					: null

				select new GetTableView
				{
					TableId = table.Id,
					DisplayName = table.DisplayName ?? string.Empty,

					OrderId = activeOrder != null ? activeOrder.Id : null,
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
