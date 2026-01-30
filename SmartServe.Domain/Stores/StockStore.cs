using Microsoft.EntityFrameworkCore;
using SmartServe.Domain.Constants;
using SmartServe.Domain.Models;
using SmartServe.EFCore.Db;
using SmartServe.EFCore.Models;

namespace SmartServe.Domain.Stores
{
	public class StockStore : BaseStore<StockEntity>, IStockStore
	{

		public StockStore(SmartServeDbContext db) : base(db)
		{
		}

		public async Task<List<StockEntity>> GetStockAsync()
		{
			return await Set
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<StockEntity>> GetStockAsync(string itemType)
		{
			return await Set
				.Where(x => x.ItemType == itemType)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<StockEntity?> GetStockAsync(string itemType, int referenceId)
		{
			return await Set
				.FirstOrDefaultAsync(x =>
					x.ItemType == itemType &&
					x.VariantId == referenceId);
		}
		public async Task<List<StockEntity>> GetAllStockAsync()
		{
			return await Set
				.AsNoTracking()
				.Include(s => s.Variant)
					.ThenInclude(v => v.Product)
						.ThenInclude(s => s.Category)
				.Where(x => x.IsActive == true)
				.ToListAsync();
		}
		public async Task AddStockAsync(StockEntity stockItem)
		{
			Set.Add(stockItem);
			await SaveAsync();
		}

		public async Task UpdateStockAsync(StockEntity stockItem)
		{
			Set.Update(stockItem);
			await SaveAsync();
		}

		
		public async Task<decimal> GetCurrentStockQuantityAsync(int stockItemId)
		{
			var qty = await Db.StockTransactions
				.Where(x => x.Id == stockItemId)
				.SumAsync(x =>
					x.TransactionType == StockTxnType.IN ? x.Quantity :
					x.TransactionType == StockTxnType.OUT ? -x.Quantity :
					x.Quantity);

			return qty;
		}

		public Task<List<CurrentStock>> GetCurrentStockAsync()
		{
			throw new NotImplementedException();
		}
	}

}
