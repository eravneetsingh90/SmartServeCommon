using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockStore : IStockStore
	{
		private readonly SmartServeDbContext _db;

		public StockStore(SmartServeDbContext db) 
		{
			_db = db;
		}

		public async Task<List<StockItem>> GetStockItemsAsync()
		{
			return await _db.StockItems
				.Where(x => x.IsActive == true)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<StockItem?> GetStockItemAsync(string itemType, int referenceId)
		{
			return await _db.StockItems
				.FirstOrDefaultAsync(x =>
					x.ItemType == itemType &&
					x.ReferenceId == referenceId &&
					x.IsActive == true);
		}

		public async Task AddTransactionAsync(StockTransaction transaction)
		{
			_db.StockTransactions.Add(transaction);
			await _db.SaveChangesAsync();
		}

		public async Task<List<CurrentStockDto>> GetCurrentStockAsync()
		{
			var query =
				from si in _db.StockItems
				where si.IsActive == true
				join st in _db.StockTransactions
					on si.StockItemId equals st.StockItemId into txnGroup
				select new CurrentStockDto
				{
					StockItemId = si.StockItemId,
					ItemType = si.ItemType,
					ReferenceId = si.ReferenceId,
					Unit = si.Unit,
					MinStockLevel = si.MinStockLevel??0,

					CurrentQuantity = txnGroup.Sum(t =>
						t.TransactionType == "IN" ? t.Quantity :
						t.TransactionType == "OUT" ? -t.Quantity :
						t.Quantity)
				};

			return await query.AsNoTracking().ToListAsync();
		}
	}

}
